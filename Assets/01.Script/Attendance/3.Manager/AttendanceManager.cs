using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    [SerializeField]
    private AttendanceRewardSOList _attendanceRewardList; // �Ϲ� ���� ����Ʈ
    private List<AttendanceRewardSO> _metaDatas;

    [SerializeField]
    private AttendanceRewardSOList _streakAttendanceRewardList; // ���� ���� ����Ʈ
    private List<AttendanceRewardSO> _metaDatasStreak;



    private List<Attendance> _attendances;
    public List<AttendanceDTO> Attendances => _attendances.ConvertAll((a) => new AttendanceDTO(a));

    private List<StreakAttendance> streakAttendances;
    public List<AttendanceDTO> StreakAttendances => streakAttendances.ConvertAll((a) => new AttendanceDTO(a));


    private AttendanceRepository _attendanceRepository;

    private DateTime _lastAttendanceDate;
    private int _attendanceCount;
    private int _maxStreakAttendanceCount;
    private int _currentStreakAttendanceCount;



    private string _email => AccountManager.Instance?.CurrentAccount.Email ?? "myEmail";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        _attendanceRepository = new AttendanceRepository();
        _metaDatas = _attendanceRewardList.Attendances;
        _metaDatasStreak = _streakAttendanceRewardList.Attendances;

        (List<AttendanceSaveData>, List<AttendanceSaveData>) attendanceSaveDataTuple = _attendanceRepository.Load(_email);

        // �Ϲ� �⼮ ����Ʈ
        _attendances = new List<Attendance>();
        List<AttendanceSaveData> saveDatas = attendanceSaveDataTuple.Item1;
        foreach (var metaData in _metaDatas)
        {
            // �ߺ� �˻�
            Attendance duplicatedAttendance = FindByID(metaData.ID);
            if (duplicatedAttendance != null)
            {
                throw new Exception($"�⼮ ID({metaData.ID})�� �ߺ��˴ϴ�.");
            }

            AttendanceSaveData saveData = 
                saveDatas?.Find(a => a.ID == metaData.ID) ?? new AttendanceSaveData();
            Attendance attendance = new Attendance(metaData, saveData);
            _attendances.Add(attendance);

            Events.AttendanceRefreshEvent.Attendance = attendance;
            EventManager.Broadcast(Events.AttendanceRefreshEvent);

        }

        // ���� �⼮ ����Ʈ
        streakAttendances = new List<StreakAttendance>();
        List<AttendanceSaveData> streakSaveDatas = attendanceSaveDataTuple.Item2;
        foreach (var metaData in _metaDatasStreak)
        {
            // �ߺ� �˻�
            StreakAttendance duplicatedStreakAttendance = streakAttendances.Find(a => a.ID == metaData.ID);
            if (duplicatedStreakAttendance != null)
            {
                throw new Exception($"���� �⼮ ID({metaData.ID})�� �ߺ��˴ϴ�.");
            }

            AttendanceSaveData saveData = 
                streakSaveDatas?.Find(a => a.ID == metaData.ID) ?? new AttendanceSaveData();
            StreakAttendance streakAttendance = new StreakAttendance(metaData, saveData);
            streakAttendances.Add(streakAttendance);

            Events.StreakAttendanceRefreshEvent.StreakAttendance = streakAttendance;
            EventManager.Broadcast(Events.StreakAttendanceRefreshEvent);
        }
        TryIncreaseStreak();
        CheckCanClaimReward();
    }

    public void TryIncreaseStreak()
    {
        int lastAttendanceDay = _lastAttendanceDate.Day;
        int currentDay = DateTime.Now.Day;
        _lastAttendanceDate = DateTime.Now;
        if (lastAttendanceDay == currentDay)
        {
            return; // �̹� �⼮�� ���̸� �ƹ��͵� ���� �ʴ´�.
        }
        if (lastAttendanceDay + 1 == currentDay)
        {
            _currentStreakAttendanceCount++;
            _maxStreakAttendanceCount = Math.Max(_maxStreakAttendanceCount, _currentStreakAttendanceCount);
        }
        else
        {
            _currentStreakAttendanceCount = 1;
        }

        _attendanceRepository.Save(_email, Attendances, StreakAttendances);
    }

    // �⼮ ���� ��, ���� �� �ִ� �ֵ��� ������ �ޱ� ��ư Ȱ��ȭ
    public void CheckCanClaimReward()
    {
        foreach (var attendance in _attendances)
        {
            if (attendance.CanClaimReward(_attendanceCount))
            {
                Events.AttendanceRewardClaimButtonActivateEvent.IsActive = true;
                return;
            }
        }

        foreach (var streakAttendance in streakAttendances)
        {
            if (streakAttendance.CanClaimReward(_maxStreakAttendanceCount))
            {
                Events.AttendanceRewardClaimButtonActivateEvent.IsActive = true;
                return;
            }
        }
    }

    // ��ư�� ������ ��, ���� �� �ִ� �ֵ� ��� �ް� �ϱ�
    public void ClaimReward()
    {
        foreach (var attendance in _attendances)
        {
            if (!attendance.RewardClaimed && attendance.TryClaimReward(_attendanceCount))
            {
                CurrencyManager.Instance.Add(attendance.CurrencyType, attendance.RewardAmount);
                Events.AttendanceRefreshEvent.Attendance = attendance;
                EventManager.Broadcast(Events.AttendanceRefreshEvent);
            }
        }

        foreach (var streakAttendance in streakAttendances)
        {
            if (!streakAttendance.RewardClaimed && streakAttendance.TryClaimReward(_maxStreakAttendanceCount))
            {
                CurrencyManager.Instance.Add(streakAttendance.CurrencyType, streakAttendance.RewardAmount);
                Events.StreakAttendanceRefreshEvent.StreakAttendance = streakAttendance;
                EventManager.Broadcast(Events.StreakAttendanceRefreshEvent);
            }
        }

        _attendanceRepository.Save(_email, Attendances, StreakAttendances);
    }


    private Attendance FindByID(string id)
    {
        return _attendances.Find(a => a.ID == id);
    }


}

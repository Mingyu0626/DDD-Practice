using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    [SerializeField]
    private AttendanceRewardSOList _attendanceRewardList; // 일반 보상 리스트
    private List<AttendanceRewardSO> _metaDatas;

    [SerializeField]
    private AttendanceRewardSOList _streakAttendanceRewardList; // 연속 보상 리스트
    private List<AttendanceRewardSO> _metaDatasStreak;



    private List<Attendance> _attendances;
    public List<DailyAttendanceDTO> Attendances => _attendances.ConvertAll((a) => new DailyAttendanceDTO(a));

    private List<StreakAttendance> streakAttendances;
    public List<DailyAttendanceDTO> StreakAttendances => streakAttendances.ConvertAll((a) => new DailyAttendanceDTO(a));

    private AttendanceHistory _attendanceHistory;
    public AttendanceHistoryDTO AttendanceHistoryDTO => new AttendanceHistoryDTO(_attendanceHistory);

    private AttendanceRepository _attendanceRepository;



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

        (List<DailyAttendanceSaveData>, List<DailyAttendanceSaveData>, AttendanceHistory) attendanceSaveDataTuple 
            = _attendanceRepository.Load(_email);

        // 일반 출석 리스트
        _attendances = new List<Attendance>();
        List<DailyAttendanceSaveData> saveDatas = attendanceSaveDataTuple.Item1;
        foreach (var metaData in _metaDatas)
        {
            // 중복 검사
            Attendance duplicatedAttendance = FindByID(metaData.ID);
            if (duplicatedAttendance != null)
            {
                throw new Exception($"출석 ID({metaData.ID})가 중복됩니다.");
            }

            DailyAttendanceSaveData saveData = 
                saveDatas?.Find(a => a.ID == metaData.ID) ?? new DailyAttendanceSaveData();
            Attendance attendance = new Attendance(metaData, saveData);
            _attendances.Add(attendance);

            Events.AttendanceRefreshEvent.Attendance = attendance;
            EventManager.Broadcast(Events.AttendanceRefreshEvent);

        }

        // 연속 출석 리스트
        streakAttendances = new List<StreakAttendance>();
        List<DailyAttendanceSaveData> streakSaveDatas = attendanceSaveDataTuple.Item2;
        foreach (var metaData in _metaDatasStreak)
        {
            // 중복 검사
            StreakAttendance duplicatedStreakAttendance = streakAttendances.Find(a => a.ID == metaData.ID);
            if (duplicatedStreakAttendance != null)
            {
                throw new Exception($"연속 출석 ID({metaData.ID})가 중복됩니다.");
            }

            DailyAttendanceSaveData saveData = 
                streakSaveDatas?.Find(a => a.ID == metaData.ID) ?? new DailyAttendanceSaveData();
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
        int lastAttendanceDay = _attendanceHistory.LastAttendanceDate.Day;
        int currentDay = DateTime.Now.Day;
        _attendanceHistory.LastAttendanceDate = DateTime.Now;
        if (lastAttendanceDay == currentDay)
        {
            return; // 이미 출석한 날이면 아무것도 하지 않는다.
        }
        if (lastAttendanceDay + 1 == currentDay)
        {
            _attendanceHistory.CurrentStreakAttendanceCount++;
            _attendanceHistory.MaxStreakAttendanceCount = 
                Math.Max(_attendanceHistory.MaxStreakAttendanceCount, _attendanceHistory.CurrentStreakAttendanceCount);
        }
        else
        {
            _attendanceHistory.CurrentStreakAttendanceCount = 1;
        }
        _attendanceHistory.AttendanceCount++;
        _attendanceRepository.Save(_email, Attendances, StreakAttendances, AttendanceHistoryDTO);
    }

    // 출석 했을 때, 받을 수 있는 애들이 있으면 받기 버튼 활성화
    public void CheckCanClaimReward()
    {
        foreach (var attendance in _attendances)
        {
            if (attendance.CanClaimReward(_attendanceHistory.AttendanceCount))
            {
                Events.AttendanceRewardClaimButtonActivateEvent.IsActive = true;
                return;
            }
        }

        foreach (var streakAttendance in streakAttendances)
        {
            if (streakAttendance.CanClaimReward(_attendanceHistory.MaxStreakAttendanceCount))
            {
                Events.AttendanceRewardClaimButtonActivateEvent.IsActive = true;
                return;
            }
        }
    }

    // 버튼을 눌렀을 때, 받을 수 있는 애들 모두 받게 하기
    public void ClaimReward()
    {
        foreach (var attendance in _attendances)
        {
            if (!attendance.RewardClaimed && attendance.TryClaimReward(_attendanceHistory.AttendanceCount))
            {
                CurrencyManager.Instance.Add(attendance.CurrencyType, attendance.RewardAmount);
                Events.AttendanceRefreshEvent.Attendance = attendance;
                EventManager.Broadcast(Events.AttendanceRefreshEvent);
            }
        }

        foreach (var streakAttendance in streakAttendances)
        {
            if (!streakAttendance.RewardClaimed && streakAttendance.TryClaimReward(_attendanceHistory.MaxStreakAttendanceCount))
            {
                CurrencyManager.Instance.Add(streakAttendance.CurrencyType, streakAttendance.RewardAmount);
                Events.StreakAttendanceRefreshEvent.StreakAttendance = streakAttendance;
                EventManager.Broadcast(Events.StreakAttendanceRefreshEvent);
            }
        }

        _attendanceRepository.Save(_email, Attendances, StreakAttendances, AttendanceHistoryDTO);
    }


    private Attendance FindByID(string id)
    {
        return _attendances.Find(a => a.ID == id);
    }


}

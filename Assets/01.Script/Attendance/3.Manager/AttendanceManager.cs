using System;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    [SerializeField]
    private AttendanceRewardSOList _attendanceRewardList; // 일반 보상 리스트
    [SerializeField]
    private StreakAttendanceRewardSOList _streakAttendanceRewardList; // 연속 보상 리스트


    private AttendanceRepository _attendanceRepository;

    public event Action OnDateChanged;

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
    }

    private void CheckDay()
    {

    }

    public bool TryGetReward()
    {
        return false;
    }

    public AttendanceRewardSO GetRewardData(int day)
    {
        return _attendanceRewardList.Attendances.Find(reward => reward.RewardDay == day);
    }
}

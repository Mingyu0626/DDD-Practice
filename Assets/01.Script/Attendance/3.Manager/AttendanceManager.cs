using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    public readonly AttendanceRewardSOList AttendanceRewardList; // 일반 보상 리스트
    public readonly StreakAttendanceRewardSOList StreakAttendanceRewardList; // 연속 보상 리스트
    private AttendanceRepository _attendanceRepository;



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

    public bool TryGetReward()
    {

    }

    public AttendanceRewardSO GetRewardData(int day)
    {

    }



}

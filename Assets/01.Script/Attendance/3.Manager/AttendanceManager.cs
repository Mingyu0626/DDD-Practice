using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    public readonly AttendanceRewardSOList AttendanceRewardList; // �Ϲ� ���� ����Ʈ
    public readonly StreakAttendanceRewardSOList StreakAttendanceRewardList; // ���� ���� ����Ʈ
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

using System;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    [SerializeField]
    private AttendanceRewardSOList _attendanceRewardList; // 일반 보상 리스트

    private Attendance _attendance;
    private AttendanceRepository _attendanceRepository;



    private string _email => AccountManager.Instance.CurrentAccount.Email;

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

}

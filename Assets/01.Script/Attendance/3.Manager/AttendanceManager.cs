using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
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
}

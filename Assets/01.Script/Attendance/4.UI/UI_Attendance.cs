using System.Collections.Generic;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;

public class UI_Attendance : MonoBehaviour
{
    [Header("# Panel Info Texts")]
    [SerializeField] private TextMeshProUGUI StreakInfoText;

    [Header("# Slots")]
    [SerializeField] private List<UI_AttendanceSlot> _slots;
    [SerializeField] private List<UI_AttendanceSlot> _streakSlots;
    //[SerializeField]
    //private GameObject _attendacneSlotUIPrefab;

    private void Start()
    {
        Init();

        EventManager.AddListener<AttendanceRefreshEvent>(RefreshAttendanceSlots);
        EventManager.AddListener<StreakAttendanceRefreshEvent>(RefreshStreakAttendanceSlots);
    }

    private void Init()
    {
        foreach (var slot in _slots)
        {
            //slot.Refresh(AttendanceManager.Instance.Attendances);
        }
        foreach(var slot in _streakSlots)
        {
            //slot.Refresh(AttendanceManager.Instance.StreakAttendances);
        }
    }

    public void OnClickClaimRewardButton()
    {
        //AttendanceManager.Instance.TryGetReward();
    }

    private void RefreshAttendanceSlots(AttendanceRefreshEvent evt)
    {
        foreach(var slot in _slots)
        {
            slot.Refresh(evt.Attendance);
        }
    }

    private void RefreshStreakAttendanceSlots(StreakAttendanceRefreshEvent evt)
    {
        foreach(var slot in _streakSlots)
        {
            slot.Refresh(evt.StreakAttendance);
        }
    }
}

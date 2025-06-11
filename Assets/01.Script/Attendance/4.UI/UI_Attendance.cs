using System.Collections.Generic;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

public class UI_Attendance : MonoBehaviour
{
    [Header("# Panel Info")]
    [SerializeField] private TextMeshProUGUI StreakInfoText;
    [SerializeField] private GameObject _getRewardButton;

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
        EventManager.AddListener<AttendanceRewardClaimButtonActivateEvent>(RefreshButton);
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
        AttendanceManager.Instance.ClaimReward();
    }

    private void RefreshAttendanceSlots(AttendanceRefreshEvent evt)
    {
        foreach(var slot in _slots)
        {
            slot.Refresh(evt.Attendance);
        }
        RefreshButton();
    }

    private void RefreshStreakAttendanceSlots(StreakAttendanceRefreshEvent evt)
    {
        foreach(var slot in _streakSlots)
        {
            slot.Refresh(evt.StreakAttendance);
        }
        RefreshButton();
    }

    private void RefreshButton()
    {
        _getRewardButton.SetActive(false);
    }

    private void RefreshButton(AttendanceRewardClaimButtonActivateEvent evt)
    {
        _getRewardButton.SetActive(evt.IsActive);
    }
}

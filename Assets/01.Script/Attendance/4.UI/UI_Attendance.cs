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
        for(int i=0; i<_slots.Count; ++i)
        {
            _slots[i].InitData(AttendanceManager.Instance.GetRewardData(i));
        }
        for (int i = 0; i < _streakSlots.Count; ++i)
        {
            _slots[i].InitData(AttendanceManager.Instance.GetRewardData(i)); // 나중에 streak용으로 수정
        }
    }

    public void OnClickClaimRewardButton()
    {
        AttendanceManager.Instance.TryGetReward();
    }

    private void RefreshAttendanceSlots(AttendanceRefreshEvent evt)
    {
        
    }

    private void RefreshStreakAttendanceSlots(StreakAttendanceRefreshEvent evt)
    {

    }
}

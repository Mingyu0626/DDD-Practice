using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttendanceSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] TextMeshProUGUI _dayText;
    [SerializeField] TextMeshProUGUI _amountText;

    public void InitData(AttendanceRewardSO attendanceReward)
    {
        _dayText.text = attendanceReward.RewardDay.ToString();
        _amountText.text = attendanceReward.RewardAmount.ToString();
    }

    public void InitData(StreakAttendanceRewardSO streakAttendanceReward)
    {
        _dayText.text = streakAttendanceReward.RewardDay.ToString();
        _amountText.text = streakAttendanceReward.RewardAmount.ToString();
    }
}

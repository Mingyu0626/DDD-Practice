using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttendanceSlot : MonoBehaviour
{
    [SerializeField] private GameObject _alreadyGetIcon;
    [SerializeField] private GameObject _canGetIcon;
    [SerializeField] TextMeshProUGUI _dayText;
    [SerializeField] TextMeshProUGUI _amountText;

    public void InitData(AttendanceRewardSO attendanceReward)
    {
        _dayText.text = attendanceReward.RewardDay.ToString();
        _amountText.text = attendanceReward.RewardAmount.ToString();
    }

    public void Refresh(Attendance attendance)
    {
        if (attendance.RewardClaimed)
        {
            _alreadyGetIcon.SetActive(true);
            _canGetIcon.SetActive(false);
        }
        //else if (!attendance.RewardClaimed && attendance.CanClaimReward(AttendanceManager.Instance.누적출석일수))
        //{
        //    _alreadyGetIcon.SetActive(false);
        //    _canGetIcon.SetActive(true);
        //}
        else
        {
            _canGetIcon.SetActive(false);
            _alreadyGetIcon.SetActive(false);
        }
    }

    public void Refresh(StreakAttendance attendance)
    {
        if (attendance.RewardClaimed)
        {
            _alreadyGetIcon.SetActive(true);
            _canGetIcon.SetActive(false);
        }
        //else if (!attendance.RewardClaimed && attendance.CanClaimReward(AttendanceManager.Instance.연속출석일수))
        //{
        //    _alreadyGetIcon.SetActive(false);
        //    _canGetIcon.SetActive(true);
        //}
        else
        {
            _canGetIcon.SetActive(false);
            _alreadyGetIcon.SetActive(false);
        }
    }
}
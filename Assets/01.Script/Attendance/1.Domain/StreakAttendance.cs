using UnityEngine;

public class StreakAttendance : Attendance
{
    public StreakAttendance(AttendanceRewardSO metaData, AttendanceSaveData saveData) : base(metaData, saveData)
    {
    }

    public override bool CanClaimReward(int day)
    {
        return !_rewardClaimed && RewardDay <= day;
    }

    public override bool TryClaimReward(int day)
    {
        if (CanClaimReward(day))
        {
            _rewardClaimed = true;
            return true;
        }
        return false;
    }

}

using System;
using UnityEngine;

public class Attendance
{
    public readonly string ID;
    public readonly int RewardDay;
    public readonly ECurrencyType CurrencyType;
    public readonly int RewardAmount;


    protected bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;
    

    public Attendance(AttendanceRewardSO  metaData, DailyAttendanceSaveData saveData)
    {
        ID = metaData.ID;
        RewardDay = metaData.RewardDay;
        CurrencyType = metaData.CurrencyType;
        RewardAmount = metaData.RewardAmount;

        _rewardClaimed = saveData.RewardClaimed;
    }

    public virtual bool CanClaimReward(int day)
    {
        return !_rewardClaimed && RewardDay <= day;
    }

    public virtual bool TryClaimReward(int day)
    {
        if (CanClaimReward(day))
        {
            _rewardClaimed = true;
            return true;
        }
        return false;
    }

    public DailyAttendanceDTO ToDTO()
    {
        return new DailyAttendanceDTO(this);
    }
}

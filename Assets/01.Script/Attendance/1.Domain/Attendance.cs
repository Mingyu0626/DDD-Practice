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
    

    public Attendance(AttendanceRewardSO  metaData, AttendanceSaveData saveData)
    {
        ID = metaData.ID;
        RewardDay = metaData.RewardDay;
        CurrencyType = metaData.CurrencyType;
        RewardAmount = metaData.RewardAmount;

        _rewardClaimed = saveData.RewardClaimed;
    }

    public Attendance(AttendanceDTO attendanceDto)
    {

    }

    public virtual bool CanClaimReward(int day)
    {
        return !_rewardClaimed && RewardDay <= day;
    }
    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO(this);
    }
}

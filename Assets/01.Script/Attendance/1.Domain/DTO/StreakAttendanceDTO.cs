using System;
using UnityEngine;

[Serializable]
public class StreakAttendanceDTO
{
    public readonly string ID;
    public readonly int RewardDay;
    public readonly bool RewardClaimed;

    public StreakAttendanceDTO(string id, int rewardDay, bool rewardClaimed)
    {
        ID = id;
        RewardDay = rewardDay;
        RewardClaimed = rewardClaimed;
    }

    public StreakAttendanceDTO(StreakAttendance streakAttendance)
    {
        ID = streakAttendance.ID;
        RewardDay = streakAttendance.RewardDay;
        RewardClaimed = streakAttendance.RewardClaimed;
    }

    public bool CanClaimReward()
    {
        return !RewardClaimed;
    }
}

using System;
using UnityEngine;

[Serializable]
public class DailyAttendanceDTO
{
    public readonly string ID;
    public readonly int RewardDay;
    public readonly ECurrencyType CurrencyType;
    public readonly int RewardAmount;
    public readonly bool RewardClaimed;

    public DailyAttendanceDTO(DailyAttendanceSaveData saveData)
    {
        ID = saveData.ID;
        RewardClaimed = saveData.RewardClaimed;
        RewardDay = 0; // �⺻��, ���� ���� Attendance���� ����
        CurrencyType = ECurrencyType.Gold; // �⺻��, ���� ���� Attendance���� ����
        RewardAmount = 0; // �⺻��, ���� ���� Attendance���� ����
    }

    public DailyAttendanceDTO(string id, int rewardDay, ECurrencyType currencyType, int rewardAmount, bool rewardClaimed)
    {
        ID = id;
        RewardDay = rewardDay;
        CurrencyType = currencyType;
        RewardAmount = rewardAmount;
        RewardClaimed = rewardClaimed;
    }

    public DailyAttendanceDTO(Attendance attendance)
    {
        ID = attendance.ID;
        RewardDay = attendance.RewardDay;
        CurrencyType = attendance.CurrencyType;
        RewardAmount = attendance.RewardAmount;
        RewardClaimed = attendance.RewardClaimed;
    }

    public bool CanClaimReward()
    {
        return !RewardClaimed;
    }
}

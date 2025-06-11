using UnityEngine;

[CreateAssetMenu(fileName = "StreakAttendanceRewardSO", menuName = "Scriptable Objects/StreakAttendanceRewardSO")]
public class StreakAttendanceRewardSO : ScriptableObject
{
    public int RewardDay;
    public ECurrencyType CurrencyType;
    public int RewardAmount;
}

using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceSO", menuName = "Scriptable Objects/AttendanceSO")]
public class AttendanceRewardSO : ScriptableObject
{
    public int RewardDay;
    public ECurrencyType CurrencyType;
    public int RewardAmount;
}

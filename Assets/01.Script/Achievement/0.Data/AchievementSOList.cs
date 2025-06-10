using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementSOList", menuName = "Scriptable Objects/AchievementSOList")]
public class AchievementSOList : ScriptableObject
{
    [SerializeField]
    private List<AchievementSO> _achievements = new List<AchievementSO>();
    public List<AchievementSO> Achievements => _achievements;
}
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AchievementSaveData
{
    public string ID;
    public int CurrentValue;
    public bool RewardClaimed;

}

[Serializable]
public struct AchievementSaveDataList
{
    public List<AchievementSaveData> DataList;
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementRepository
{
    // Repository: 데이터의 영속성 보장
    // 영속성: 프로그램을 종료해도 데이터가 보존되는 것

    private const string SAVE_KEY = nameof(AchievementRepository);

    public void Save(List<AchievementDTO> dataList)
    {
        AchievementSaveDataList datas = new AchievementSaveDataList();
        datas.DataList = dataList.ConvertAll(data => new AchievementSaveData
        {
            ID = data.ID,
            CurrentValue = data.CurrentValue,
            RewardClaimed = data.RewardClaimed
        });
        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    // 원래 FM대로는 DTO를 넘겨주는게 맞지만..
    // 이정도 유도리는 있어도 된다고 생각
    public List<AchievementSaveData> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }
        string json = PlayerPrefs.GetString(SAVE_KEY);
        AchievementSaveDataList datas = JsonUtility.FromJson<AchievementSaveDataList>(json);
        return datas.DataList;
    }
}

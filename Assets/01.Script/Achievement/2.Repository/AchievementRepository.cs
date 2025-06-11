using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementRepository
{
    // Repository: �������� ���Ӽ� ����
    // ���Ӽ�: ���α׷��� �����ص� �����Ͱ� �����Ǵ� ��

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

    // ���� FM��δ� DTO�� �Ѱ��ִ°� ������..
    // ������ �������� �־ �ȴٰ� ����
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

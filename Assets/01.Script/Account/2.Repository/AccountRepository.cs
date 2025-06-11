using System.Collections.Generic;
using UnityEngine;

public class AccountRepository
{
    // Repository: 데이터의 영속성 보장
    // 영속성: 프로그램을 종료해도 데이터가 보존되는 것
    private const string SAVE_KEY = nameof(AccountRepository);

    public void Save(AccountDTO data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public AccountDTO Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }
        string json = PlayerPrefs.GetString(SAVE_KEY);
        return JsonUtility.FromJson<AccountDTO>(json);
    }

}

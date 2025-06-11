using System.Collections.Generic;
using UnityEngine;

public class AccountRepository
{
    // Repository: �������� ���Ӽ� ����
    // ���Ӽ�: ���α׷��� �����ص� �����Ͱ� �����Ǵ� ��
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

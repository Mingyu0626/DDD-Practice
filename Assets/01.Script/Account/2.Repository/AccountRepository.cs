using System.Collections.Generic;
using UnityEngine;

public class AccountRepository
{
    // Repository: 데이터의 영속성 보장
    // 영속성: 프로그램을 종료해도 데이터가 보존되는 것
    private const string SAVE_PREFIX = "ACCOUNT_";

    public void Save(AccountDTO accountDto)
    {
        AccountSaveData accountSaveData = new AccountSaveData(accountDto);
        string json = JsonUtility.ToJson(accountSaveData);
        PlayerPrefs.SetString(SAVE_PREFIX + accountSaveData.Email, json);
    }

    public AccountSaveData Find(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_PREFIX + email))
        {
            return null;
        }
        string json = PlayerPrefs.GetString(SAVE_PREFIX + email);
        return JsonUtility.FromJson<AccountSaveData>(json);
    }
}

public class AccountSaveData
{
    public string Email;
    public string Nickname;
    public string Password;
    public AccountSaveData(AccountDTO accountDto)
    {
        Email = accountDto.Email;
        Nickname = accountDto.Nickname;
        Password = accountDto.Password;
    }
}

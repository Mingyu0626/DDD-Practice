using System;
using UnityEngine;

[Serializable]
public class AccountDTO
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public AccountDTO(string email, string nickname, string password)
    {
        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDTO(Account account)
    {
        Email = account.Email;
        Nickname = account.Nickname;
        Password = account.Password;
    }
    public AccountDTO(AccountSaveData accountSaveData)
    {
        Email = accountSaveData.Email;
        Nickname = accountSaveData.Nickname;
        Password = accountSaveData.Password;
    }
}

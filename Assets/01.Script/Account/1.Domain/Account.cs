using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public Account(string email, string nickname, string password)
    {
        // 규칙을 객체로 캡슐화해서 분리한다.
        // 그래서, 도메인과 UI는 모두 "이 규칙을 만족하니?"라고 물어보면 된다.
        // 캡슐화한 규칙을 DDD에서는 "명세"라고 한다.

        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }

    // 보통 도메인 자체에서 DTO로 만들어주는 메서드를 넣는다.
    public AccountDTO ToDTO()
    {
        return new AccountDTO(this);
    }
}
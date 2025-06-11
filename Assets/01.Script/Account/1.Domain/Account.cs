using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    // ÀÌ¸ŞÀÏ Á¤±ÔÇ¥Çö½Ä (°£´ÜÇÑ RFC5322 ±â¹İ)
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    // ´Ğ³×ÀÓ: ÇÑ±Û ¶Ç´Â ¿µ¾î·Î ±¸¼º, 2~7ÀÚ
    private static readonly Regex NicknameRegex = new Regex(@"^[°¡-ÆRa-zA-Z]{2,7}$", RegexOptions.Compiled);

    // ±İÁöµÈ ´Ğ³×ÀÓ (ºñ¼Ó¾î µî)
    private static readonly string[] ForbiddenNicknames = { "¹Ùº¸", "¸ÛÃ»ÀÌ", "¿î¿µÀÚ", "±èÈ«ÀÏ" };

    public Account(string email, string nickname, string password)
    {
        // ÀÌ¸ŞÀÏ °ËÁõ
        if (string.IsNullOrEmpty(email))
        {
            throw new Exception("ÀÌ¸ŞÀÏÀº ºñ¾îÀÖÀ» ¼ö ¾ø½À´Ï´Ù.");
        }

        if (!EmailRegex.IsMatch(email))
        {
            throw new Exception("¿Ã¹Ù¸¥ ÀÌ¸ŞÀÏ Çü½ÄÀÌ ¾Æ´Õ´Ï´Ù.");
        }

        // ´Ğ³×ÀÓ °ËÁõ
        if (string.IsNullOrEmpty(nickname))
        {
            throw new Exception("´Ğ³×ÀÓÀº ºñ¾îÀÖÀ» ¼ö ¾ø½À´Ï´Ù.");
        }

        if (!NicknameRegex.IsMatch(nickname))
        {
            throw new Exception("´Ğ³×ÀÓÀº 2ÀÚ ÀÌ»ó 7ÀÚ ÀÌÇÏÀÇ ÇÑ±Û ¶Ç´Â ¿µ¹®ÀÌ¾î¾ß ÇÕ´Ï´Ù.");
        }

        foreach (var forbidden in ForbiddenNicknames)
        {
            if (nickname.Contains(forbidden))
            {
                throw new Exception($"´Ğ³×ÀÓ¿¡ ºÎÀûÀıÇÑ ´Ü¾î°¡ Æ÷ÇÔµÇ¾î ÀÖ½À´Ï´Ù: {forbidden}");
            }
        }

        // ºñ¹Ğ¹øÈ£ °ËÁõ
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("ºñ¹Ğ¹øÈ£´Â ºñ¾îÀÖÀ» ¼ö ¾ø½À´Ï´Ù.");
        }

        if (password.Length < 6 || 12 < password.Length)
        {
            throw new Exception("ºñ¹Ğ¹øÈ£´Â 6ÀÚ ÀÌ»ó 12ÀÚ ÀÌÇÏÀÌ¾î¾ß ÇÕ´Ï´Ù.");
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }
}
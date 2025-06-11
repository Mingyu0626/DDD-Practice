using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    // �̸��� ����ǥ���� (������ RFC5322 ���)
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    // �г���: �ѱ� �Ǵ� ����� ����, 2~7��
    private static readonly Regex NicknameRegex = new Regex(@"^[��-�Ra-zA-Z]{2,7}$", RegexOptions.Compiled);

    // ������ �г��� (��Ӿ� ��)
    private static readonly string[] ForbiddenNicknames = { "�ٺ�", "��û��", "���", "��ȫ��" };

    public Account(string email, string nickname, string password)
    {
        // �̸��� ����
        if (string.IsNullOrEmpty(email))
        {
            throw new Exception("�̸����� ������� �� �����ϴ�.");
        }

        if (!EmailRegex.IsMatch(email))
        {
            throw new Exception("�ùٸ� �̸��� ������ �ƴմϴ�.");
        }

        // �г��� ����
        if (string.IsNullOrEmpty(nickname))
        {
            throw new Exception("�г����� ������� �� �����ϴ�.");
        }

        if (!NicknameRegex.IsMatch(nickname))
        {
            throw new Exception("�г����� 2�� �̻� 7�� ������ �ѱ� �Ǵ� �����̾�� �մϴ�.");
        }

        foreach (var forbidden in ForbiddenNicknames)
        {
            if (nickname.Contains(forbidden))
            {
                throw new Exception($"�г��ӿ� �������� �ܾ ���ԵǾ� �ֽ��ϴ�: {forbidden}");
            }
        }

        // ��й�ȣ ����
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("��й�ȣ�� ������� �� �����ϴ�.");
        }

        if (password.Length < 6 || 12 < password.Length)
        {
            throw new Exception("��й�ȣ�� 6�� �̻� 12�� �����̾�� �մϴ�.");
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }
}
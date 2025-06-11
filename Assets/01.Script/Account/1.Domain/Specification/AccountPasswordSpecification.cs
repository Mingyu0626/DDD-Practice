using System;
using UnityEngine;

public class AccountPasswordSpecification : ISpecification<string>
{
    public string ErrorMessage { get; private set; }

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "��й�ȣ�� ������� �� �����ϴ�.";
            return false;
        }

        if (value.Length < 6 || 12 < value.Length)
        {
            Debug.Log($"��й�ȣ ���� ����: {value.Length}��");
            ErrorMessage = "��й�ȣ�� 6�� �̻� 12�� �����̾�� �մϴ�.";
            return false;
        }
        return true;
    }
}

using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    private Account _myAccount;
    public AccountDTO CurrentAccount => _myAccount?.ToDTO();
    private AccountRepository _accountRepository;

    private const string SALT = "20010626";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        _accountRepository = new AccountRepository();

    }

    public Result TryRegister(string email, string nickname, string password1)
    {

        AccountSaveData saveData = _accountRepository.Find(email);
        if (saveData != null)
        {
            return new Result(false, "�̹� ������ �̸����Դϴ�.");
        }

        string encryptedPassword = CryptoUtil.Encryption(password1, SALT);
        Account account = new Account(email, nickname, encryptedPassword);
        _accountRepository.Save(account.ToDTO());
        return new Result(true);
    }


    public bool TryLogin(string email, string password)
    {
        AccountSaveData saveData = _accountRepository.Find(email);

        if (saveData == null)
        {
            return false; // �������� �ʴ� �����̸�(�̸����� Ʋ����) false ��ȯ
        }
        if (!CryptoUtil.Verify(password, saveData.Password, SALT))
        {
            return false; // ��й�ȣ�� Ʋ���� false ��ȯ
        }
        _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
        return true;
    }
}
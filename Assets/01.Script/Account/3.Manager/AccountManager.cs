using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    private Account _myAccount;
    private AccountRepository _accountRepository;

    private const string PREFIX = "ID_";
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

    public bool TryRegister(string email, string nickname, string password1, string password2)
    {

        AccountSaveData saveData = _accountRepository.Find(email);
        if (saveData != null)
        {
            return false; // �̹� �����ϴ� �����̸� false ��ȯ
        }

        if (password1 != password2)
        {
            return false; // ��й�ȣ�� ��ġ���� ������ false ��ȯ
        }

        // ��й�ȣ ��Ģ ����


        string encryptedPassword = CryptoUtil.Encryption(password1, SALT);
        Account account = new Account(email, nickname, encryptedPassword);
        _accountRepository.Save(account.ToDTO());
        return true;
    }


    public bool TryLogin(string email, string password)
    {
        AccountSaveData saveData = _accountRepository.Find(email);

        if (saveData == null)
        {
            return false; // �������� �ʴ� �����̸�(�̸����� Ʋ����) false ��ȯ
        }
        if (CryptoUtil.Verify(password, saveData.Password, SALT))
        {
            return false; // ��й�ȣ�� Ʋ���� false ��ȯ
        }
        _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
        return true;
    }
}
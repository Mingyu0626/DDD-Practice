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
            return false; // 이미 존재하는 계정이면 false 반환
        }

        if (password1 != password2)
        {
            return false; // 비밀번호가 일치하지 않으면 false 반환
        }

        // 비밀번호 규칙 검증


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
            return false; // 존재하지 않는 계정이면(이메일이 틀리면) false 반환
        }
        if (CryptoUtil.Verify(password, saveData.Password, SALT))
        {
            return false; // 비밀번호가 틀리면 false 반환
        }
        _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
        return true;
    }
}
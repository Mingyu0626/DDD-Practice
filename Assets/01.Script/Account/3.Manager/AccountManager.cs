using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    private Account _myAccount;

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
    }

    public bool TryRegister(string email, string nickname, string password)
    {







        return false;
    }


    public bool TryLogin(string email, string password)
    {
        return false;
    }
}

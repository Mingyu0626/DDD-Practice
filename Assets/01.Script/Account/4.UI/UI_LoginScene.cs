using System;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText;  // ��� �ؽ�Ʈ
    public TMP_InputField IDInputField;
    public TMP_InputField NicknameInputField; // ȸ�����Կ� �г��� �Է� �ʵ�
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordComfirmInputField;
    public Button ConfirmButton;   // �α��� or ȸ������ ��ư
}

public class UI_LoginScene : MonoBehaviour
{
    [Header("�г�")]
    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    [Header("�α���")]
    public UI_InputFields LoginInputFields;

    [Header("ȸ������")]
    public UI_InputFields RegisterInputFields;

    private const string PREFIX = "ID_";
    private const string SALT = "10043420";



    // ���� �����ϸ� �α��� ���ְ� ȸ�������� ���ְ�..
    private void Start()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        LoginInputFields.ResultText.text = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;

        LoginCheck();
    }

    // ȸ�������ϱ� ��ư Ŭ��
    public void OnClickGoToResisterButton()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }


    // ȸ������
    public void Register()
    {
        string email = RegisterInputFields.IDInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            RegisterInputFields.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }


        string nickname = RegisterInputFields.NicknameInputField.text;

        string password1 = RegisterInputFields.PasswordInputField.text;

        string password2 = RegisterInputFields.PasswordComfirmInputField.text;

        if (AccountManager.Instance.TryRegister(email, nickname, password1, password2))
        {

            // 5. �α��� â���� ���ư���.
            // (�̶� ���̵�� �ڵ� �ԷµǾ� �ִ�.)
            OnClickGoToLoginButton();
        }
    }

    public string Encryption(string text)
    {
        // �ؽ� ��ȣȭ �˰��� �ν��Ͻ��� �����Ѵ�.
        SHA256 sha256 = SHA256.Create();

        // �ü�� Ȥ�� ���α׷��� ���� string ǥ���ϴ� ����� �� �ٸ��Ƿ�
        // UTF8 ���� ����Ʈ�� �迭�� �ٲ���Ѵ�.
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        byte[] hash = sha256.ComputeHash(bytes);

        string resultText = string.Empty;
        foreach (byte b in hash)
        {
            // byte�� �ٽ� string���� �ٲ㼭 �̾���̱�
            resultText += b.ToString("X2");
        }

        return resultText;
    }


    public void Login()
    {
        string email = LoginInputFields.IDInputField.text;
        string password = LoginInputFields.PasswordInputField.text;

        if (AccountManager.Instance.TryLogin(email, password))
        {
            Debug.Log("�α��� ����!");
            SceneManager.LoadScene(1);
        }
    }


    // ���̵�� ��й�ȣ InputField ���� �ٲ���� ��쿡��
    public void LoginCheck()
    {
        string id = LoginInputFields.IDInputField.text;
        string password = LoginInputFields.PasswordInputField.text;

        LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password);
    }

}
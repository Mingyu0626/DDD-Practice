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
        // LoginCheck();
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
            Debug.Log(emailSpecification.ErrorMessage);
            return;
        }


        string nickname = RegisterInputFields.NicknameInputField.text;
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            RegisterInputFields.ResultText.text = nicknameSpecification.ErrorMessage;
            Debug.Log(nicknameSpecification.ErrorMessage);
            return;
        }

        string password1 = RegisterInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password1))
        {
            RegisterInputFields.ResultText.text = passwordSpecification.ErrorMessage;
            Debug.Log(passwordSpecification.ErrorMessage);
            return;
        }

        string password2 = RegisterInputFields.PasswordComfirmInputField.text;
        if (!passwordSpecification.IsSatisfiedBy(password2))
        {
            RegisterInputFields.ResultText.text = passwordSpecification.ErrorMessage;
            Debug.Log(passwordSpecification.ErrorMessage);
            return;
        }

        if (password1 != password2)
        {
            RegisterInputFields.ResultText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            Debug.Log("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        Result result = AccountManager.Instance.TryRegister(email, nickname, password1);
        if (result.IsSuccess)
        {
            // 5. �α��� â���� ���ư���.
            // (�̶� ���̵�� �ڵ� �ԷµǾ� �ִ�.)
            OnClickGoToLoginButton();
        }
        else
        {
            RegisterInputFields.ResultText.text = result.Message;
        }
    }

    public void Login()
    {
        string email = LoginInputFields.IDInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            LoginInputFields.ResultText.text = emailSpecification.ErrorMessage;
            Debug.Log(emailSpecification.ErrorMessage);
            return;
        }

        string password = LoginInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            LoginInputFields.ResultText.text = passwordSpecification.ErrorMessage;
            Debug.Log(passwordSpecification.ErrorMessage);
            return;
        }

        if (AccountManager.Instance.TryLogin(email, password))
        {
            Debug.Log("�α��� ����!");
            SceneManager.LoadScene(1);
        }
        else
        {
            LoginInputFields.ResultText.text = "�̸��� �Ǵ� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
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
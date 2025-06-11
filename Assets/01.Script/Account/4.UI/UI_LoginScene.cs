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
    public TextMeshProUGUI ResultText;  // 결과 텍스트
    public TMP_InputField IDInputField;
    public TMP_InputField NicknameInputField; // 회원가입용 닉네임 입력 필드
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordComfirmInputField;
    public Button ConfirmButton;   // 로그인 or 회원가입 버튼
}

public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    [Header("로그인")]
    public UI_InputFields LoginInputFields;

    [Header("회원가입")]
    public UI_InputFields RegisterInputFields;

    private const string PREFIX = "ID_";
    private const string SALT = "10043420";



    // 게임 시작하면 로그인 켜주고 회원가입은 꺼주고..
    private void Start()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        LoginInputFields.ResultText.text = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;
        // LoginCheck();
    }

    // 회원가입하기 버튼 클릭
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


    // 회원가입
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
            RegisterInputFields.ResultText.text = "비밀번호가 일치하지 않습니다.";
            Debug.Log("비밀번호가 일치하지 않습니다.");
            return;
        }

        Result result = AccountManager.Instance.TryRegister(email, nickname, password1);
        if (result.IsSuccess)
        {
            // 5. 로그인 창으로 돌아간다.
            // (이때 아이디는 자동 입력되어 있다.)
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
            Debug.Log("로그인 성공!");
            SceneManager.LoadScene(1);
        }
        else
        {
            LoginInputFields.ResultText.text = "이메일 또는 비밀번호가 일치하지 않습니다.";
        }
    }


    // 아이디와 비밀번호 InputField 값이 바뀌었을 경우에만
    public void LoginCheck()
    {
        string id = LoginInputFields.IDInputField.text;
        string password = LoginInputFields.PasswordInputField.text;

        LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password);
    }

}
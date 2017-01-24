using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extension
{
    public static void clear(this InputField inputfield)
    {
        inputfield.Select();
        inputfield.text = "";
    }
}
public class Menus : MonoBehaviour {
    public Canvas StartScreen;
    public Canvas LoginMenu;
    public Canvas CreateAccount;
    public Canvas MainMenu;
    public Canvas OptionCanvas;

    //private variables
    public InputField EnterUsername;
    public InputField EnterPassword;
    public InputField CreateUsername;
    public InputField CreatePassword;
    public InputField ConfirmPassword;

    //private variables
    private string Username = "";
    private string Password = "";
    private string CreateAccountUrl = "http://giramdev.000webhostapp.com/CreateAccountT.php";
    private string LoginUrl = "http://giramdev.000webhostapp.com/LoginAccountT.php";
    private string ConfirmPass = "";
    private string CPassword = "";
    private string CUsername = "";

    private void Awake()
    {
        if (StartScreen == null)
            Debug.Log("Could not initialize StartScreen.");
        if (LoginMenu == null)
            Debug.Log("Could not initialize LoginMenu.");
        if (CreateAccount == null)
            Debug.Log("Could not initialize CreateAccount.");
        if (MainMenu == null)
            Debug.Log("Could not initialize MainMenu.");
        if (OptionCanvas == null)
            Debug.Log("Could not initialize OptionCanvas.");
    }

    private void SetMenu(Canvas menu)
    {
        StartScreen.gameObject.SetActive(menu == StartScreen);
        LoginMenu.gameObject.SetActive(menu == LoginMenu);
        CreateAccount.gameObject.SetActive(menu == CreateAccount);
        MainMenu.gameObject.SetActive(menu == MainMenu);
        OptionCanvas.gameObject.SetActive(menu == OptionCanvas);
    }

    void Start()
    {
        SetMenu(StartScreen);
    }
    
    public void StartScreenOn()
    {
        SetMenu(StartScreen);
    }

    public void LoginOn()
    {
        EnterUsername.clear();
        EnterPassword.clear();
        SetMenu(LoginMenu);
    }

    public void CreateOn()
    {
        SetMenu(CreateAccount);
    }

    public void MainOn()
    {
        SetMenu(MainMenu);
    }

    public void OptionsOn()
    {
        SetMenu(OptionCanvas);
    }

    public void CreateNewAcount()
    {
        if (CreateUsername != null && CreatePassword != null && ConfirmPassword != null)
        {
            CUsername = CreateUsername.text;
            CPassword = CreatePassword.text;
            ConfirmPass = ConfirmPassword.text;
            StartCoroutine(CreateNewAccount());
        }
    }

    public void LoginToAccount()
    {
        if (EnterUsername != null && EnterPassword != null)
        {
            Username = EnterUsername.text;
            Password = EnterPassword.text;
            StartCoroutine(LoginAccount());
        }
    }

    IEnumerator CreateNewAccount()
    {
        Debug.Log("button pressed");
        WWWForm Form = new WWWForm();
        Form.AddField("Username", CUsername);
        Form.AddField("Password", CPassword);
        //Form.AddField("RePassword", ConfirmPass);
        WWW CreateAccountWWW = new WWW(CreateAccountUrl, Form);
        yield return CreateAccountWWW; //wait for php

        if (CreateAccountWWW.error != null)
        {
            Debug.LogError("Cannot connect to account Creation");
        }
        else
        {
            Debug.Log(CreateAccountWWW.text);
            string CreateAccountReturn = CreateAccountWWW.text;
            Debug.Log(CreateAccountReturn);
            if (CreateAccountReturn == "Success")
            {
                Debug.Log("Success: Account created");
                LoginOn();
            }
        }
    }

    IEnumerator LoginAccount()
    {
        WWWForm Form = new WWWForm();
        Form.AddField("Username", Username);
        Form.AddField("Password", Password);
        WWW LoginAccountWWW = new WWW(LoginUrl, Form);
        yield return LoginAccountWWW;
        if (LoginAccountWWW.error != null)
        {
            Debug.LogError("Cannot connect to Login");
        }
        else
        {
            string LogText = LoginAccountWWW.text;
            Debug.Log(LogText);
            string[] LogTextSplit = LogText.Split(':');
            if (LogTextSplit[0] == "0")
            {
                if (LogTextSplit[1] == "Success")
                {
                    MainOn();
                }
            }
        }
    }
}

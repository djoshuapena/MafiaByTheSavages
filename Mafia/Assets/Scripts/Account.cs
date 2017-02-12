using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region clear_extension
public static class Extension
{
    public static void clear(this InputField inputfield)
    {
        inputfield.Select();
        inputfield.text = "";
    }
}
#endregion

public class Account : MonoBehaviour {

    // public script set in inspector.
    public Menus changeMenu;

    //public variables
    public InputField EnterUsername;
    public InputField EnterPassword;
    public InputField CreateUsername;
    public InputField CreatePassword;
    public InputField ConfirmPassword;

    //private variables
    private string username;
    private string password;
    private string confirmPass;
    private string cPassword;
    private string cUsername;

    // URL from web domian to connect to database.
    private string CreateAccountUrl = "http://giramdev.000webhostapp.com/CreateAccountT.php";

    /// <summary>
    /// Set username from Username Input Field.
    /// </summary>
    public void UserName()
    {
        username = EnterUsername.text.ToString();
    }

    /// <summary>
    /// Set password from Password Input Field.
    /// </summary>
    public void Password()
    {
        password = EnterPassword.text.ToString();
    }

    /// <summary>
    /// Getter for the username.
    /// </summary>
    /// <returns>Username</returns>
    public string GetUsername()
    {
        return username;
    }

    /// <summary>
    /// Getter for the password.
    /// </summary>
    /// <returns>Password</returns>
    public string GetPassword()
    {
        return password;
    }

    /// <summary>
    /// Set cUsername from create username input field.
    /// </summary>
    public void NewUserName()
    {
        cUsername = CreateUsername.text.ToString();
    }

    /// <summary>
    /// Set cPassword from create password input field.
    /// </summary>
    public void NewPassword()
    {
        cPassword = CreatePassword.text.ToString();
    }

    /// <summary>
    /// set confirmPass from confirm password input field.
    /// </summary>
    public void ConfirmNewPassword()
    {
        confirmPass = ConfirmPassword.text.ToString();
    }

    /// <summary>
    /// Clear all input fields.
    /// </summary>
    public void clearAll()
    {
        EnterUsername.clear();
        EnterPassword.clear();
        CreateUsername.clear();
        CreatePassword.clear();
        ConfirmPassword.clear();
    }

    /// <summary>
    /// Create a user account.
    /// </summary>
    public void createAccount()
    {
        StartCoroutine(CreateNewAccount());
    }
   
    /// <summary>
    /// Create a new account using user input username,
    /// password, and confirmed password, and store it
    /// in the database.
    /// </summary>
    /// <returns>Connection to php script.</returns>
    public IEnumerator CreateNewAccount()
    {
        Debug.Log("button pressed");
        WWWForm Form = new WWWForm();
        Form.AddField("Username", cUsername);
        Form.AddField("Password", cPassword);
        Form.AddField("RePassword", confirmPass);
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
                changeMenu.LoginOn();
            }
        }
    }

    #region code_graveyard
    /*
    ==========================================================================================
                            Code Graveyard
    ==========================================================================================


        //take you to the login menu
        public void LoginOn()
        {
            //clear the username and password text field
            EnterUsername.clear();
            EnterPassword.clear();
            menu.callSetMenu("LoginMenu");
        }

        //take you to the create account page
        public void CreateOn()
        {
            //clear the username, password and confirm password text field
            CreateUsername.clear();
            CreatePassword.clear();
            ConfirmPassword.clear();
            menu.callSetMenu("CreateAccount");
        }

    //take you to the main menu if you are connected to the network
    IEnumerator Connection()
    {
        while (!PhotonNetwork.connected)
        {
            yield return null;
        }
        //MainOn();
        menu.callSetMenu("MainMenu");
    }

    */
    #endregion
}

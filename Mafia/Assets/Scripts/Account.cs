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
public class Account : MonoBehaviour {

    //creates an instance of the class menus.cs
    //private Menus menu;

    //private variables
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

    private string CreateAccountUrl = "http://giramdev.000webhostapp.com/CreateAccountT.php";


    //get the username from the input field from the login 
    public void UserName()
    {
        username = EnterUsername.text.ToString();
        //var someVariable : int;
        //objectWithOtherScript.GetComponent.< script1 >().someVariable = someNumber;
        Menus.usernamestats = username;
    }

    //get the password from the input field from the login
    public void Password()
    {
        password = EnterPassword.text.ToString();
    }

    //get the username from the input field from the create account
    public void NewUserName()
    {
        cUsername = CreateUsername.text.ToString();
    }

    //get the password from the input field from the create account
    public void NewPassword()
    {
        cPassword = CreatePassword.text.ToString();
    }

    //get the confirm password from the input field from the create account
    public void ConfirmNewPassword()
    {
        confirmPass = ConfirmPassword.text.ToString();
    }

    //clears all the text fields associated with create account and login
    public void clearAll()
    {
        EnterUsername.clear();
        EnterPassword.clear();
        CreateUsername.clear();
        CreatePassword.clear();
        ConfirmPassword.clear();
    }

    //connect to the photon network when you attempt to login
    public void AttemptConnection()
    {
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
        PhotonNetwork.AuthValues.AddAuthParameter("Password", password);
        PhotonNetwork.AuthValues.AddAuthParameter("Username", username);
        PhotonNetwork.ConnectUsingSettings("Version 0.1");
        //StartCoroutine(Connection());
    }

    //starts the IEnumerator CreateNewAccount()
    public void createAccount()
    {
        StartCoroutine(CreateNewAccount());
    }

   
    //create the account this enumerator will take all the information fromt the input fields
    //and connect to the php
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
                //LoginOn();
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
}

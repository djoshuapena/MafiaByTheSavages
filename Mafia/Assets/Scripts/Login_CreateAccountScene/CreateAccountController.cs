using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAccountController : MonoBehaviour {

    public LoginCanvasProcess menu;
    public CreateAccount info;
    private string CreateAccountUrl = "mafiasav.com/CreateAccountT.php";

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
        Form.AddField("Username", info.NewUsername);
        Form.AddField("Password", info.NewPassword);
        Form.AddField("RePassword", info.ConfirmNewPassword);
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
                menu.SetLoginMenu();
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

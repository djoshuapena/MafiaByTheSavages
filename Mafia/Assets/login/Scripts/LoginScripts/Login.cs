using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {
#region Variables
	//static variables
	public static string Username = "";
	public static string Password = "";

	//public variables
	public string CurrentMenu = "Login";

	//private variables
	private string CreateAccountUrl = "http://giramdev.000webhostapp.com/CreateAccountT.php";
	private string LoginUrl = "http://giramdev.000webhostapp.com/LoginAccountT.php";
	private string ConfirmPass = "";
	private string CPassword = "";
	private string CUsername = "";

	//Gui Test Section
	public float X;
	public float Y;
	public float Width;
	public float Height;
#endregion

	// Use this for initialization
	void Start () {
		
	}//end start

	//Main Gui Function
	void OnGUI(){
		// If our current menu is = login, then display the loign menu by calling our loingui function
		// else display the create account gui by calling its function
		if (CurrentMenu == "Login") {
			//call login gui.
			LoginGUI ();
		} else if (CurrentMenu == "CreateAccount") {
			CreateAccountGUI ();
		}
	}//end OnGUI

#region Custom methods
	//This method will login the accounts
	void LoginGUI() {
		//Create box to simulate window
		GUI.Box (new Rect (256, 2, (Screen.width / 4) + 100, (Screen.height / 4) + 221), "Login");
		//Create account button and login button
		if (GUI.Button (new Rect (276, 210, 108, 26), "Create Account")) {
			CurrentMenu = "CreateAccount";
		}
		if (GUI.Button (new Rect (400, 210, 108, 26), "Log In")) {
			StartCoroutine(LoginAccount());
		}//End buttons

		GUI.Label (new Rect (330, 55, 138, 22), "Username:");
		Username = GUI.TextField(new Rect(328, 74, 138, 22), Username);

		GUI.Label (new Rect (330, 115, 138, 22), "Password:");
		Password = GUI.TextField(new Rect(328, 134, 138, 22), Password);
	
	}//end loginGUI

	//This method will be the gui for creating the account.
	void CreateAccountGUI(){
		//Create box to simulate window
		GUI.Box (new Rect (256, 2, (Screen.width / 4) + 100, (Screen.height / 4) + 221), "Creat Account");

		GUI.Label (new Rect (330, 70, 138, 22), "Username:");
		CUsername = GUI.TextField(new Rect(328, 89, 138, 22), CUsername);

		GUI.Label (new Rect (330, 130, 138, 22), "Password:");
		CPassword = GUI.TextField(new Rect(328, 149, 138, 22), CPassword);

		GUI.Label (new Rect (330, 190, 138, 22), "Confirm Password:");
		ConfirmPass = GUI.TextField(new Rect(328, 209, 138, 22), ConfirmPass);


		//Create account button and login button
		if (GUI.Button (new Rect (276, 265, 108, 26), "Create Account")) {
			if (CPassword == ConfirmPass) {
				StartCoroutine ("CreateAccount");
			} else {
				//StartCoroutine ("LoginAccount");			
			}
		}
		if (GUI.Button (new Rect (400, 265, 108, 26), "Back")) {
			CurrentMenu = "Login";
		}//End buttons


	}//End CA GUI

#endregion

#region CoRoutinea
	//Php comm
	IEnumerator CreateAccount(){
		Debug.Log ("button pressed");
		WWWForm Form = new WWWForm ();
		Form.AddField ("Username", CUsername);
		Form.AddField ("Password", CPassword);
		WWW CreateAccountWWW = new WWW (CreateAccountUrl, Form);
		yield return CreateAccountWWW; //wait for php

		if (CreateAccountWWW.error != null) {
			Debug.LogError ("Cannot connect to account Creation");
		} else {
			Debug.Log (CreateAccountWWW.text);
			string CreateAccountReturn = CreateAccountWWW.text;
			if (CreateAccountReturn == "Success") {
				Debug.Log ("Success: Account created");
				CurrentMenu = "Login";
			}
		}
	}

	IEnumerator LoginAccount(){
		WWWForm Form = new WWWForm();
		Form.AddField ("Username", Username);
		Form.AddField ("Password", Password);
		WWW LoginAccountWWW = new WWW(LoginUrl, Form);
		yield return LoginAccountWWW;
		if (LoginAccountWWW.error != null) {
			Debug.LogError("Cannot connect to Login");
		} else {
			string LogText = LoginAccountWWW.text;
			Debug.Log(LogText);
			string[] LogTextSplit = LogText.Split (':');
            if (LogTextSplit[0] == "0")
            {
                if (LogTextSplit[1] == "Success")
                {
                    SceneManager.LoadScene(1);

                }
            }
			//} else {
			//	if (LogTextSplit[0] == "Wrong Password/Username") {
			//		Application.LoadLevel("CharacterSelection");
			//	}
			//}
		}
	}


#endregion

}//end class

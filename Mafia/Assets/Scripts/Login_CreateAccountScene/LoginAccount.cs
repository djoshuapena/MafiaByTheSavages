using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginAccount : MonoBehaviour {

    public InputField enterUsername;
    public InputField enterPassword;

    public string Username
    {
        get { return enterUsername.text.ToString(); }
    }

    public string Password
    {
        get { return enterPassword.text.ToString(); }
    }

    public void OnLoginButton()
    {
        Debug.Log(Username);
        Debug.Log(Password);
    }

    // Use this for initialization
    void Start () {
        enterUsername.clear();
        enterPassword.clear();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

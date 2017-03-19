using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutAccount : MonoBehaviour {

    private string LogoutAccountUrl = "mafiasav.com/logout.php";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //run the php logout code and logout of photon
    public void onLogout()
    {
        StartCoroutine(logoutAccount());
    }

    /// <summary>
    /// Create a new account using user input username,
    /// password, and confirmed password, and store it
    /// in the database.
    /// </summary>
    /// <returns>Connection to php script.</returns>
    public IEnumerator logoutAccount()
    {
        Debug.Log("button pressed");
        WWWForm Form = new WWWForm();
        Form.AddField("Username", PhotonNetwork.playerName);
        WWW LogoutAccountWWW = new WWW(LogoutAccountUrl, Form);
        yield return LogoutAccountWWW; //wait for php

        if (LogoutAccountWWW.error != null)
        {
            Debug.LogError("Cannot connect to account logout");
        }
        else
        {
            Debug.Log(LogoutAccountWWW.text);
            string LogoutAccountReturn = LogoutAccountWWW.text;
            Debug.Log(LogoutAccountReturn);
                Debug.Log("Success: Logged out");
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("Login_CreateAccount");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginCanvasProcess : MonoBehaviour {

    public LoginCanvasController setCanvas;
    //public LogoutAccount logout;
    //int count = 0;

    // Use this for initialization
    void Start () {
        SetLoginMenu();
	}

    public void SetLoginMenu()
    {
        setCanvas.SetMenu("LoginMenu");
    }

    public void SetCreateAccountMenu()
    {
        setCanvas.SetMenu("CreateAccountMenu");
    }
	
	/*// Update is called once per frame
	void Update () {
        if (count == 1)
        {
            count = 0;
        }
        else
        {
            count = 1;
        }
    }

    void OnApplicationQuit()
    {
        //LogoutAccount logout = new LogoutAccount();
        logout.onLogout();
    }*/
}

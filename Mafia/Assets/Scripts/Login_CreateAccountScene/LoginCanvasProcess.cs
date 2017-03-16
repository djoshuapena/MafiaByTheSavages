using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginCanvasProcess : MonoBehaviour {

    public LoginCanvasController setCanvas;

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
	
	// Update is called once per frame
	void Update () {
		
	}
}

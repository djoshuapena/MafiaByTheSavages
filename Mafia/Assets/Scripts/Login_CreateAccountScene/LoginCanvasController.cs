using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginCanvasController : MonoBehaviour {

    public Canvas LoginMenu;
    public Canvas CreateAccountMenu;

    private void Awake()
    {
        if (LoginMenu == null)
            Debug.Log("Could not initialize LoginMenu.");
        if (CreateAccountMenu == null)
            Debug.Log("Could not initialize CreateAccountMenu.");
    }

    public void SetMenu(string menu)
    {
        LoginMenu.gameObject.SetActive(menu == "LoginMenu");
        CreateAccountMenu.gameObject.SetActive(menu == "CreateAccountMenu");
    }

        // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

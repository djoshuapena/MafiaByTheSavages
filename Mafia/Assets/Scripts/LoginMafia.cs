using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour {

    //static variables
    public static string Username = "";
    public static string Password = "";

    //private variables
    private string CreateAccountUrl = "http://giramdev.000webhostapp.com/CreateAccountT.php";
    private string LoginUrl = "http://giramdev.000webhostapp.com/LoginAccountT.php";
    private string ConfirmPass = "";
    private string CPassword = "";
    private string CUsername = "";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

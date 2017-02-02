using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Extension
{
    public static void clear(this InputField inputfield)
    {
        inputfield.Select();
        inputfield.text = "";
    }
}
public class Menus : MonoBehaviour {
    public Canvas StartScreen;
    public Canvas LoginMenu;
    public Canvas CreateAccount;
    public Canvas MainMenu;
    public Canvas OptionCanvas;
	public Canvas Stats;
    public Canvas JoinGameCanvas;

    //private variables
    public InputField EnterUsername;
    public InputField EnterPassword;
    public InputField CreateUsername;
    public InputField CreatePassword;
    public InputField ConfirmPassword;
	//stat stuff
	public string[] items;
	public getdata data; //link to getdata.cs
	public static string usernamestats;

    //private variables
    private string username;
    private string password;
    private string confirmPass;
    private string cPassword;
    private string cUsername;
    private string CreateAccountUrl = "http://giramdev.000webhostapp.com/CreateAccountT.php";
	private string UpdateStatsUrl = "http://giramdev.000webhostapp.com/updateStats.php";
    //private string LoginUrl = "http://giramdev.000webhostapp.com/LoginAccountT.php";
	private int[] stats = new int[13];

    private void Awake()
    {
        if (StartScreen == null)
            Debug.Log("Could not initialize StartScreen.");
        if (LoginMenu == null)
            Debug.Log("Could not initialize LoginMenu.");
        if (CreateAccount == null)
            Debug.Log("Could not initialize CreateAccount.");
        if (MainMenu == null)
            Debug.Log("Could not initialize MainMenu.");
        if (OptionCanvas == null)
            Debug.Log("Could not initialize OptionCanvas.");
		if (Stats == null)
			Debug.Log("Could not initialize Stats.");
        if (JoinGameCanvas == null)
            Debug.Log("Could not initialize JoinGameCanvas.");
    }

    private void SetMenu(Canvas menu)
    {
        StartScreen.gameObject.SetActive(menu == StartScreen);
        LoginMenu.gameObject.SetActive(menu == LoginMenu);
        CreateAccount.gameObject.SetActive(menu == CreateAccount);
        MainMenu.gameObject.SetActive(menu == MainMenu);
        OptionCanvas.gameObject.SetActive(menu == OptionCanvas);
		Stats.gameObject.SetActive(menu == Stats);
        JoinGameCanvas.gameObject.SetActive(menu == JoinGameCanvas);
    }

    void Start()
    {
        //Application.runInBackground = true;
        //Connect();
        SetMenu(StartScreen);
    }

    //private void Connect()
    //{
    //    PhotonNetwork.ConnectUsingSettings("Version 0.01");
    //}

    public void StartScreenOn()
    {
        SetMenu(StartScreen);
    }

    public void LoginOn()
    {
        EnterUsername.clear();
        EnterPassword.clear();
        SetMenu(LoginMenu);
    }

    public void CreateOn()
    {
        CreateUsername.clear();
        CreatePassword.clear();
        ConfirmPassword.clear();
        SetMenu(CreateAccount);
    }

    public void MainOn()
    {
        SetMenu(MainMenu);
    }

    public void MainOnandConnect()
    {
        Debug.Log(PhotonNetwork.connected);
        SetMenu(MainMenu);
    }

    void OnJoinedLobby()
    {
        Debug.Log("We did it!");
    }

    public void OptionsOn()
    {
        SetMenu(OptionCanvas);
    }

	public void StatsOn()
	{
		StartCoroutine(UserStat());
	}

    public void JoinMenuOn()
    {
        //SetMenu(JoinGameCanvas);
        SceneManager.LoadScene("Lobby");
    }

    public void UserName()
    {
        username = EnterUsername.text.ToString();
		usernamestats = username;
    }

    public void Password()
    {
        password = EnterPassword.text.ToString();
    }

    public void NewUserName()
    {
        cUsername = CreateUsername.text.ToString();
    }

    public void NewPassword()
    {
        cPassword = CreatePassword.text.ToString();
    }

    public void ConfirmNewPassword()
    {
        confirmPass = ConfirmPassword.text.ToString();
    }

    public void CreateNewAcount()
    {
        StartCoroutine(CreateNewAccount());
    }

	//UpdateStats fills stats with temporary values for testing,
	//converts the stats array into a string to send to php and
	//calls the IEnumerator UpdateStats to post the string to the database.
	public void UpdateStats()
	{
		StartCoroutine(UpdateStat());
	}
		
    public void AttemptConnection()
    {
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
        PhotonNetwork.AuthValues.AddAuthParameter("Password", password);
        PhotonNetwork.AuthValues.AddAuthParameter("Username", username);
        PhotonNetwork.ConnectUsingSettings("Version 0.1");
        StartCoroutine(Connection());
    }

    IEnumerator Connection()
    {
        while (!PhotonNetwork.connected)
        {
            yield return null;
        }
        MainOn();
    }

    private void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    } 

    public void LogoutButton()
    {
        PhotonNetwork.Disconnect();
        StartScreenOn();
    }

    private void OnCustomAuthenticationFailed(string DebugMessage)
    {
        Debug.Log(DebugMessage);
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    //public void LoginToAccount()
    //{
    //    StartCoroutine(LoginAccount());
    //}

	//temporary function to fill stats array
	public void fillStatsArray(ref int[] stats)
	{
		for(int i=0; i<stats.Length; i++)
		{
			stats[i] = i;
		}
	}
		

	//convertToString(stats) takes in an array, converts stats 
	//to a string and then returns it.
	//This creates a string that seperates each number by a comma.
	//Ex: 1,12,5,45,...
	private string convertToString(int[] stats)
	{
		//string[] result = stats.Select(x=>x.ToString()).ToArray();
		string result = String.Join(",", stats.Select(p=>p.ToString()).ToArray());
		return result;
	}

    IEnumerator CreateNewAccount()
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
                LoginOn();
            }
        }
    }

	IEnumerator UserStat() {
		WWWForm Form = new WWWForm();
		Form.AddField("Username", username);
		WWW itemsData = new WWW ("https://giramdev.000webhostapp.com/getstats.php", Form);
		yield return itemsData;
		string itemsDataString = itemsData.text;
//		print (itemsDataString);
		items = itemsDataString.Split ('|');
		//print (GetDataValue (items [0], "TotalGameWin"));
		data.InsertData();
		SetMenu(Stats);
	}

	IEnumerator UpdateStat()
	{
		//fillStatsArray (ref stats);
		//statsString = convertToString (stats);
		WWWForm Form = new WWWForm ();
		string statsString = "1 12 5 6 4 0 25 6 200 54 1 6 18";
		Form.AddField ("Username", usernamestats);
		Form.AddField ("stats", statsString);
		WWW UpdateStatsWWW = new WWW (UpdateStatsUrl, Form);
		yield return UpdateStatsWWW; //wait for php

		if (UpdateStatsWWW.error != null) {
			Debug.LogError ("Cannot connect to account Creation");
		} 
		else {
			Debug.Log (UpdateStatsWWW.text);
			string UpdateStatsReturn = UpdateStatsWWW.text;
			Debug.Log (UpdateStatsReturn);
			if (UpdateStatsReturn == "Success") {
				Debug.Log ("Success: Stats Updated");
				MainOn ();
			}
		}
	}

    //IEnumerator LoginAccount()
    //{
    //    WWWForm Form = new WWWForm();
    //    Form.AddField("Username", username);
    //    Form.AddField("Password", password);
    //    WWW LoginAccountWWW = new WWW(LoginUrl, Form);
    //    yield return LoginAccountWWW;
    //    if (LoginAccountWWW.error != null)
    //    {
    //        Debug.LogError("Cannot connect to Login");
    //    }
    //    else
    //    {
    //        string LogText = LoginAccountWWW.text;
    //        Debug.Log(LogText);
    //        string[] LogTextSplit = LogText.Split(':');
    //        if (LogTextSplit[0] == "0")
    //        {
    //            if (LogTextSplit[1] == "Success")
    //            {
    //                MainOnandConnect();
    //            }
    //        }
    //    }
    //}
}

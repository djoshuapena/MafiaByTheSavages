using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menus : MonoBehaviour {
	//create canvas which you connect to the canvas in unity
    public Canvas StartScreen;
    public Canvas LoginMenu;
    public Canvas CreateAccount;
    public Canvas MainMenu;
    public Canvas OptionCanvas;
	public Canvas Stats;
    public Canvas JoinGameCanvas;

    //link to Account.cs and Stats.cs
    public Account accounts;
    public stats stats;
	public static string usernamestats;


	//make sure the canvas are connected 
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

	//set the canvas
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

    //callSetMenu(menu) takes in a string and set the
    //menu based on the string. This is a public method
    //to allow access to set menus in other scripts.
    public void callSetMenu(string menu)
    {
        if(menu.Equals("StartScreen"))
        {
            SetMenu(StartScreen);
        }
        else if (menu.Equals("LoginMenu"))
        {
            SetMenu(LoginMenu);
        }
        else if (menu.Equals("CreateAccount"))
        {
            SetMenu(CreateAccount);
        }
        else if (menu.Equals("MainMenu"))
        {
            SetMenu(MainMenu);
        }
        else if (menu.Equals("OptionCanvas"))
        {
            SetMenu(OptionCanvas);
        }
        else if (menu.Equals("Stats"))
        {
            SetMenu(Stats);
        }
        else if (menu.Equals("JoinGameCanvas"))
        {
            SetMenu(JoinGameCanvas);
        }
    }

    void Start()
    {
        //Application.runInBackground = true;
        //Connect();
        SetMenu(StartScreen);
    }

	//take you to the start screen
    public void StartScreenOn()
    {
        SetMenu(StartScreen);
    }

	//take you to the login menu
    public void LoginOn()
    {
        //clear the username and password text field
        //EnterUsername.clear();
        //EnterPassword.clear();
        accounts.clearAll();
        SetMenu(LoginMenu);
    }

	//take you to the create account page
    public void CreateOn()
    {
        //clear the username, password and confirm password text field
        //CreateUsername.clear();
        //CreatePassword.clear();
        //ConfirmPassword.clear();
        accounts.clearAll();
        SetMenu(CreateAccount);
    }

	//take you to the main menu
    public void MainOn()
    {
        SetMenu(MainMenu);
    }

	//connect to the photon network and set to the main menu
    public void MainOnandConnect()
    {
        Debug.Log(PhotonNetwork.connected);
        SetMenu(MainMenu);
    }

	//james
    void OnJoinedLobby()
    {
        Debug.Log("We did it!");
    }

	//take you to the options page
    public void OptionsOn()
    {
        SetMenu(OptionCanvas);
    }

	//james
    public void JoinMenuOn()
    {
        //SetMenu(JoinGameCanvas);
        SceneManager.LoadScene("Lobby");
    }

	//start the coroutine for create account
    public void CreateNewAcount()
    {
        accounts.createAccount();
        SetMenu(LoginMenu);
    }

    //start the coroutine for getting stats
    public void StatsOn()
    {
        stats.statOn();
    }

    //start the coroutine for updating stats
    public void UpdateStats()
    {
        stats.updateStat();
    }

	//debug
    private void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        SetMenu(MainMenu);
    } 

	//disconnect from the photon network and go to the start screen
    public void LogoutButton()
    {
        PhotonNetwork.Disconnect();
        StartScreenOn();
    }

	//debug
    private void OnCustomAuthenticationFailed(string DebugMessage)
    {
        Debug.Log(DebugMessage);
    }

	//debug
    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }


    /*
    ==========================================================================================
                            Code Graveyard
    ==========================================================================================

    //private int[] stats = new int[13];
	//public string[] items; //string that will hold your stats
    //private string LoginUrl = "http://giramdev.000webhostapp.com/LoginAccountT.php";
    //public variables
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



    public static class Extension
{
    public static void clear(this InputField inputfield)
    {
        inputfield.Select();
        inputfield.text = "";
    }
}

     public method to send the create account php to other .cs files
    public string getCreateAccountURL()
    {
        return CreateAccountUrl;
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
    
    public void LoginToAccount()
    {
        StartCoroutine(LoginAccount());
    }

    private void Connect()
    {
       PhotonNetwork.ConnectUsingSettings("Version 0.01");
    }

    //connect to the photon network when you attempt to login
    public void AttemptConnection()
    {
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
        PhotonNetwork.AuthValues.AddAuthParameter("Password", password);
        PhotonNetwork.AuthValues.AddAuthParameter("Username", username);
        PhotonNetwork.ConnectUsingSettings("Version 0.1");
        StartCoroutine(Connection());
    }

	//take you to the main menu if you are connected to the network
    IEnumerator Connection()
    {
        while (!PhotonNetwork.connected)
        {
            yield return null;
        }
        MainOn();
    }

    IEnumerator LoginAccount()
    {
        WWWForm Form = new WWWForm();
       Form.AddField("Username", username);
        Form.AddField("Password", password);
        WWW LoginAccountWWW = new WWW(LoginUrl, Form);
        yield return LoginAccountWWW;
        if (LoginAccountWWW.error != null)
        {
            Debug.LogError("Cannot connect to Login");
        }
        else
        {
            string LogText = LoginAccountWWW.text;
            Debug.Log(LogText);
            string[] LogTextSplit = LogText.Split(':');
            if (LogTextSplit[0] == "0")
            {
                if (LogTextSplit[1] == "Success")
                {
                    MainOnandConnect();
                }
            }
        }
    }

    */
}

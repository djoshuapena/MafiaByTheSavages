using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menus : MonoBehaviour {

    public MenuProcess newMenu;

    /// <summary>
    /// Start into the star menu.
    /// </summary>
    void Start()
    {
        newMenu.MenuStart();
    }

	/// <summary>
    /// Turns on the start menu.
    /// </summary>
    public void StartScreenOn()
    {
        newMenu.setNewMenu("StartScreen");
    }

	/// <summary>
    /// Turn on the login account menu.
    /// </summary>
    public void LoginOn()
    {
        newMenu.setNewMenu("LoginMenu");
    }

	/// <summary>
    /// Turn on the create account menu.
    /// </summary>
    public void CreateOn()
    {
        newMenu.setNewMenu("CreateAccount");
    }

	/// <summary>
    /// Turn on the main menu.
    /// </summary>
    public void MainOn()
    {
        newMenu.setNewMenu("MainMenu");
    }

	/// <summary>
    /// Turn on the options menu.
    /// </summary>
    public void OptionsOn()
    {
        newMenu.setNewMenu("OptionCanvas");
    }

    /// <summary>
    /// Turn on the stats menu.
    /// </summary>
    public void StatsOn()
    {
        newMenu.setNewMenu("Stats");
    }

    //***********************************************
    //          THIS NEEDS TO BE MOVED
    //***********************************************
    #region move_this
    public void JoinMenuOn()
    {
        //SetMenu(JoinGameCanvas);
        SceneManager.LoadScene("Lobby");
    }

    //aaron, using this to get back to main menu from other scenes
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //also aaron
    //return to lobby and disconnect from room
    public void BackToRooms()
    {
        SceneManager.LoadScene("Lobby");
        PhotonNetwork.LeaveRoom();
        //PhotonNetwork.JoinLobby();
    }
    #endregion

    #region code_graveyard

    //debug
    //private void OnConnectedToMaster()
    //{
    //    Debug.Log("Connected To Master");
    //    SetMenu(MainMenu);
    //} 

    //disconnect from the photon network and go to the start screen
    //public void LogoutButton()
    //{
    //    PhotonNetwork.Disconnect();
    //    StartScreenOn();
    //}

    ////debug
    //   private void OnCustomAuthenticationFailed(string DebugMessage)
    //   {
    //       Debug.Log(DebugMessage);
    //   }

    ////debug
    //   private void OnGUI()
    //   {
    //       GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    //   }

    //private int[] stats = new int[13];
    //public string[] items; //string that will hold your stats
    //private string LoginUrl = "http://giramdev.000webhostapp.com/LoginAccountT.php";
    //public variables
//    public InputField EnterUsername;
//    public InputField EnterPassword;
//    public InputField CreateUsername;
//    public InputField CreatePassword;
//    public InputField ConfirmPassword;
//    //private variables
//    private string username;
//    private string password;
//    private string confirmPass;
//    private string cPassword;
//    private string cUsername;



//    public static class Extension
//{
//    public static void clear(this InputField inputfield)
//    {
//        inputfield.Select();
//        inputfield.text = "";
//    }
//}

//     public method to send the create account php to other .cs files
//    public string getCreateAccountURL()
//    {
//        return CreateAccountUrl;
//    }

//	//convertToString(stats) takes in an array, converts stats 
//	//to a string and then returns it.
//	//This creates a string that seperates each number by a comma.
//	//Ex: 1,12,5,45,...
//	private string convertToString(int[] stats)
//	{
//		//string[] result = stats.Select(x=>x.ToString()).ToArray();
//		string result = String.Join(",", stats.Select(p=>p.ToString()).ToArray());
//		return result;
//	}
    
//    public void LoginToAccount()
//    {
//        StartCoroutine(LoginAccount());
//    }

//    private void Connect()
//    {
//       PhotonNetwork.ConnectUsingSettings("Version 0.01");
//    }

//    //connect to the photon network when you attempt to login
//    public void AttemptConnection()
//    {
//        PhotonNetwork.AuthValues = new AuthenticationValues();
//        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
//        PhotonNetwork.AuthValues.AddAuthParameter("Password", password);
//        PhotonNetwork.AuthValues.AddAuthParameter("Username", username);
//        PhotonNetwork.ConnectUsingSettings("Version 0.1");
//        StartCoroutine(Connection());
//    }

//	//take you to the main menu if you are connected to the network
//    IEnumerator Connection()
//    {
//        while (!PhotonNetwork.connected)
//        {
//            yield return null;
//        }
//        MainOn();
//    }

//    IEnumerator LoginAccount()
//    {
//        WWWForm Form = new WWWForm();
//       Form.AddField("Username", username);
//        Form.AddField("Password", password);
//        WWW LoginAccountWWW = new WWW(LoginUrl, Form);
//        yield return LoginAccountWWW;
//        if (LoginAccountWWW.error != null)
//        {
//            Debug.LogError("Cannot connect to Login");
//        }
//        else
//        {
//            string LogText = LoginAccountWWW.text;
//            Debug.Log(LogText);
//            string[] LogTextSplit = LogText.Split(':');
//            if (LogTextSplit[0] == "0")
//            {
//                if (LogTextSplit[1] == "Success")
//                {
//                    MainOnandConnect();
//                }
//            }
//        }
//    }
    #endregion
}

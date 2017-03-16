using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonCustomAuthentication : MonoBehaviour {

    //public scripts set in inspector.
    public LoginAccount accountInfo;
    //public Menus changeMenu;

    /// <summary>
    /// Attempt to connect to Photon Network, using custom authentication.
    /// </summary>
    public void AttemptConnection()
    {
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
        PhotonNetwork.AuthValues.AddAuthParameter("Password", accountInfo.Password);
        PhotonNetwork.AuthValues.AddAuthParameter("Username", accountInfo.Username);
        PhotonNetwork.ConnectUsingSettings("Version 0.1");
        PhotonNetwork.playerName = accountInfo.Username;
    }

    /// <summary>
    /// Change to the Main Menu upon successfully connecting to
    /// master Photon Network.
    /// </summary>
    private void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        SceneManager.LoadScene("MainMenu");
        //changeMenu.MainOn();
    }

    /// <summary>
    /// Display the debug message upon failing custom authentication.
    /// </summary>
    /// <param name="DebugMessage">Message to display to console.</param>
    private void OnCustomAuthenticationFailed(string DebugMessage)
    {
        Debug.Log(DebugMessage);
    }

    /// <summary>
    /// Display the current connection state in Photon.
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}

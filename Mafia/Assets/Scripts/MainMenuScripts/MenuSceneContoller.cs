using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneContoller : MonoBehaviour {

    public void Start()
    {
        //if(!PhotonNetwork.isMasterClient)
        //{
        //    PhotonNetwork.ConnectToMaster(PhotonNetwork.ServerAddress, PhotonNetwork.PhotonServerSettings.ServerPort, PhotonNetwork.PhotonServerSettings.AppID, PhotonNetwork.gameVersion);
        //}
    }
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnProfileButton()
    {
        SceneManager.LoadScene("Stats");
    }

    public void OnOptionsButton()
    {
        SceneManager.LoadScene("Options");
    }

    public void OnLogoutButton()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Login_CreateAccount");
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}

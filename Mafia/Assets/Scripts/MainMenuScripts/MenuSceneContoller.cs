using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneContoller : MonoBehaviour {

    public LogoutAccount logout;

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
        logout.onLogout();
        
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneContoller : MonoBehaviour {

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

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}

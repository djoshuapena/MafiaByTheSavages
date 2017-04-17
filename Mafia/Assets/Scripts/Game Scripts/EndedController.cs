using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndedController : MonoBehaviour
{


    // php script in web domain.
    private string updateStatsUrl = "mafiasav.com/updateStats.php";

    //for when we decide on a data structure instead of my statsString
    //private int[] stats = new int[13];


    /// <summary>
    /// Updates the users stats.
    /// </summary>
    public void ActivateEnd()
    {
        //stats = whatever is sent to this function;
        StartCoroutine(UpdateStats());
       // LoadLobby();
    }

    /// <summary>
    /// Updates the users stats in the stats database.
    /// </summary>
    /// <returns>Data from the php script, when connected.</returns>
	IEnumerator UpdateStats()
    {
        //to be used if we decide to use an array for stats
        //string statsString = convertToString (stats);
        WWWForm Form = new WWWForm();
        //string statsString = "1 12 5 6 4 0 25 6 200 54 1 6 18";
        string statsString = "17 12 25 6 42 10 2 6 54 54 1 6 18";
        Form.AddField("Username", name);
        Form.AddField("stats", statsString);
        WWW UpdateStatsWWW = new WWW(updateStatsUrl, Form);
        yield return UpdateStatsWWW; //wait for php

        if (UpdateStatsWWW.error != null)
        {
            Debug.LogError("Cannot connect to account Creation");
        }
        else
        {
            Debug.Log(UpdateStatsWWW.text);
            string UpdateStatsReturn = UpdateStatsWWW.text;
            Debug.Log(UpdateStatsReturn);
            if (UpdateStatsReturn == "Success")
            {
                Debug.Log("Success: Stats Updated");
            }
        }
        LoadLobby();
    }

    /// <summary>
    /// Loads the lobby after updating to the database
    /// </summary>
    private void LoadLobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    //join lobby scene, connection status: JoinedLobby
    //private void OnConnectedToMaster()
    // {
    //   PhotonNetwork.JoinLobby();
    //PhotonNetwork.LoadLevel("Lobby");
    // }

    void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
        PhotonNetwork.JoinLobby();
    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTester : MonoBehaviour
{

    public EndGameController Control;
    public AssignRoles1 ILoveKittens;
    public Text outputText;


    public void onMafiaButtonPress()
    {
        //PhotonPlayer[] playerList = PhotonNetwork.playerList;//list of players in game
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add("dead", true);

        for (int x = 0; x < PhotonNetwork.playerList.Length; x++)
        {
            Debug.Log("Did I come here");
            Debug.Log(PhotonNetwork.playerList[x].CustomProperties["roles"].Equals("Mafia"));
            if (PhotonNetwork.playerList[x].CustomProperties["roles"].Equals("Mafia"))
            {
                Debug.Log("I came here");
                PhotonNetwork.playerList[x].SetCustomProperties(hash);
            }
        }
        if (Control.MafiaAlive())
        {
            outputText.text = "A mafia is left";
        }
        else
        {
            outputText.text = "All mafia are dead";
        }
    }

    public void onSheriffButtonPress()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;//list of players in game
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add("dead", true);

        for (int x = 0; x < PhotonNetwork.playerList.Length; x++)
        {
            if (PhotonNetwork.playerList[x].CustomProperties["roles"].Equals("Sheriff"))
            {
                PhotonNetwork.playerList[x].SetCustomProperties(hash);
            }
        }
        if (Control.SheriffAlive())
        {
            outputText.text = "A sheriff is left";
        }
        else
        {
            outputText.text = "All sheriffs are dead";
        }
    }

    public void onCivlianButtonPress()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;//list of players in game
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add("dead", true);

        for (int x = 0; x < PhotonNetwork.playerList.Length; x++)
        {
            if (PhotonNetwork.playerList[x].CustomProperties["roles"].Equals("Healer"))
            {
                PhotonNetwork.playerList[x].SetCustomProperties(hash);
            }
            else if (PhotonNetwork.playerList[x].CustomProperties["roles"].Equals("Civilian"))
            {
                PhotonNetwork.playerList[x].SetCustomProperties(hash);
            }
            else if (PhotonNetwork.playerList[x].CustomProperties["roles"].Equals("Sheriff"))
            {
                PhotonNetwork.playerList[x].SetCustomProperties(hash);
            }
        }
        if (Control.CivilianAlive())
        {
            outputText.text = "A civilian is left";
        }
        else
        {
            outputText.text = "All civilian are dead";
        }
    }

        // Use this for initialization
        void Start()
    {
        ILoveKittens.InitializeRoles();

        /*PhotonPlayer sheriff = new PhotonPlayer(true, 1, "Bob");
        sheriff.SetCustomProperties("role", "sheriff");
        sheriff = sheriff.SetCustomProperties("dead", false);

        PhotonPlayer mafia = new PhotonPlayer(true, 2, "Jerry");
        mafia.SetCustomProperties("role", "mafia");
        mafia = mafia.SetCustomProperties("dead", true);*/

        //Debug.Log("Sheriff alive");
        //Debug.Log("Mafia dead");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

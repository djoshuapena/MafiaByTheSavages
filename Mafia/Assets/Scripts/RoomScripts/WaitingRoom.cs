﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitingRoom : MonoBehaviour
{
    private List<GameObject> playerNamePrefabs = new List<GameObject>();
    //stores the information about the player
    //public Text playerNameText;
    //public Text playerIdText;
    public GameObject playerNamePrefab;

    //the minimum amount of players that can be in the game
    private int minPlayers = 2;

    //future expansion
    //YouText will say YOU next your name in the list of names
    //MasterText will say master next to the creater of the game in the list of names
    //public Text YouText;
    //public Text MasterText;


    //public GameObject UIList;

    //public GameObject PlayerItemPrefab;

    //public LobbyMenuController lmc; //lobby menu controller


    //Total time the waiting room wil stay open
    float timeLeft = 30.0f;
    public Text timer;

    // Use this for initialization
    void Start()
    {
        //wait 3 seconds and refresh the playerlist
        InvokeRepeating("RefreshPlayerList", 0.1f, 3.0f);

        //update the player count
        //playerCount = playerList();
    }

    // Update is called once per frame
    void Update()
    {
        //timing for the waiting room
        if (PhotonNetwork.playerList.Length > minPlayers)
        {
            timeLeft -= Time.deltaTime;
            timer.text = "Time Left:" + Mathf.Round(timeLeft);
        }
        else
        {
            timeLeft = 30.0f;
            timer.text = "Time Left:" + Mathf.Round(timeLeft);
        }
        if (timeLeft < 0 && PhotonNetwork.playerList.Length >= minPlayers)
        {
            //join the JoinGame Room
            //lmc.JoinGameCanvasOn();
            timer.text = "Time is up";
        }
    }


    //getPlayer will get the users name and users ID
    //public void getPlayer()
    //{
    //    //PhotonNetwork.playerName = PlayerPrefs.GetString ("CurrUser"); //delete later
    //    //get the players name
    //    PlayerNameText.text = PhotonNetwork.playerName;
    //    //get the players ID
    //    //PlayerIdText.text = PhotonNetwork.player;

    //    //Find out which player in the list is you and which player is the Master
    //    //YouText.enabled = player.isLocal;
    //    //MasterText.enabled = player.isMasterClient;
    //}

    ////playerList will go through the list of players and get their names and id
    ////        it will also return the length of the Player List(the number of people in the game)
    //        public int playerList()
    //{
    //    for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
    //    {

    //        getPlayer(_player);

    //}
    //       return PhotonNetwork.playerList.Length;
    //   }

    void RefreshPlayerList()
    {
        if (playerNamePrefabs.Count > 0)
        {
            for (int i = 0; i < playerNamePrefabs.Count; i++)
            {
                Destroy(playerNamePrefabs[i]);
            }

            playerNamePrefabs.Clear();
        }

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            //Debug.Log(PhotonNetwork.playerList[i]);

            GameObject g = Instantiate(playerNamePrefab);
            g.transform.SetParent(playerNamePrefab.transform.parent);

            g.GetComponent<RectTransform>().localScale = playerNamePrefab.GetComponent<RectTransform>().localScale;
            g.GetComponent<RectTransform>().localPosition = new Vector3(playerNamePrefab.GetComponent<RectTransform>().localPosition.x, playerNamePrefab.GetComponent<RectTransform>().localPosition.y - (i * 80), playerNamePrefab.GetComponent<RectTransform>().localPosition.z);
            g.transform.FindChild("Name").GetComponent<Text>().text = PhotonNetwork.playerList[i].ToString();
            //g.transform.FindChild("numPlayers").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].PlayerCount + "/" + PhotonNetwork.GetRoomList()[i].MaxPlayers;
            //g.transform.FindChild("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(() => { JoinRoom(g.transform.FindChild("game_name").GetComponent<Text>().text); });
            g.SetActive(true);
            playerNamePrefabs.Add(g);
        }
    }

    //the back button wil 
    public void BackButton()
	{
		//leave the photn waiting room
		PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Lobby");
        //lmc.JoinGameCanvasOn();
        //go to the lobby room
        //this will close the entire room if the master leaves the room (future extension)
        //PhotonNetwork.room.open = false;
    }
}
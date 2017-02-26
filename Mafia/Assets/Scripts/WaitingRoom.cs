using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoom : MonoBehaviour {

	//stores the information about the player
	public Text PlayerNameText; 

	//public Text PlayerIdText;

	//the amount of players in the game
	private int playerCount = 0;

	//the minimum amount of players that can be in the game
	private int minPlayers = 2;

	//future expansion
	//YouText will say YOU next your name in the list of names
	//MasterText will say master next to the creater of the game in the list of names
	//public Text YouText;
	//public Text MasterText;


	//public GameObject UIList;

	//public GameObject PlayerItemPrefab;

	public LobbyMenuController lmc; //lobby menu controller


	//Total time the waiting room wil stay open
	float timeLeft = 30.0f;
	public Text timer;

	// Use this for initialization
	void Start () {
		//wait 3 seconds and refresh the playerlist
		InvokeRepeating("playerList", 0.1f, 3.0f);

		//update the player count
		playerCount = playerList();
	}
	
	// Update is called once per frame
	void Update()
	{
		//timing for the waiting room
			timeLeft -= Time.deltaTime;
			timer.text = "Time Left:" + Mathf.Round (timeLeft);
			if (timeLeft < 0 && playerCount >= minPlayers) {
				//join the JoinGame Room
				lmc.JoinGameCanvasOn ();

			}
	}


	//getPlayer will get the users name and users ID
	public void getPlayer(PhotonPlayer player)
	{
        //PhotonNetwork.playerName = PlayerPrefs.GetString ("CurrUser"); //delete later
        //get the players name
        //PlayerNameText.text = player.name;
        PlayerNameText.text = player.NickName;
        //get the players ID
       // PlayerIdText.text = player.ID.ToString();

		//Find out which player in the list is you and which player is the Master
		//YouText.enabled = player.isLocal;
		//MasterText.enabled = player.isMasterClient;
	}

	//playerList will go through the list of players and get their names and id
	//it will also return the length of the Player List (the number of people in the game)
	public int playerList()
	{
		//string s = "";
		foreach (PhotonPlayer _player in PhotonNetwork.playerList) {
			getPlayer (_player);
		//	s += _player.ToString ();
		}
		//PlayerListText.text = s;
		//print (s);
        return PhotonNetwork.playerList.Length;
    }

	//the back button wil 
	public void backButton()
	{
		//leave the photn waiting room
		PhotonNetwork.LeaveRoom();
		//go to the lobby room
		lmc.RoomCanvasOn ();
		//this will close the entire room if the master leaves the room (future extension)
		//PhotonNetwork.room.open = false;
	}



}

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class AssignRoles1 : MonoBehaviour {

	//private static int MAXPLAYER = 16;

	//list of playernames that holds the preset name
	List <string> playerNames = new List<string> ();

	//public ExitGames.Client.Photon.Hashtable votedfor = new ExitGames.Client.Photon.Hashtable ();
	//public ExitGames.Client.Photon.Hashtable playerNames = new ExitGames.Client.Photon.Hashtable();


	int villan;
	int hero;
	int healer = 1;

	bool done = false;

	// Dont worry about this


	public bool InitializeRoles()
	{
		//Adds the preset names
		InitializePlayer();

		//change the nickname for each of the player
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			PhotonNetwork.playerList [i].NickName = playerNames[i];
			print (PhotonNetwork.playerList [i].NickName);
		}


		//calculate the roles (the math stuff how many mafia sheriffs etc)
		villan = (int)Math.Floor(PhotonNetwork.playerList.Length/3.0);
		hero = (int)Math.Ceiling (PhotonNetwork.playerList.Length / 6.0);

		//list of player names
		List<string> id = new List<string>();

        ExitGames.Client.Photon.Hashtable setVotes = new ExitGames.Client.Photon.Hashtable();
        setVotes.Add("VotedFor", "");
        setVotes.Add("dead", false);
        //initialize the VotedFor array and add nickname to the id list
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			id.Add (PhotonNetwork.playerList[i].NickName);
			PhotonNetwork.playerList [i].SetCustomProperties (setVotes);
		}

		//shuffle the list of id
		ListRandomizer.Shuffle(id);


		SetupPlayer (id);

		return done;

	}
	
	//shuffle the list player names and give them nicknames 
	private void SetupPlayer(List<string> list)
	{

		//Assign roles to each of the players
		for (int i = 0; i < list.Count; i++) {
			ExitGames.Client.Photon.Hashtable roles = new ExitGames.Client.Photon.Hashtable();
			if (villan != 0) {
				roles.Add ("roles", "Mafia");
				villan--;
			} else if (hero != 0) {
				roles.Add ("roles", "Sheriff");
				hero--;
			} else if (healer != 0) {
				roles.Add ("roles", "Healer");
				healer--;
			}
			else {
				roles.Add ("roles", "Civillian");
			}

			//find a player name in the photon that corresponds to the random list of id
			for(int k = 0; k < PhotonNetwork.playerList.Length; k++)
			{
				if (list [i].Equals (PhotonNetwork.playerList [k].NickName))
					PhotonNetwork.playerList[k].SetCustomProperties(roles);
			}
		}
	 

		done = true;
	}

	//Adds the preset names
	private void InitializePlayer(){
		playerNames.Add ("Giram");
		playerNames.Add ("Hayden");
		playerNames.Add ("David");
		playerNames.Add ("Dean");
		playerNames.Add ("James");
		playerNames.Add ("Andrew");
		playerNames.Add ("Aaron");
		playerNames.Add ("Ding");
		playerNames.Add ("Faith");
		playerNames.Add ("Umar");
		playerNames.Add ("Chloe");
		playerNames.Add ("Kelsy");
		playerNames.Add ("Troy");
		playerNames.Add ("Amanda");
		playerNames.Add ("Kelsy");
		playerNames.Add ("Insignia");
	}
					
}
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class AssignRoles : MonoBehaviour {

	public bool InitializeRoles()
	{
        bool done = false;
        //Adds the preset names
        List<string> playerNames = InitializePlayerNames();
		ListRandomizer.Shuffle (playerNames);
        ExitGames.Client.Photon.Hashtable setName = new ExitGames.Client.Photon.Hashtable();

        //change the nickname for each of the player
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
            if (!setName.ContainsKey(Global.CustomProperties.Name))
                setName.Add(Global.CustomProperties.Name, playerNames[i]);
            else
                setName[Global.CustomProperties.Name] = playerNames[i];
            PhotonNetwork.playerList[i].SetCustomProperties(setName);
			print (PhotonNetwork.playerList [i].NickName);
		}

		//list of player names
		List<string> id = new List<string>();
        //ExitGames.Client.Photon.Hashtable status = new ExitGames.Client.Photon.Hashtable();
        ExitGames.Client.Photon.Hashtable setVotes = new ExitGames.Client.Photon.Hashtable();
        setVotes.Add (Global.CustomProperties.VotedFor, "");
		setVotes.Add (Global.CustomProperties.Dead, false);

		//initialize the VotedFor array and add nickname to the id list
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			id.Add (PhotonNetwork.playerList[i].NickName);
			PhotonNetwork.playerList [i].SetCustomProperties (setVotes);
		}
		//shuffle the list of id
		ListRandomizer.Shuffle(id);


		done = SetupPlayer(id);

		return done;

	}
	
	//shuffle the list player names and give them nicknames 
	private bool SetupPlayer(List<string> list)
	{
        //calculate the roles (the math stuff how many mafia sheriffs etc)
        int villan = (int)Math.Floor(PhotonNetwork.playerList.Length / 3.0);
        int hero = (int)Math.Ceiling(PhotonNetwork.playerList.Length / 6.0);
        int healer = 1;
        ExitGames.Client.Photon.Hashtable roles = new ExitGames.Client.Photon.Hashtable();
        roles.Add(Global.CustomProperties.Roles, "");

        //Assign roles to each of the players
        for (int i = 0; i < list.Count; i++) {
			if (villan != 0) {
                roles[Global.CustomProperties.Roles] = Global.Role.Mafia;
				villan--;
			} else if (hero != 0) {
                roles[Global.CustomProperties.Roles] = Global.Role.Sheriff;
                hero--;
			} else if (healer != 0) {
                roles[Global.CustomProperties.Roles] = Global.Role.Nurse;
                healer--;
			}
			else {
                roles[Global.CustomProperties.Roles] = Global.Role.Civilian;
            }

			//find a player name in the photon that corresponds to the random list of id
			for(int k = 0; k < PhotonNetwork.playerList.Length; k++)
			{
				if (list [i].Equals (PhotonNetwork.playerList [k].NickName))
					PhotonNetwork.playerList[k].SetCustomProperties(roles);
			}
		}
        return true;
	}

	//Adds the preset names
	private List<string> InitializePlayerNames(){
        List<string> playerNames = new List<string>();
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


        return playerNames;
	}
					
}

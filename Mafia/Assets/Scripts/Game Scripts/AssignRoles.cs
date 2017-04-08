using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class AssignRoles : MonoBehaviour {

	//public AssignRolesTest test;

	//Initialiaze Roles will get the list of players in the current game and shuffle each of them so it is random.
	//The function will then give each player a preset name which is also shuffled
	//The InitializeRoles function will call the SetupPlayer at the end
	public bool InitializeRoles()
	{
        bool done = false;
        //Adds the preset names
        List<string> playerNames = InitializePlayerNames();

		/*//test
		test.testing (playerNames);*/

		ListRandomizer.Shuffle (playerNames);

		/*//test
		test.testing (playerNames);*/

        ExitGames.Client.Photon.Hashtable setName = new ExitGames.Client.Photon.Hashtable();

        //change the nickname for each of the player
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
            if (!setName.ContainsKey(Global.CustomProperties.Name))
                setName.Add(Global.CustomProperties.Name, playerNames[i]);
            else
                setName[Global.CustomProperties.Name] = playerNames[i];
            PhotonNetwork.playerList[i].SetCustomProperties(setName);
		}

		/*//test
		test.testingCustomProperties ();*/

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

		/*//test
		test.testingVoteanDeath ();*/

		//test.testing (id);

		//shuffle the list of id
		ListRandomizer.Shuffle(id);

		/*print ("Shuffle?");
		testing (id);*/

		done = SetupPlayer(id);

		//test
		//test.testingSetRole ();

		return done;

	}
	
	//SetupPlayer will give each person a role and set their role as part of the players customProperties 
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

	//Adds the preset names to a list and return that list
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
		playerNames.Add ("Aloy");
		playerNames.Add ("Insignia");


        return playerNames;
	}
}

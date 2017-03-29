using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class AssignRoles : MonoBehaviour {

	private static int MAXPLAYER = 16;

	private string [] names = new string[MAXPLAYER];
	//List <string> playerNames = new List<string> ();

	public ExitGames.Client.Photon.Hashtable votedfor = new ExitGames.Client.Photon.Hashtable ();
	public ExitGames.Client.Photon.Hashtable playerNames = new ExitGames.Client.Photon.Hashtable();


	// Dont worry about this
	void Start () {
		int unicode = 65;
		for (int count = 0; count < MAXPLAYER; count++) {
			char character = (char)unicode;
			names [count] = character.ToString ();
			unicode++;
		}
	}


	public bool InitializaRoles()
	{
		//calculate the roles (the math stuff how many mafia sheriffs etc)

		//set player nick name here to the preset one


		//SetupPlayer ();


		return true;

	}

	//shuffle the list player names and give them nicknames 
	private void SetupPlayer()
	{
		//add nicknames to playerName array

		//shuffle the playerName array 

		//GivePlayerNames()

	
	}


	private void GivePlayerNames()
	{
		//add roles to the playerName array so first one in the list is mafias then sheriffs, then doctor and rest civ
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}

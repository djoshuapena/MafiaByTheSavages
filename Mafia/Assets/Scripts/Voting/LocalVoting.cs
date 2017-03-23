using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class LocalVoting : MonoBehaviour {

	public GlobalVoting globalVoting;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//The IncVote function will take the dictonary and find the value associated with the the name given and increase
	//its value by one
	private void IncVote(String name)
	{
		int value = globalVoting.players [name];
		globalVoting.players [name] = value + 1;
	}

	//The DecVote function will take the dictonary and find the value associated with the the name given and decrease
	//its value by one
	private void DecVote(String name)
	{
		int value = globalVoting.players [name];
		globalVoting.players [name] = value - 1;
	}

	//The vote function will take a players name and check to see if they already voted if they did, it will decrease the
	//vote of the player they voted for and increase the player they are going to vote for
	public void Vote()
	{
		string name = "Dean";
		string myname = PhotonNetwork.playerName;

		if(globalVoting.voteMap[myname] == "null")
		{
			print("in null if");
			globalVoting.voteMap [myname] = name;
			IncVote (name);
			print (globalVoting.voteMap [myname]);
			print (globalVoting.players [name]);
		}
		else
		{	
			print ("in else if");
			string decString = globalVoting.voteMap [myname];
			DecVote (decString);
			print (globalVoting.players [name]);
			globalVoting.voteMap [myname] = name;
			IncVote (name);
			print (globalVoting.players [name]);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class VoteTesting : MonoBehaviour {

	public GlobalVoting globalVoting;
	List<string> returnedList;
	public AssignRoles gameController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void test1()
	{
		print ("test1");
		//votedfor.Add("Dean", "null");
		gameController.votedfor.Add ("David", "");
		//players.Add ("Dean", 0);
		globalVoting.players.Add ("nkjasd", 9);
		globalVoting.players.Add ("ss", 14);
		globalVoting.players.Add ("nkjafffsd", 14);
		globalVoting.players.Add ("nksssjasd", 9);
		globalVoting.players.Add ("nkjsasd", 9);
	}

	public void test()
	{
		print ("test");
		//votedfor.Add("Dean", "null");
		gameController.votedfor.Add ("gasdgads", "null");
		//players.Add ("Dean", 0);
		globalVoting.players.Add ("nkjasd", 9);
		globalVoting.players.Add ("Giram", 14);
		globalVoting.players.Add ("Patel", 14);
		globalVoting.players.Add ("nksssjasd", 9);
		globalVoting.players.Add ("nkjsasd", 9);

		globalVoting.InitializeVotes ();
		globalVoting.ChangeVote ("Dean");
		returnedList =globalVoting.GetVote(3);

		//print ("here");
		print (returnedList.Count);
		returnedList.ForEach(print);
	}


	//PhotonNetwork.player.SetCustomProperties (votedfor);
	//object name;
	//string myname = PhotonNetwork.playerName;
	//votedfor.TryGetValue ("Dean" ,out name);
	//print (name.ToString());
	//	for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
	//		string photonName = PhotonNetwork.playerList [i].NickName;
	//		string vote;
	//		PhotonNetwork.player.c
	//		votedfor.TryGetValue ((string)photonName, (string)vote);
	//		print (vote);
	//	}
}

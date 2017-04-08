using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class VoteTesting : MonoBehaviour {

	public VoteController globalVoting;
	List<string> returnedList = new List<string>();
	//public AssignRolesController gameController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//testinit() tests that the InitializeVotes actually
	public void testinit()
	{
		ExitGames.Client.Photon.Hashtable clearVotes = new ExitGames.Client.Photon.Hashtable();
		clearVotes.Add(Global.CustomProperties.VotedFor, "Dean");

		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			PhotonNetwork.playerList [i].SetCustomProperties (clearVotes);
		}

		for (int count = 0; count < PhotonNetwork.playerList.Length; count++) {
			//print (PhotonNetwork.playerList[count].NickName);
			print (PhotonNetwork.playerList [count].CustomProperties [Global.CustomProperties.VotedFor]);

		}
		print ("------------------");
		globalVoting.InitializeVotes ();

		for (int count = 0; count < PhotonNetwork.playerList.Length; count++) {
			//print (PhotonNetwork.playerList[count].NickName);
			print (PhotonNetwork.playerList [count].CustomProperties [Global.CustomProperties.VotedFor]);

		}
	}

	//test the ChangeVote script
	public void testchangeVote()
	{
		ExitGames.Client.Photon.Hashtable clearVotes = new ExitGames.Client.Photon.Hashtable();
		clearVotes.Add(Global.CustomProperties.VotedFor, "Dean");

		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			PhotonNetwork.playerList [i].SetCustomProperties (clearVotes);
		}

		for (int count = 0; count < PhotonNetwork.playerList.Length; count++) {
			//print (PhotonNetwork.playerList[count].NickName);
			print (PhotonNetwork.playerList [count].CustomProperties [Global.CustomProperties.VotedFor]);

		}
		print ("------------------");
		globalVoting.ChangeVote ("Giram");

		for (int count = 0; count < PhotonNetwork.playerList.Length; count++) {
			//print (PhotonNetwork.playerList[count].NickName);
			print (PhotonNetwork.playerList [count].CustomProperties [Global.CustomProperties.VotedFor]);

		}

	}

	//getVoteTest() tests if GetVote(int) correctly returns the person with the highest vote
	public void getVoteTest()
	{
		returnedList = globalVoting.GetVote(4);
		print ("here");
		for (int count = 0; count <= returnedList.Count; count++) {
			print ("in for");
			print (returnedList [count]);
		}
	}

	//getSheriffpick() tests GetSheriffArrest()
	public void getSheriffpick()
	{
		ExitGames.Client.Photon.Hashtable clearVotes = new ExitGames.Client.Photon.Hashtable();
		//clearVotes.Add(Global.CustomProperties.VotedFor, "Dean");

		int x = 0;
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			if (PhotonNetwork.playerList [i].CustomProperties [Global.CustomProperties.Roles].Equals (Global.Role.Mafia)) {
				x = i;
			}
		}

		clearVotes.Add(Global.CustomProperties.VotedFor, PhotonNetwork.playerList[x].CustomProperties[Global.CustomProperties.Name]);
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			PhotonNetwork.playerList [i].SetCustomProperties (clearVotes);
		}

		returnedList = globalVoting.GetSheriffArrest();
		for (int count = 0; count < returnedList.Count; count++) {
			print (returnedList [count]);
		}
	}

	//getMafiapick() tests GetMafiaKill()
	public void getMafiapick()
	{
		ExitGames.Client.Photon.Hashtable clearVotes = new ExitGames.Client.Photon.Hashtable();

		int x = 0; 
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			if (PhotonNetwork.playerList [i].CustomProperties [Global.CustomProperties.Roles].Equals (Global.Role.Sheriff)) {
				x = i;
			}
		}

		clearVotes.Add(Global.CustomProperties.VotedFor, PhotonNetwork.playerList[x].CustomProperties[Global.CustomProperties.Name]);
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			PhotonNetwork.playerList [i].SetCustomProperties (clearVotes);
		}

		string kill = globalVoting.GetMafiaKill();
		print (kill);
	}

	//getNursePick() tests GetNurseSave
	public void getNursepick()
	{
		ExitGames.Client.Photon.Hashtable clearVotes = new ExitGames.Client.Photon.Hashtable();

		int x = 0; 
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			if (PhotonNetwork.playerList [i].CustomProperties [Global.CustomProperties.Roles].Equals (Global.Role.Nurse)) {
				x = i;
			}
		}

		clearVotes.Add(Global.CustomProperties.VotedFor, PhotonNetwork.playerList[x].CustomProperties[Global.CustomProperties.Name]);
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			PhotonNetwork.playerList [i].SetCustomProperties (clearVotes);
		}

		string save = globalVoting.GetNurseSave();
		print (save);
	}
}



//	//PhotonNetwork.player.SetCustomProperties (votedfor);
//	//object name;
//	//string myname = PhotonNetwork.playerName;
//	//votedfor.TryGetValue ("Dean" ,out name);
//	//print (name.ToString());
//	//	for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
//	//		string photonName = PhotonNetwork.playerList [i].NickName;
//	//		string vote;
//	//		PhotonNetwork.player.c
//	//		votedfor.TryGetValue ((string)photonName, (string)vote);
//	//		print (vote);
//	//	}
//}

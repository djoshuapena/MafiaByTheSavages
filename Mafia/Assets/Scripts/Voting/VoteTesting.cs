using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteTesting : MonoBehaviour {

	public GlobalVoting globalVoting;
	public LocalVoting localVoting;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void test1()
	{
		globalVoting.voteMap.Add ("Dean", "null");
		globalVoting.players.Add ("Dean", 2);
		globalVoting.voteMap.Add ("Hayden", "null");
		globalVoting.players.Add ("Hayden", 2);
		globalVoting.voteMap.Add ("David", "null");
		globalVoting.players.Add ("David", 3);

		globalVoting.AddPlayers ();
		localVoting.Vote ("Dean");
		globalVoting.HighestVote ();
		localVoting.Vote ("David");
		globalVoting.HighestVote ();
	}
}

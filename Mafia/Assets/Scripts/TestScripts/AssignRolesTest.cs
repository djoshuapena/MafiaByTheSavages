using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignRolesTest : MonoBehaviour {

	public AssignRoles assignRoles;

	//Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
//	void Update () {
		
//	}

	public void test()
	{
		assignRoles.InitializeRoles ();
	}

	public void testing(List<string> playerNames)
	{
		for (int count = 0; count < playerNames.Count; count++) {
			print (playerNames [count]);
		}
	}

	public void testingCustomProperties()
	{
		for (int count = 0; count < PhotonNetwork.playerList.Length; count++) {
			print (PhotonNetwork.playerList[count].NickName);
			print(PhotonNetwork.playerList [count].CustomProperties [Global.CustomProperties.Name]);
		}
	}

	public void testingVoteanDeath()
	{
		for (int count = 0; count < PhotonNetwork.playerList.Length; count++) {
			//print (PhotonNetwork.playerList[count].NickName);
			print(PhotonNetwork.playerList [count].CustomProperties [Global.CustomProperties.VotedFor]);

			print (PhotonNetwork.playerList [count].CustomProperties [Global.CustomProperties.Dead]);

		}
	}

	public void testingSetRole()
	{
		for (int count = 0; count < PhotonNetwork.playerList.Length; count++) {
			print (PhotonNetwork.playerList [count].CustomProperties [Global.CustomProperties.Roles]);
		}
	}
}

//using System;
//using System.Collections.Generic;
//using System.Text;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Linq;

//public class GlobalVoting : MonoBehaviour {

//	public Dictionary<string, int> players = new Dictionary<string, int>();
//	List<string> ret = new List<string> ();
//	List<string> emptyList = new List<string> ();

//	//public AssignRoles gameController;


//	public void InitializeVotes(){
		
//		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
//			string photonName = PhotonNetwork.playerList [i].NickName;
//			//gameController.votedfor.Remove (photonName);
//			//gameController.votedfor.Add (photonName, "");
//		}
//		//PhotonNetwork.player.SetCustomProperties (gameController.votedfor);
//	}

//	public void ChangeVote(string name){
//		//gameController.votedfor.Remove (PhotonNetwork.player);
//		//gameController.votedfor.Add (PhotonNetwork.player, name);
//		//PhotonNetwork.player.SetCustomProperties (gameController.votedfor);
//	}
		
//	public List<string> GetVote(int majority)
//	{
//		for (int i = 0; i < PhotonNetwork.playerList.Length; i++){
//			string photonName = PhotonNetwork.playerList [i].NickName;
//			object name;
//			//gameController.votedfor.TryGetValue (photonName,out name);
//			/*if (name.ToString () != "") {
//				int value = players [name.ToString ()];
//				players [name.ToString ()] = value + 1;
//			} else {
//				players.Add (photonName, 1);
//			}
//		}
//		var sortedDict = from entry in players orderby entry.Value descending select entry;
//		var first = sortedDict.First();
//		int values = first.Value;

//		foreach (KeyValuePair<string, int> pair in players)
//		{
//			if (pair.Value == values)
//			{
//				ret.Add (pair.Key);
//			}
//		}
//		if (ret.Count == 0 || ret.Count > majority) {
//			return emptyList;
//		} else {
//			return ret;
//		}
//	}

//}


////-------------------------------------------------Code Graveyard----------------------------------------------------------
///*


////UI stuff

////private List<GameObject> roomPrefabs = new List<GameObject>();
////public GameObject roomPrefab;

////public void DispalyOnUI()
////{
////int count = 0;
////foreach (KeyValuePair<string, int> kvp in players) {

////GameObject g = Instantiate(roomPrefab);
////g.transform.SetParent(roomPrefab.transform.parent);

////g.GetComponent<RectTransform>().localScale = roomPrefab.GetComponent<RectTransform>().localScale;
////g.GetComponent<RectTransform>().localPosition = new Vector3(roomPrefab.GetComponent<RectTransform>().localPosition.x, roomPrefab.GetComponent<RectTransform>().localPosition.y - (count * 80), roomPrefab.GetComponent<RectTransform>().localPosition.z);
////g.transform.FindChild ("insert name on UI here").GetComponent<Text> ().text = kvp.Key;
////g.transform.FindChild ("insert name on UI here").GetComponent<Text> ().text = kvp.Value.ToString();
////g.transform.FindChild("Insert Button name here").GetComponent<Button>().onClick.AddListener(() => { Vote(kvp.Key, kvp.Value); });
////g.SetActive(true);
////roomPrefabs.Add(g);
////count++;
////}
////}

////End UI stuff


////foreach (KeyValuePair<string, string> kvp in voteMap) {
////	print (kvp.Key);
////	print (kvp.Value);
////}

//private void PrHighestVotes()
//{
//	for(int count = 0; count < maxVote.Length; count++)
//	{
//		if (maxVote [count] != null)
//		{
//			print (maxVote [count]);
//		}
//	}
//}

//public class GlobalVoting : MonoBehaviour {

//	//The players dictonary holds the players name and the number of vote the player has
//	public Dictionary<string, int> players = new Dictionary<string, int>();
//	//The voteMap disctonary holds the players name and the player they voted for 
//	//(it is null if they have not voted for anyone)
//	public Dictionary<string, string> voteMap = new Dictionary<string, string>();

//	//This is the max amount of players that is in the game
//	private static int MAXPLAYERS = 16;
//	//The arrayCount is a counter variable that holds the postion of the array
//	private int arrayCount = 0;
//	//This array holds the players with the most votes
//	private string [] maxVote = new string[MAXPLAYERS];

//	// Use this for initialization
//	void Start () {

//	}

//	// Update is called once per frame
//	void Update () {

//	}

//	//The AddPlayers function adds the players in the game to the two dictonary
//	public void AddPlayers()
//	{
//		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
//			string photonName = PhotonNetwork.playerList [i].NickName;
//			print (photonName);
//			players.Add (photonName, 0);
//			voteMap.Add (photonName, "null");
//		}
//	}

//	//The HigestVote function will go through the disctonary and find who has the highest vote and store
//	//the people with the highest vote in the maxVote array
//	public void HighestVote()
//	{
//		KeyValuePair<string, int> max = new KeyValuePair<string, int>(); 

//		foreach (KeyValuePair<string, int> kvp in players)
//		{
//			if (kvp.Value > max.Value) {
//				for(int count = 0; count < 16; count++)
//				{
//					maxVote [count] = null;
//				}
//				arrayCount = 0;
//				max = kvp;
//				maxVote [arrayCount] = kvp.Key;
//			} 
//			else if (kvp.Value == max.Value) 
//			{
//				arrayCount++;
//				maxVote [arrayCount] = kvp.Key;
//			}
//		}
//		PrHighestVotes ();
//	}

//	//debug
//	private void PrHighestVotes()
//	{
//		for(int count = 0; count < maxVote.Length; count++)
//		{
//			if (maxVote [count] != null)
//			{
//				print (maxVote [count]);
//			}
//		}
//	}

//}


////var r = from entry in players
////		where entry.Value == values
////      select entry.Key;
////foreach (var key in r) {
////	ret.Add (key);
////	print (key);
////	}



//*/
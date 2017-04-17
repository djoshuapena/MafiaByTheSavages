using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class VoteController : Photon.MonoBehaviour {

	//InitializeVotes() sets all votes to an empty string
	public void InitializeVotes(){
		ExitGames.Client.Photon.Hashtable clearVotes = new ExitGames.Client.Photon.Hashtable();
        clearVotes.Add(Global.CustomProperties.VotedFor, "");

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			PhotonNetwork.playerList [i].SetCustomProperties (clearVotes);
		}
	}

	//ChangeVote(name) changes the vote to name given
	public void ChangeVote(string name){
        PhotonNetwork.player.CustomProperties[Global.CustomProperties.VotedFor] = name;
        photonView.RPC("ChangeVoteHelper", PhotonTargets.Others, PhotonNetwork.player, name);
		//ExitGames.Client.Photon.Hashtable replaceVote = new ExitGames.Client.Photon.Hashtable ();
		//replaceVote.Add (Global.CustomProperties.VotedFor, name);
		//PhotonNetwork.player.SetCustomProperties (replaceVote);
	//	if ((string)PhotonNetwork.player.CustomProperties ["VotedFor"] == name) {
	//		Debug.Log ("We did it");
	//	} 
	//	else {
	//		Debug.Log ("Fail");
	//	}

	}

    [PunRPC]
    public void ChangeVoteHelper(PhotonPlayer player, string name)
    {
        for(int pos = 0; pos < PhotonNetwork.playerList.Length; pos++)
        {
            if(PhotonNetwork.playerList[pos] == player)
            {
                PhotonNetwork.playerList[pos].CustomProperties[Global.CustomProperties.VotedFor] = name;
            }
        }
    }
		
	//GetVote(majority) will take the votedFor custom properties for each player and find out which person got voted
	//for the most (multiple people if any) and return the names of those person in a list.
	public List<string> GetVote(int majority)
	{

        Dictionary<string, int> players = new Dictionary<string, int>();
        List<string> returnList = new List<string>();

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++){
			//string photonName = PhotonNetwork.playerList [i].NickName;
			string name = (string)PhotonNetwork.playerList[i].CustomProperties[Global.CustomProperties.VotedFor];
			//gameController.votedfor.TryGetValue (photonName,out name);
			if (name != "") {
				if (players.ContainsKey (name))
					players [name] ++;
				else
					players.Add (name, 1);
			}
		}

        if (players.Count > 0)
        {
            var sortedDict = from entry in players orderby entry.Value descending select entry;
            var first = sortedDict.First();
            int values = first.Value;

            foreach (KeyValuePair<string, int> pair in players)
            {
                if (pair.Value == values)
                {
                    returnList.Add(pair.Key);
                }
            }
        }

		if (returnList.Count == 0 || returnList.Count > majority)
        {
            returnList.Clear();
			print ("clear");
            return returnList;
		}
        else 
        {
			return returnList;
		}
	}

	//GetMafiaKill() will take votedFor custom properties and find out who the mafia voted for and return the name
	//they voted for as a string
    public string GetMafiaKill()
    {
        Dictionary<string, int> killPlayer = new Dictionary<string, int>();
        List<string> returnList = new List<string>();

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            //string photonName = PhotonNetwork.playerList [i].NickName;
            string name = (string)PhotonNetwork.playerList[i].CustomProperties[Global.CustomProperties.VotedFor];
            //gameController.votedfor.TryGetValue (photonName,out name);
            if ((name != "") && PhotonNetwork.playerList[i].CustomProperties[Global.CustomProperties.Roles].Equals(Global.Role.Mafia))
            {
                if (killPlayer.ContainsKey(name))
                    killPlayer[name]++;
                else
                    killPlayer.Add(name, 1);
            }
        }

        if (killPlayer.Count > 0)
        {
            var sortedDict = from entry in killPlayer orderby entry.Value descending select entry;
            var first = sortedDict.First();
            int values = first.Value;

            foreach (KeyValuePair<string, int> pair in killPlayer)
            {
                if (pair.Value == values)
                {
                    returnList.Add(pair.Key);
                }
            }
        }
        if (returnList.Count == 1)
            return returnList[0];

        return "";
    }

	//GetSheiffArrest() will take votedFor custom properties and find out who Sheriff arrested and return the list of 
	//arrested players
    public List<string> GetSheriffArrest()
    {
        Dictionary<string, int> arrestPlayer = new Dictionary<string, int>();
        List<string> returnList = new List<string>();

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            //string photonName = PhotonNetwork.playerList [i].NickName;
            string name = (string)PhotonNetwork.playerList[i].CustomProperties[Global.CustomProperties.VotedFor];
            //gameController.votedfor.TryGetValue (photonName,out name);
            if ((name != "") && PhotonNetwork.playerList[i].CustomProperties[Global.CustomProperties.Roles].Equals(Global.Role.Sheriff) && isPlayerMafia(name))
            {
                if (arrestPlayer.ContainsKey(name))
                    arrestPlayer[name]++;
                else
                    arrestPlayer.Add(name, 1);
            }
        }

        //if (arrestPlayer.Count > 0)
        //{
        //    var sortedDict = from entry in arrestPlayer orderby entry.Value descending select entry;
        //    //var first = sortedDict.First();
        //    //int values = first.Value;

        foreach (KeyValuePair<string, int> pair in arrestPlayer)
        {
            //if (pair.Value == values)
            //{
            returnList.Add(pair.Key);
            //}
        }
        //}
        if (returnList.Count == 0 || returnList.Count > 3)
        {
            returnList.Clear();
        }

        return returnList;
    }

	//checks to see if the player is a mafia
    private bool isPlayerMafia(string name)
    {
        for(int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (PhotonNetwork.playerList[i].CustomProperties[Global.CustomProperties.Name].Equals(name) && PhotonNetwork.playerList[i].CustomProperties[Global.CustomProperties.Roles].Equals(Global.Role.Mafia))
                return true;
        }
        return false;
    }

	//GetNurseSave() will take votedFor custom properties and find out who the nurse chose to heal and return the name of that person
    public string GetNurseSave()
    {
        Dictionary<string, int> savePlayer = new Dictionary<string, int>();
        List<string> returnList = new List<string>();

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            //string photonName = PhotonNetwork.playerList [i].NickName;
            string name = (string)PhotonNetwork.playerList[i].CustomProperties[Global.CustomProperties.VotedFor];
            //gameController.votedfor.TryGetValue (photonName,out name);
            if ((name != "") && PhotonNetwork.playerList[i].CustomProperties[Global.CustomProperties.Roles].Equals(Global.Role.Nurse))
            {
                if (savePlayer.ContainsKey(name))
                    savePlayer[name]++;
                else
                    savePlayer.Add(name, 1);
            }
        }

        if (savePlayer.Count > 0)
        {
            var sortedDict = from entry in savePlayer orderby entry.Value descending select entry;
            var first = sortedDict.First();
            int values = first.Value;

            foreach (KeyValuePair<string, int> pair in savePlayer)
            {
                if (pair.Value == values)
                {
                    returnList.Add(pair.Key);
                }
            }
        }
        if (returnList.Count == 1)
            return returnList[0];

        return "";
    }
}


//-------------------------------------------------Code Graveyard----------------------------------------------------------
/*


//UI stuff

//private List<GameObject> roomPrefabs = new List<GameObject>();
//public GameObject roomPrefab;

//public void DispalyOnUI()
//{
//int count = 0;
//foreach (KeyValuePair<string, int> kvp in players) {

//GameObject g = Instantiate(roomPrefab);
//g.transform.SetParent(roomPrefab.transform.parent);

//g.GetComponent<RectTransform>().localScale = roomPrefab.GetComponent<RectTransform>().localScale;
//g.GetComponent<RectTransform>().localPosition = new Vector3(roomPrefab.GetComponent<RectTransform>().localPosition.x, roomPrefab.GetComponent<RectTransform>().localPosition.y - (count * 80), roomPrefab.GetComponent<RectTransform>().localPosition.z);
//g.transform.FindChild ("insert name on UI here").GetComponent<Text> ().text = kvp.Key;
//g.transform.FindChild ("insert name on UI here").GetComponent<Text> ().text = kvp.Value.ToString();
//g.transform.FindChild("Insert Button name here").GetComponent<Button>().onClick.AddListener(() => { Vote(kvp.Key, kvp.Value); });
//g.SetActive(true);
//roomPrefabs.Add(g);
//count++;
//}
//}

//End UI stuff


//foreach (KeyValuePair<string, string> kvp in voteMap) {
//	print (kvp.Key);
//	print (kvp.Value);
//}

private void PrHighestVotes()
{
	for(int count = 0; count < maxVote.Length; count++)
	{
		if (maxVote [count] != null)
		{
			print (maxVote [count]);
		}
	}
}

public class GlobalVoting : MonoBehaviour {

	//The players dictonary holds the players name and the number of vote the player has
	public Dictionary<string, int> players = new Dictionary<string, int>();
	//The voteMap disctonary holds the players name and the player they voted for 
	//(it is null if they have not voted for anyone)
	public Dictionary<string, string> voteMap = new Dictionary<string, string>();

	//This is the max amount of players that is in the game
	private static int MAXPLAYERS = 16;
	//The arrayCount is a counter variable that holds the postion of the array
	private int arrayCount = 0;
	//This array holds the players with the most votes
	private string [] maxVote = new string[MAXPLAYERS];

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	//The AddPlayers function adds the players in the game to the two dictonary
	public void AddPlayers()
	{
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			string photonName = PhotonNetwork.playerList [i].NickName;
			print (photonName);
			players.Add (photonName, 0);
			voteMap.Add (photonName, "null");
		}
	}

	//The HigestVote function will go through the disctonary and find who has the highest vote and store
	//the people with the highest vote in the maxVote array
	public void HighestVote()
	{
		KeyValuePair<string, int> max = new KeyValuePair<string, int>(); 

		foreach (KeyValuePair<string, int> kvp in players)
		{
			if (kvp.Value > max.Value) {
				for(int count = 0; count < 16; count++)
				{
					maxVote [count] = null;
				}
				arrayCount = 0;
				max = kvp;
				maxVote [arrayCount] = kvp.Key;
			} 
			else if (kvp.Value == max.Value) 
			{
				arrayCount++;
				maxVote [arrayCount] = kvp.Key;
			}
		}
		PrHighestVotes ();
	}

	//debug
	private void PrHighestVotes()
	{
		for(int count = 0; count < maxVote.Length; count++)
		{
			if (maxVote [count] != null)
			{
				print (maxVote [count]);
			}
		}
	}

}


//var r = from entry in players
//		where entry.Value == values
//      select entry.Key;
//foreach (var key in r) {
//	ret.Add (key);
//	print (key);
//	}



*/
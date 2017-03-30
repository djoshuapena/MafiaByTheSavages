using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class EndGameController : MonoBehaviour {
    
    //determines if the last civilian has been killed
    public bool isLastCivilian()
    {
        PhotonPlayer [] playerList = PhotonNetwork.playerList;//list of players in game
        for (int x = 0; x < playerList.Length; x++)
        {
            if(playerList[x].CustomProperties.ContainsKey("sheriff"))
            {
                return false;
            }
            if (playerList[x].CustomProperties.ContainsKey("nurse"))
            {
                return false;
            }
            if (playerList[x].CustomProperties.Equals("civilian"))
            {
                return false;
            }
        }
        return true;
    }

    //determines if the last mafia has been killed
    public bool isLastMafia()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;//list of players in game
        for (int x = 0; x < playerList.Length; x++)
        {
            if (playerList[x].CustomProperties.ContainsKey("mafia"))
            {
                return false;
            }
        }
            return true;
    }

    //determines if the last sheriff has been killed
    public bool isLastSheriff()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;//list of players in game

        for (int x = 0; x < playerList.Length; x++)
        {
            if (playerList[x].CustomProperties.ContainsKey("sheriff"))
            {
                return false;
            }
        }
        return true;
    }
}

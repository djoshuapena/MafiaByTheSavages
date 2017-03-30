using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour {
    
    //determines if the last civilian has been killed
    public bool CivilianAlive()
    {
        PhotonPlayer [] playerList = PhotonNetwork.playerList;//list of players in game
        for(int x = 0; x < playerList.Length; x++)
        {
            if(playerList[x].CustomProperties.ContainsValue("sheriff") && playerList[x].CustomProperties["dead"].Equals(false))
            {
                return false;
            }
            else if(playerList[x].CustomProperties.ContainsValue("nurse") && playerList[x].CustomProperties["dead"].Equals(false))
            {
                return false;
            }
            else if(playerList[x].CustomProperties.ContainsValue("civilian") && playerList[x].CustomProperties["dead"].Equals(false))
            {
                return false;
            }
        }
        return true;
    }

    //determines if the last mafia has been killed
    public bool MafiaAlive()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;//list of players in game
        for(int x = 0; x < playerList.Length; x++)
        {
            if(playerList[x].CustomProperties.ContainsValue("mafia") && playerList[x].CustomProperties["dead"].Equals(false))
            {
                return false;
            }
        }
            return true;
    }

    //determines if the last sheriff has been killed
    public bool SheriffAlive()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;//list of players in game

        for(int x = 0; x < playerList.Length; x++)
        {
            if(playerList[x].CustomProperties.ContainsValue("sheriff") && playerList[x].CustomProperties["dead"].Equals(false))
            {
                return false;
            }
        }
        return true;
    }
}
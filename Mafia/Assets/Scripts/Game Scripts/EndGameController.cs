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
            if(playerList[x].CustomProperties.ContainsValue("Civilian") && (bool)playerList[x].CustomProperties["Dead"] == false)
            {
                return true;
            }
            else if(playerList[x].CustomProperties.ContainsValue("Nurse") && (bool)playerList[x].CustomProperties["Dead"] == false)
            {
                return true;
            }
            else if(playerList[x].CustomProperties.ContainsValue("Sheriff") && (bool)playerList[x].CustomProperties["Dead"] == false)
            {
                return true;
            }
        }
        return false;
    }

    //determines if the last mafia has been killed
    public bool MafiaAlive()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;//list of players in game
        for(int x = 0; x < playerList.Length; x++)
        {
            Debug.Log("Is there a mafia?" + playerList[x].CustomProperties.ContainsValue("Mafia"));
            if (playerList[x].CustomProperties.ContainsValue("Mafia") && (bool)playerList[x].CustomProperties["Dead"] == false)
            {
                return true;
            }
        }
            return false;
    }

    //determines if the last sheriff has been killed
    public bool SheriffAlive()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;//list of players in game

        for(int x = 0; x < playerList.Length; x++)
        {
            if(playerList[x].CustomProperties.ContainsValue("Sheriff") && (bool)playerList[x].CustomProperties["Dead"] == false)
            {
                return true;
            }
        }
        return false;
    }
}
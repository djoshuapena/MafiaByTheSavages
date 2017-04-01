using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour {
    
    //determines if the last civilian has been killed
    public bool CivilianAlive()
    {
        for(int x = 0; x < PhotonNetwork.playerList.Length; x++)
        {
            if(PhotonNetwork.playerList[x].CustomProperties.ContainsValue(Global.Role.Civilian) && !(bool)PhotonNetwork.playerList[x].CustomProperties[Global.CustomProperties.Dead])
            {
                return true;
            }
            else if(PhotonNetwork.playerList[x].CustomProperties.ContainsValue(Global.Role.Nurse) && !(bool)PhotonNetwork.playerList[x].CustomProperties[Global.CustomProperties.Dead])
            {
                return true;
            }
            else if(PhotonNetwork.playerList[x].CustomProperties.ContainsValue(Global.Role.Sheriff) && !(bool)PhotonNetwork.playerList[x].CustomProperties[Global.CustomProperties.Dead])
            {
                return true;
            }
        }
        return false;
    }

    //determines if the last mafia has been killed
    public bool MafiaAlive()
    {
        for(int x = 0; x < PhotonNetwork.playerList.Length; x++)
        {
            if (PhotonNetwork.playerList[x].CustomProperties.ContainsValue(Global.Role.Mafia) && !(bool)PhotonNetwork.playerList[x].CustomProperties[Global.CustomProperties.Dead])
            {
                return true;
            }
        }
            return false;
    }

    //determines if the last sheriff has been killed
    public bool SheriffAlive()
    {
        for(int x = 0; x < PhotonNetwork.playerList.Length; x++)
        {
            if(PhotonNetwork.playerList[x].CustomProperties.ContainsValue(Global.Role.Sheriff) && !(bool)PhotonNetwork.playerList[x].CustomProperties[Global.CustomProperties.Dead])
            {
                return true;
            }
        }
        return false;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DAVID DO NOT 
//CHANGE
//MY SHIT
//IT DOES NOT NEED
//TO BE CHANGED
public class Connect : Photon.MonoBehaviour {

    public ChatHandler chatHandler; // Set in inspector
    public string UserName;
   
	// Use this for initialization
	void Start ()
    {
        UserName = (string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Name];
        if (string.IsNullOrEmpty(UserName))
        {
            UserName = PhotonNetwork.player.UserId; 
        }
       // "FakeName" + Environment.TickCount % 99; //Put real username here.
        ConnectToChat(UserName);
	}
	
    public void ConnectToChat (string userName)
    {
        chatHandler.Connect(UserName);
    } 
	
    

}

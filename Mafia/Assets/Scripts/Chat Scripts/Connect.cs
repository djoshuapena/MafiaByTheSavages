using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect : MonoBehaviour {

    public ChatHandler chatHandler;
    public string UserName { get; set; }

	// Use this for initialization
	void Start ()
    {
        chatHandler.StateText.text = "";
        UserName = "FakeName" + Environment.TickCount % 99;
        ConnectToChat(UserName);
	}
	
    void ConnectToChat (string userName)
    {
        chatHandler.Connect(UserName);
    } 
	
    

}

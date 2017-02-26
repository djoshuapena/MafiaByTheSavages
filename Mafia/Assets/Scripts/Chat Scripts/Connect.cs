﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect : MonoBehaviour {

    public ChatHandler chatHandler;
    public string UserName { get; set; }

	// Use this for initialization
	void Start ()
    {
        chatHandler.StateText.text = "";
        UserName = "Aaron";
        ConnectToChat(UserName);
	}
	
    void ConnectToChat (string userName)
    {
        chatHandler.Connect("Aaron");
    } 
	
    

}

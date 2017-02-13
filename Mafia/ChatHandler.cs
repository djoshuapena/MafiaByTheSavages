using System;
using System.Collections.Generic;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using UnityEngine.UI;

public class ChatHandler : MonoBehaviour, IChatClientListener
{
    public ChatClient chatClient;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true; // this must run in background or it will drop connection if not focussed.
    }

}
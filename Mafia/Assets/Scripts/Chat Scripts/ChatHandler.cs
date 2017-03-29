using System;
//using System.Collections.Generic;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class ChatHandler : MonoBehaviour, IChatClientListener {

    public string[]     GameChannels;
    public string       UserName { get; set; }
    public ChatClient   chatClient { get; set; }
    public Text         StateText;
    public ChatDisplay  chatDisplay;


    public string       PlayerStatus;
    public int          CurrentChannel;

    public void DebugReturn(DebugLevel level, string message)
    {
        if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
        {
            UnityEngine.Debug.LogError(message);
        }
        else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
        {
            UnityEngine.Debug.LogWarning(message);
        }
        else
        {
            UnityEngine.Debug.Log(message);
        }
    }

    public void OnChatStateChange(ChatState state)
    {
        this.StateText.text = state.ToString();
    }

    public void Connect(string userName)
    {
        Debug.Log("ChatHandler Start() test!");
        //UserName = "Aaron Jackson";
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "US";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.ChatAppID, "0.1", new ExitGames.Client.Photon.Chat.AuthenticationValues(userName));
    }

    public void OnConnected()
    {
        Debug.Log("Connected as: " + chatClient.UserId);
        chatClient.Subscribe(GameChannels);
    }

    public void OnDisconnected()
    {
        //throw new NotImplementedException();
        Debug.Log("Disconnected");
    }

    public void OnApplicationQuit()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        chatDisplay.ShowMessages(GameChannels[CurrentChannel]);
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for(int i = 0;i<channels.Length;i++)
        {
            if (results[i] == true)
            {
                SendChatMessage(channels[i], "You may chat");
            }
        }
        Debug.Log("Subscribed to: " + channels[0]);
    }

    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log("Unsubscribed to: " + GameChannels[0]);
    }

    public void SendChatMessage(string channel, string msg)
    {
        if (string.IsNullOrEmpty(msg))
        {
            return;
        }
        chatClient.PublishMessage(channel, msg);
    }

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true;
	}

    // Update is called once per frame
	void Update () {
        if (this.chatClient != null)
        {
            this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }
    }
}

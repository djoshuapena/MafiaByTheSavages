using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChatHandler : MonoBehaviour, IChatClientListener {

    public string[]     GameChannel;
    public string       UserName { get; set; }
    public ChatClient   chatClient;
    public Text         StateText;
    public Text         CurrentChannelText;
    //public Text         InputFieldChat;

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
        chatClient.Subscribe(GameChannel);
       
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
        ShowChannel(channelName);
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
        //chatClient.PublishMessage(channels[0], "Hello Everyone!");
        SendChatMessage("Hello Everyone!");
        Debug.Log("Subscribed to: " + GameChannel[0]);
    }

    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log("Unsubscribed to: " + GameChannel[0]);
    }

    public void ShowChannel(string channelName)
    {
        if (string.IsNullOrEmpty(channelName))
        {
            return;
        }

        ChatChannel channel = null;
        bool found = this.chatClient.TryGetChannel(channelName, out channel);
        if (!found)
        {
            Debug.Log("ShowChannel failed to find channel: " + channelName);
            return;
        }

        CurrentChannelText.text = channel.ToStringMessages();
    }

    public void SendChatMessage(string msg)
    {
        if (string.IsNullOrEmpty(msg))
        {
            return;
        }
        chatClient.PublishMessage(GameChannel[0], msg);
    }

   /* public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            SendChatMessage(InputFieldChat.text);
            InputFieldChat.text = "";
        }
    }

    /// <summary>
    /// Send a message if the Send button is pressed.
    /// </summary>
    public void OnClickSend()
    {
        if (InputFieldChat != null)
        {
            SendChatMessage(InputFieldChat.text);
            InputFieldChat.text = "";
        }
    }*/

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true;
        //return;
        //UserName = "Aaron Jackson";
        //Connect("Aaron");
	}


    // Update is called once per frame
	void Update () {
        if (this.chatClient != null)
        {
            this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }
    }
}

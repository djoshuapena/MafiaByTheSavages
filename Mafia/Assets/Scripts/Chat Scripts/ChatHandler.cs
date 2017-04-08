using System;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


//DAVID DO NOT 
//CHANGE
//MY SHIT
//IT DOES NOT NEED
//TO BE CHANGED
public class ChatHandler : Photon.MonoBehaviour, IChatClientListener {

    public string[]     GameChannels; // Set in inspector temporarily.
    public ChatClient chatClient;// { get; set; }
    //public Text         StateText; // Set in inspector
    public ChatDisplay  chatDisplay; // Set in inspector

    //These 2 vars can be removed when chat has access to the custom player traits etc.
    //Can make seperate players traits (whatever they're called) for a ChatClient.
    //And make calls to them.
    public string       PlayerStatus; // Set in inspector
    private int          CurrentChannel; // Set in inspector


    public void DebugReturn(DebugLevel level, string message)
    {
        if (level == DebugLevel.ERROR)
        {
            Debug.LogError(message);
        }
        else if (level == DebugLevel.WARNING)
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.Log(message);
        }
    }


    public void OnChatStateChange(ChatState state)
    {
        //this.StateText.text = state.ToString();
    }


    public void Connect(string userName)
    {
        Debug.Log("ChatHandler Start() test!");
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "US";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.ChatAppID, "0.1", new ExitGames.Client.Photon.Chat.AuthenticationValues(userName));
    }


    public void OnConnected()
    {
        Debug.Log("Connected as: " + chatClient.UserId);
        
        GetCurrentChannel();
        chatClient.Subscribe(GameChannels);
        
    }

    public void GetCurrentChannel()
    {
        string role = (string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles];
        if (role == Global.Role.Civilian || role == Global.Role.Nurse || role == Global.Role.Sheriff)
            PlayerStatus = Global.Role.Civilian;

        if (PlayerStatus == Global.Role.Mafia)
        {
            CurrentChannel = Global.Chats.Mafia;
        }
        else
        {
            CurrentChannel = Global.Chats.Civilian;
        }
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

    
    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatClient.PublishMessage(channels[0],"You may chat");
        Debug.Log("Subscribed to: " + channels[0]);
    }


    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log("Unsubscribed to: " + GameChannels[0]);
    }


    private string formatMessage(string playerStatus, int Channel, string msg)
    {
        string msgFormat;
        StringBuilder txt = new StringBuilder();

        if (playerStatus == "Civilian" || Channel == Global.Chats.Civilian)
        {
            msgFormat = "[ {0} ] {1}";
        }
        else if (playerStatus == "Mafia")
        {
            msgFormat = "<color=red>[{0}]</color> {1}";
        }
        else // Dead Players
        {
            msgFormat = "<color=cyan>[{0}] {1}</color>";
        }
        txt.AppendLine(string.Format(msgFormat, chatClient.UserId, msg));
        return txt.ToString().TrimEnd('\n');
    }


    public void SendChatMessage(/*string channel,*/ string msg)
    {
        if (string.IsNullOrEmpty(msg))
        {
            return;
        }

        for (int i = 0; i < GameChannels.Length; i++)
        {
            if (PlayerStatus != "Dead" || i == Global.Chats.Dead)
                chatClient.PublishMessage(GameChannels[i], formatMessage(PlayerStatus, i, msg)); 
        }
    }

    // Use this for initialization
    void Start ()
    {
        //DontDestroyOnLoad(gameObject);
        Application.runInBackground = true;
	}

    // Update is called once per frame
	void Update () {
        if (this.chatClient != null)
        {
            this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new NotImplementedException();
    }
}

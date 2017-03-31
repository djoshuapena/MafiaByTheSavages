using System;
using System.Collections.Generic;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using UnityEngine.UI;

public class MafiaChatScript : MonoBehaviour, IChatClientListener
{

    public string[] ChannelsToJoinOnConnect; // set in inspector.
    private string selectedChannelName; // mainly used for GUI/input
    //public string[] FriendsList;

    public int HistoryLengthToFetch; // set in inspector. Up to a certain degree, previously sent messages can be fetched for context

    public string UserName { get; set; }

    public ChatClient chatClient;

    public GameObject missingAppIdErrorPanel;

    public GameObject ConnectingLabel;

    public RectTransform ChatPanel;     // set in inspector (to enable/disable panel)
    //public GameObject UserIdFormPanel; //this is where we put the user id, dont need this because it will be imported from previous scene
    public InputField InputFieldChat;   // set in inspector
    public Text CurrentChannelText;     // set in inspector
    //public Toggle ChannelToggleToInstantiate; // set in inspector 

    //public GameObject FriendListUiItemToInstantiate;  // this is where the friend list comes from

    //private readonly Dictionary<string, Toggle> channelToggles = new Dictionary<string, Toggle>(); //no channel toggle, so we dont need this
    //private readonly Dictionary<string, FriendItem> friendListItemLUT = new Dictionary<string, FriendItem>(); //no friend list, we dont need this

    public bool ShowState = true;
    public GameObject Title;
    public Text StateText; // set in inspector, shows the status of the connection
    public Text UserIdText; // set in inspector, shous the users Id on their own screen


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true; // this must run in background or it will drop connection if not focussed.

        UserIdText.text = "";
        StateText.text = "";
        setGameObjectActive(StateText.gameObject, UserIdText.gameObject);

        if(string.IsNullOrEmpty(UserName))
        {
            UserName = "FakeName" + Environment.TickCount % 99; //Make up username
        }

        bool _AppIdPresent = string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.ChatAppID);
        this.missingAppIdErrorPanel.SetActive(_AppIdPresent); //Show error if there is no ChatAppId

        //this.UserIdFormPanel.gameObject.SetActive(!_AppIdPresent);

        if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.ChatAppID))
        {
            Debug.LogError("You need to set the chat app ID in the PhotonServerSettings file in order to continue.");
            return;
        }

        Connect();
    }

    public void Connect()
    {
        Debug.Log("it came here");
        //this.UserIdFormPanel.gameObject.SetActive(false); //dont need this because there is not useridformpanel

        this.chatClient = new ChatClient(this);
        this.chatClient.Connect(PhotonNetwork.PhotonServerSettings.ChatAppID, "0.1", new ExitGames.Client.Photon.Chat.AuthenticationValues(UserName));

        //this.ChannelToggleToInstantiate.gameObject.SetActive(false); //dont need this because there are no channels
        Debug.Log("Connecting as: " + UserName);

        setGameObjectActive(ConnectingLabel);
    }

    /// <summary>
    /// To avoid that the Editor becomes unresponsive, disconnect all Photon connections in OnApplicationQuit.
    /// </summary>
    public void OnApplicationQuit()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }

    #region setGameObjectActive
    /// <summary>
    /// This will set one GameObjectActive.
    /// </summary>
    /// <param name="Object">Game Object that needs to be activated</param>
    private void setGameObjectActive(GameObject Object)
    {
        StateText.gameObject.SetActive(Object == StateText.gameObject);
        UserIdText.gameObject.SetActive(Object == UserIdText.gameObject);
        ChatPanel.gameObject.SetActive(Object == ChatPanel.gameObject);
        ConnectingLabel.SetActive(Object == ConnectingLabel);
    }
	

    /// <summary>
    /// This will set two GameObjects Active
    /// </summary>
    /// <param name="Object">Game Object that needs to be activated</param>
    /// <param name="Object2">Game Object thtat needs to be activated</param>
    private void setGameObjectActive(GameObject Object, GameObject Object2)
    {
        StateText.gameObject.SetActive(Object == StateText.gameObject || Object2 == StateText.gameObject);
        UserIdText.gameObject.SetActive(Object == UserIdText.gameObject || Object2 == UserIdText.gameObject);
        ChatPanel.gameObject.SetActive(Object == ChatPanel.gameObject || Object2 == ChatPanel.gameObject);
        ConnectingLabel.SetActive(Object == ConnectingLabel || Object2 == ConnectingLabel);
    }
    #endregion

    // Update is called once per frame
    void Update () {
        if (this.chatClient != null)
        {
            this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }

        // check if we are missing context, which means we got kicked out to get back to the StartPage.
        if (this.StateText == null)
        {
            Destroy(this.gameObject);
            return;
        }

        this.StateText.gameObject.SetActive(ShowState); // this could be handled more elegantly, but for the demo it's ok.
    }

    /// <summary>
    /// Send a message if the keyboard Return is pressed.
    /// </summary>
    public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            SendChatMessage(this.InputFieldChat.text);
            this.InputFieldChat.text = "";
        }
    }

    /// <summary>
    /// Send a message if the Send button is pressed.
    /// </summary>
    public void OnClickSend()
    {
        if (this.InputFieldChat != null)
        {
            SendChatMessage(this.InputFieldChat.text);
            this.InputFieldChat.text = "";
        }
    }

    public int TestLength = 2048;
    private byte[] testBytes = new byte[2048];

    /// <summary>
    /// This will send a message form the InputField to the Output field
    /// </summary>
    /// <param name="inputLine">String from Input Field that user wants to send</param>
    private void SendChatMessage(string inputLine)
    {
        if (string.IsNullOrEmpty(inputLine))
        {
            return;
        }

        if ("test".Equals(inputLine))
        {
            if (this.TestLength != this.testBytes.Length)
            {
                this.testBytes = new byte[this.TestLength];
            }

            this.chatClient.SendPrivateMessage(this.chatClient.AuthValues.UserId, testBytes, true);
        }

        bool doingPrivateChat = this.chatClient.PrivateChannels.ContainsKey(this.selectedChannelName); // no channels, chat should be to this only.

        string privateChatTarget = string.Empty;
        if (doingPrivateChat)
        {
            // the channel name for a private conversation is (on the client!!) always composed of both user's IDs: "this:remote"
            // so the remote ID is simple to figure out

            string[] splitNames = this.selectedChannelName.Split(new char[] { ':' });
            privateChatTarget = splitNames[1];
        }
        UnityEngine.Debug.Log("selectedChannelName: " + selectedChannelName + " doingPrivateChat: " + doingPrivateChat + " privateChatTarget: " + privateChatTarget);

        if (inputLine[0].Equals('\\'))
        {
            string[] tokens = inputLine.Split(new char[] { ' ' }, 2);
            //if (tokens[0].Equals("\\help"))
            //{
            //    PostHelpToCurrentChannel();
            //}
            if (tokens[0].Equals("\\state"))
            {
                int newState = 0;


                List<string> messages = new List<string>();
                messages.Add("i am state " + newState);
                string[] subtokens = tokens[1].Split(new char[] { ' ', ',' });

                if (subtokens.Length > 0)
                {
                    newState = int.Parse(subtokens[0]);
                }

                if (subtokens.Length > 1)
                {
                    messages.Add(subtokens[1]);
                }

                this.chatClient.SetOnlineStatus(newState, messages.ToArray()); // this is how you set your own state and (any) message
            }
            else if ((tokens[0].Equals("\\subscribe") || tokens[0].Equals("\\s")) && !string.IsNullOrEmpty(tokens[1]))
            {
                this.chatClient.Subscribe(tokens[1].Split(new char[] { ' ', ',' }));
            }
            else if ((tokens[0].Equals("\\unsubscribe") || tokens[0].Equals("\\u")) && !string.IsNullOrEmpty(tokens[1]))
            {
                this.chatClient.Unsubscribe(tokens[1].Split(new char[] { ' ', ',' }));
            }
            else if (tokens[0].Equals("\\clear"))
            {
                if (doingPrivateChat)
                {
                    this.chatClient.PrivateChannels.Remove(this.selectedChannelName);
                }
                else
                {
                    ChatChannel channel;
                    if (this.chatClient.TryGetChannel(this.selectedChannelName, doingPrivateChat, out channel))
                    {
                        channel.ClearMessages();
                    }
                }
            }
            else if (tokens[0].Equals("\\msg") && !string.IsNullOrEmpty(tokens[1]))
            {
                string[] subtokens = tokens[1].Split(new char[] { ' ', ',' }, 2);
                if (subtokens.Length < 2) return;

                string targetUser = subtokens[0];
                string message = subtokens[1];
                this.chatClient.SendPrivateMessage(targetUser, message);
            }
            else if ((tokens[0].Equals("\\join") || tokens[0].Equals("\\j")) && !string.IsNullOrEmpty(tokens[1]))
            {
                string[] subtokens = tokens[1].Split(new char[] { ' ', ',' }, 2);

                // If we are already subscribed to the channel we directly switch to it, otherwise we subscribe to it first and then switch to it implicitly
                //if (channelToggles.ContainsKey(subtokens[0]))
                //{
                //    ShowChannel(subtokens[0]);
                //}
                //else
                //{
                    this.chatClient.Subscribe(new string[] { subtokens[0] });
                //}
            }
            else
            {
                Debug.Log("The command '" + tokens[0] + "' is invalid.");
            }
        }
        else
        {
            if (doingPrivateChat)
            {
                this.chatClient.SendPrivateMessage(privateChatTarget, inputLine);
            }
            else
            {
                this.chatClient.PublishMessage(this.selectedChannelName, inputLine);
            }
        }
    }

    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
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

    public void OnConnected()
    {
        if (this.ChannelsToJoinOnConnect != null && this.ChannelsToJoinOnConnect.Length > 0)
        {
            this.chatClient.Subscribe(this.ChannelsToJoinOnConnect, this.HistoryLengthToFetch);
        }

        ConnectingLabel.SetActive(false);

        UserIdText.text = "Connected as " + this.UserName;

        this.ChatPanel.gameObject.SetActive(true);

        //if (FriendsList != null && FriendsList.Length > 0)
        //{
        //    this.chatClient.AddFriends(FriendsList); // Add some users to the server-list to get their status updates

        //    // add to the UI as well
        //    foreach (string _friend in FriendsList)
        //    {
        //        if (this.FriendListUiItemtoInstantiate != null && _friend != this.UserName)
        //        {
        //            this.InstantiateFriendButton(_friend);
        //        }

        //    }

        //}

        //if (this.FriendListUiItemtoInstantiate != null)
        //{
        //    this.FriendListUiItemtoInstantiate.SetActive(false);
        //}


        this.chatClient.SetOnlineStatus(ChatUserStatus.Online); // You can set your online state (without a mesage).
    }

    public void OnDisconnected()
    {
        ConnectingLabel.SetActive(false);
    }

    public void OnChatStateChange(ChatState state)
    {
        // use OnConnected() and OnDisconnected()
        // this method might become more useful in the future, when more complex states are being used.

        this.StateText.text = state.ToString();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        // in this demo, we simply send a message into each channel. This is NOT a must have!
        //foreach (string channel in channels)
        //{
        //    this.chatClient.PublishMessage(channel, "says 'hi'."); // you don't HAVE to send a msg on join but you could.

        //    if (this.ChannelToggleToInstantiate != null)
        //    {
        //        this.InstantiateChannelButton(channel);

        //    }
        //}

        Debug.Log("OnSubscribed: " + string.Join(", ", channels));

        /*
        // select first subscribed channel in alphabetical order
        if (this.chatClient.PublicChannels.Count > 0)
        {
            var l = new List<string>(this.chatClient.PublicChannels.Keys);
            l.Sort();
            string selected = l[0];
            if (this.channelToggles.ContainsKey(selected))
            {
                ShowChannel(selected);
                foreach (var c in this.channelToggles)
                {
                    c.Value.isOn = false;
                }
                this.channelToggles[selected].isOn = true;
                AddMessageToSelectedChannel(WelcomeText);
            }
        }
        */

        // Switch to the first newly created channel
        //ShowChannel(channels[0]);
    }

    public void AddMessageToSelectedChannel(string msg)
    {
        ChatChannel channel = null;
        bool found = this.chatClient.TryGetChannel(this.selectedChannelName, out channel);
        if (!found)
        {
            Debug.Log("AddMessageToSelectedChannel failed to find channel: " + this.selectedChannelName);
            return;
        }

        if (channel != null)
        {
            channel.Add("Bot", msg);
        }
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

        this.selectedChannelName = channelName;
        this.CurrentChannelText.text = channel.ToStringMessages();
        Debug.Log("ShowChannel: " + this.selectedChannelName);

        //foreach (KeyValuePair<string, Toggle> pair in channelToggles)
        //{
        //    pair.Value.isOn = pair.Key == channelName ? true : false;
        //}
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        throw new NotImplementedException();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new NotImplementedException();
    }
}

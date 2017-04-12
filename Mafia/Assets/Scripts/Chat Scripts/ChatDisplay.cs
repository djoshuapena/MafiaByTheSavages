using System.Collections;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Chat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//DAVID DO NOT 
//CHANGE
//MY SHIT
//IT DOES NOT NEED
//TO BE CHANGED
public class ChatDisplay : Photon.MonoBehaviour {
    public ChatHandler chatHandler; // Set in inspector
    public Text CurrentChannelText; // Set in inspector
    public Text CurrentChannelText2; // Set in inspector
    public GameObject ChatPanel; // Set in inspector
    public GameObject ChatInputPanel; // Set in inspector
    public ScrollRect ChatPanelPosition; // Set in inspector
	public GameObject OpenChatButton; // Set in inspector, used only for testing
    public GameObject SmallerChatPanel; // Set in inspector

    void Start() {}

    //Testing function, not really needed.
    //Could be altered to be an event that happens when a player dies
    /*public void TogglePlayerStatus()
    {
        if (chatHandler.PlayerStatus == "Civilian")
        {
            chatHandler.PlayerStatus = "Mafia";
            chatHandler.CurrentChannel = Global.Chats.Mafia;
            RoleButton.GetComponent<Image>().color = Color.red;
        }
        else if (chatHandler.PlayerStatus == "Mafia")
        {
            chatHandler.PlayerStatus = "Dead";
            chatHandler.CurrentChannel = Global.Chats.Dead;
            RoleButton.GetComponent<Image>().color = Color.magenta;
        }
        else
        {
            chatHandler.PlayerStatus = "Civilian";
            chatHandler.CurrentChannel = Global.Chats.Civilian;
            RoleButton.GetComponent<Image>().color = Color.white;
        }
        RoleButtonText.text = chatHandler.PlayerStatus;
        ShowMessages(chatHandler.GameChannels[chatHandler.CurrentChannel]);
    }*/


    /*public void ToggleChatPanel()
    {
        if (ChatPanel.activeInHierarchy)
        {
            ChatPanel.SetActive(false);
        }
        else
        {
            ChatPanel.SetActive(true);
        }
    }*/

	public void CloseChat()
	{
		ChatPanel.SetActive(false);
		OpenChatButton.SetActive(true);
        SmallerChatPanel.SetActive(true);
	}

	public void OpenChat()
	{
        OpenChatButton.SetActive(false);
        SmallerChatPanel.SetActive(false);
        ChatPanel.SetActive(true);
    }

    public void ToggleChatInput(/*bool status*/)
    {
        if (ChatInputPanel.activeInHierarchy)
        {
            ChatInputPanel.SetActive(false);
        }
        else
        {
            ChatInputPanel.SetActive(true);
        }
    }


    public void ShowMessages(string channelName)
    {
        if (string.IsNullOrEmpty(channelName))
        {
            return;
        }
        ChatChannel channel = null;
        bool found = chatHandler.chatClient.TryGetChannel(channelName, out channel);
        if (!found)
        {
            Debug.Log("ShowChannel failed to find channel: " + channelName);
            return;
        }
        CurrentChannelText.text = channel.ToStringMessages().TrimEnd('\n');
        CurrentChannelText2.text = channel.ToStringMessages().TrimEnd('\n');

    }


    void Update () {
		
	}
}
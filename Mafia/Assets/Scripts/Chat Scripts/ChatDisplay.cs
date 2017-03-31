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
    public GameObject ChatPanel; // Set in inspector
    public GameObject ChatInputPanel; // Set in inspector
    public ScrollRect ChatPanelPosition; // Set in inspector
    public Button RoleButton; // Set in inspector, used only for testing
    public Text RoleButtonText; // Set in inspector

    void Start() {}

    //Testing function, not really needed.
    //Could be altered to be an event that happens when a player dies
    public void TogglePlayerStatus()
    {
        if (chatHandler.PlayerStatus == "Civilian")
        {
            chatHandler.PlayerStatus = "Mafia";
            chatHandler.CurrentChannel = Global.Mafia;
            RoleButton.GetComponent<Image>().color = Color.red;
        }
        else if (chatHandler.PlayerStatus == "Mafia")
        {
            chatHandler.PlayerStatus = "Dead";
            chatHandler.CurrentChannel = Global.Dead;
            RoleButton.GetComponent<Image>().color = Color.magenta;
        }
        else
        {
            chatHandler.PlayerStatus = "Civilian";
            chatHandler.CurrentChannel = Global.Civilian;
            RoleButton.GetComponent<Image>().color = Color.white;
        }
        RoleButtonText.text = chatHandler.PlayerStatus;
        ShowMessages(chatHandler.GameChannels[chatHandler.CurrentChannel]);
    }


    public void ToggleChatPanel(/*bool status*/)
    {
        if (ChatPanel.activeInHierarchy)
        {
            ChatPanel.SetActive(false);
        }
        else
        {
            ChatPanel.SetActive(true);
        }
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
        CurrentChannelText.text = channel.ToStringMessages();
        ChatPanelPosition.verticalNormalizedPosition = 0;
    }


    void Update () {
		
	}
}
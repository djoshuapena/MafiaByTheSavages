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

}
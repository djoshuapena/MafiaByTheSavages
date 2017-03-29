using System.Collections;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Chat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDisplay : Photon.MonoBehaviour {
    public ChatHandler chatHandler;
    public Text CurrentChannelText;
    public GameObject panel;
    public ScrollRect x;
    public Button RoleButton;
    public Text RoleButtonText;

    void Start() {}

    public void TogglePlayerStatus()
    {
        if (chatHandler.PlayerStatus == "Civilian")
        {
            chatHandler.PlayerStatus = "Mafia";
            chatHandler.CurrentChannel = 1;
            
            RoleButton.GetComponent<Image>().color = Color.red;
        }
        else if (chatHandler.PlayerStatus == "Mafia")
        {
            chatHandler.PlayerStatus = "Dead";
            chatHandler.CurrentChannel = 2;
            
            RoleButton.GetComponent<Image>().color = Color.magenta;
        }
        else
        {
            chatHandler.PlayerStatus = "Civilian";
            chatHandler.CurrentChannel = 0;
            RoleButton.GetComponent<Image>().color = Color.white;
        }
        RoleButtonText.text = chatHandler.PlayerStatus;
        ShowMessages(chatHandler.GameChannels[chatHandler.CurrentChannel]);
    }


    public void ToggleChat()
    {
        if (panel.activeInHierarchy)
            panel.SetActive(false);
        else
            panel.SetActive(true);
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

        CurrentChannelText.text = channel.ToStringMessages();//.TrimEnd('\n');
        x.verticalNormalizedPosition = 0;
    }

    void Update () {
		
	}
}

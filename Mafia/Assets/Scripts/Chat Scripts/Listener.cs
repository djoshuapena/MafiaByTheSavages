//using System.Collections;
//using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Listener : MonoBehaviour {

    public ChatHandler chatObject;
    public InputField InputFieldChat;
 
    void Start () {}

    // Sends the message currently in the input field
    // Empties the field
    public void SendMessage()
    {
        if (chatObject.PlayerStatus == "Dead")
        {
            chatObject.SendChatMessage(chatObject.GameChannels[2], formatMessage(2).TrimEnd('\n'));
        }
        else
        {
            for (int i = 0; i < chatObject.GameChannels.Length; i++)
            {
                chatObject.SendChatMessage(chatObject.GameChannels[i], formatMessage(i).TrimEnd('\n'));
            }
        }
        InputFieldChat.text = "";
        InputFieldChat.ActivateInputField();
    }


    // Update is called once per frame
    void Update () {
        if (/*InputFieldChat.isFocused && */InputFieldChat.text != "" && Input.GetKey(KeyCode.Return))
        {
            SendMessage();
        }
    }


    private string formatMessage(int ArrayIndexOfChannel)
    {
        StringBuilder txt = new StringBuilder();
        if (chatObject.PlayerStatus == "Civilian" || ArrayIndexOfChannel == 0)
        {
            txt.AppendLine(string.Format("{0}: {1}", chatObject.chatClient.UserId, InputFieldChat.text));
        }
        else if (chatObject.PlayerStatus == "Mafia")
        {
            txt.AppendLine(string.Format("<color=red>{0}</color>: {1}", chatObject.chatClient.UserId, InputFieldChat.text));     
        }
        else
        {
            txt.AppendLine(string.Format("<color=purple>{0}: {1}</color>", chatObject.chatClient.UserId, InputFieldChat.text));
        }
        return txt.ToString();
    }

}

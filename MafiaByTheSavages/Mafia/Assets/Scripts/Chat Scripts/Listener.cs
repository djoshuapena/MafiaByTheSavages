using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Listener : MonoBehaviour {

    public ChatHandler chatHandler;
    public InputField InputFieldChat;
 
    void Start () {
		
	}

    public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            chatHandler.SendChatMessage(InputFieldChat.text);
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
            chatHandler.SendChatMessage(InputFieldChat.text);
            InputFieldChat.text = "";
        }
    }

    // Update is called once per frame
    void Update () {
	}
}

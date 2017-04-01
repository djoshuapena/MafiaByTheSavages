using UnityEngine;
using UnityEngine.UI;

//DAVID DO NOT 
//CHANGE
//MY SHIT
//IT DOES NOT NEED
//TO BE CHANGED
public class Listener : MonoBehaviour {
    public ChatHandler chatObject;
    public InputField InputFieldChat;
 
    void Start () {}


    // Sends the message currently in the input field
    // Empties the field
    public void SendMessage()
    {
        chatObject.SendChatMessage(InputFieldChat.text); 
        InputFieldChat.text = "";
        InputFieldChat.ActivateInputField();
    }


    // Update is called once per frame
    void Update ()
    {
        if (InputFieldChat.IsActive() && InputFieldChat.text != "" && Input.GetKey(KeyCode.Return))
        {
            SendMessage();
        }
    }
}

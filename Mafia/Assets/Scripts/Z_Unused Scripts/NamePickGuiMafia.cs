using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (MafiaChatScript))]
public class NamePickGuiMafia : MonoBehaviour
{
    private const string UserNamePlayerPref = "NamePickUserName";

    public MafiaChatScript chatNewComponent;

    public InputField idInput;

    public void Start()
    {
        this.chatNewComponent = FindObjectOfType<MafiaChatScript>();


        string prefsName = PlayerPrefs.GetString(NamePickGuiMafia.UserNamePlayerPref);
        if (!string.IsNullOrEmpty(prefsName))
        {
            this.idInput.text = prefsName;
        }
    }


    // new UI will fire "EndEdit" event also when loosing focus. So check "enter" key and only then StartChat.
    public void EndEditOnEnter()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            this.StartChat();
        }
    }

    public void StartChat()
    {
        Debug.Log("it came here");
        MafiaChatScript chatNewComponent = FindObjectOfType<MafiaChatScript>();
        chatNewComponent.UserName = this.idInput.text.Trim();
		chatNewComponent.Connect();
        enabled = false;

        PlayerPrefs.SetString(NamePickGuiMafia.UserNamePlayerPref, chatNewComponent.UserName);
    }
}
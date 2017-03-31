using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour {
    public Text outputText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OutputMessage()
    {
        if (PhotonNetwork.isMasterClient)
        {
            outputText.text = "Hello from the Master, I am " + PhotonNetwork.playerName;
        }
        else
        {
            outputText.text = "Hello from " + PhotonNetwork.playerName;
        }
    }

}

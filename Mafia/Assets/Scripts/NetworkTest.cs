using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTest : MonoBehaviour {
    public GameObject msg;
    private PhotonView photonView;

    // Use this for initialization
    void Start () {
        photonView = gameObject.GetComponent<PhotonView>();
        msg.SetActive(false);
	}

    public void buttonPress()
    {
        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("showMessage", PhotonTargets.All);
        }
        
    }
	
    [PunRPC]
    public void showMessage()
    {
            if (msg.activeInHierarchy)
                msg.SetActive(false);
            else
                msg.SetActive(true);
    }
    
	// Update is called once per frame
	void Update () {
	}
}

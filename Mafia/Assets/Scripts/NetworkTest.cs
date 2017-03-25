using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTest : Photon.MonoBehaviour{
    public GameObject msg;
    private PhotonView myPhotonView;

    // Use this for initialization
    void Start () {
        myPhotonView = gameObject.GetComponent<PhotonView>();
        msg.SetActive(false);
	}

    public void buttonPress()
    {
        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("showMessage", PhotonTargets.All);
        }
        
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting == true)
        {
          
        }
        else
        {

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

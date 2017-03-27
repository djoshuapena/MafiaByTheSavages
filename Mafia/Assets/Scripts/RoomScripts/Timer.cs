using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : Photon.PunBehaviour//MonoBehaviour
{
    private List<GameObject> playerNamePrefabs = new List<GameObject>();

    private PhotonView myPhotonView;

    private int minPlayers = 2;

    float timeLeft = 30.0f;

    public Text timer;


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting == true)
        {
            stream.SendNext(timeLeft);
            if (timeLeft < 1)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else
        {
            timeLeft = (float)stream.ReceiveNext();
            ShowTime();
            if (timeLeft < 1)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    void Start()
    {
        myPhotonView = gameObject.GetComponent<PhotonView>();

        if (PhotonNetwork.isMasterClient == true) // host starts the countdown
        {
            //timeLeft = 30.0f;
            myPhotonView.RPC("Countdown", PhotonTargets.AllBuffered, PhotonNetwork.time);
        }
    }

    [PunRPC]
    void Countdown(double timerStart)
    {
        timeLeft -= (float)(timerStart - PhotonNetwork.time);
        StartCoroutine("TimerCountdown");
    }

    IEnumerator TimerCountdown()
    {
        while (timeLeft > 0f)
        {
            yield return new WaitForEndOfFrame();
            if (PhotonNetwork.isMasterClient == true)
            {
                timeLeft -= Time.deltaTime;
            }
            ShowTime();
        }

    }

    void ShowTime()
    {
        timer.text = "Time Left:" + Mathf.Round(timeLeft);
    }
}
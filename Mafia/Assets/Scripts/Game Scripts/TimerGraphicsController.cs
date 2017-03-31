using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerGraphicsController : Photon.PunBehaviour//MonoBehaviour
{
    //private List<GameObject> playerNamePrefabs = new List<GameObject>();

    private PhotonView myPhotonView;

    //private int minPlayers = 2;

    private float timeLeft = -1.0f;

    public Text timer;

    //function that synchronizes the timer for client and server
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //This is for the master. Only he can update the time.
        if (stream.isWriting == true)
        {
            stream.SendNext(timeLeft);
            if (timeLeft < 1)
            {
                //Debug.Log("Time is up");
            }
        }
        //This is for everyone else to just read the time.
        else
        {
            timeLeft = (float)stream.ReceiveNext();
            ShowTime();
            if (timeLeft < 1)
            {
                //Debug.Log("Time is up");
            }
        }
    }

    public void Countdown()
    {
        myPhotonView = gameObject.GetComponent<PhotonView>();

        if (PhotonNetwork.isMasterClient == true) // host starts the countdown
        {
            myPhotonView.RPC("Countdown1", PhotonTargets.AllBuffered, PhotonNetwork.time);
        }
    }

    //public function to start the time
    public void InitializeTime(int time)
    {
        timeLeft = time;
    }

    public void test()
    {
        InitializeTime(45);
    }

    //public synchronized function that starts the countdown based on the timeRemaining
    [PunRPC]
    public void Countdown1(double timerStart)
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

    //shows the time remaining
    void ShowTime()
    {
        timer.text = "Time Left:" + Mathf.Round(timeLeft);
    }

    //public function to show the time
    public void Activate()
    {
        timer.gameObject.SetActive(true);
    }

    //public function to hide the time
    public void Deactivate()
    {
        timer.gameObject.SetActive(false);
    }
}
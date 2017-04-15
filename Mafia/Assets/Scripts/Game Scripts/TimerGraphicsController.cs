using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerGraphicsController : Photon.PunBehaviour//MonoBehaviour
{
    public GameController game;
    //private PhotonView myPhotonView;
    private float outtime;
    public Text timer;
    //private bool active = false;


    //function that synchronizes the timer for client and server
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //This is for the master. Only he can update the time.
        if (stream.isWriting == true)
        {
            if (outtime > 0)
            {
                stream.SendNext(outtime);
                ShowTime(outtime);
            }
            ////ShowTime();
            //if (timeLeft < 1)
            //{
            //    //Debug.Log("Time is up");
            //}
        }
        //This is for everyone else to just read the time.
        else
        {
            outtime = (float)stream.ReceiveNext();
            ShowTime(outtime);
            //if (timeLeft < 1)
            //{
            //    //Debug.Log("Time is up");
            //}
        }
    }

    public void Countdown(string state, float time)
    {
		//ShowTime ();
        //myPhotonView = gameObject.GetComponent<PhotonView>();
        Debug.Log("the timer is started");
        if (PhotonNetwork.isMasterClient) // host starts the countdown
        {
            //active = true;
            photonView.RPC("Countdown1", PhotonTargets.AllBuffered, PhotonNetwork.time, state, time);
        }
    }

    //public function to start the time
    //public void InitializeTime(int time)
    //{
    //    timeLeft = time;
    //}

    //public void test()
    //{
    //    InitializeTime(45);
    //}

    //public synchronized function that starts the countdown based on the timeRemaining
    [PunRPC]
    public void Countdown1(double timerStart, string state, float time)
    {
        Debug.Log("I Came here afterwards");
        float timeLeft = time - (float)(timerStart - PhotonNetwork.time);
        object[] param = new object[2] { state, timeLeft };
        StartCoroutine("TimerCountdown", param);
    }

    IEnumerator TimerCountdown(object[] param)
    {
        float timeLeft = (float)param[1];
        string state = (string)param[0];
        //Debug.Log(timeLeft);
        while (timeLeft > 0f)
        {
            //Debug.Log(timeLeft);
            yield return new WaitForEndOfFrame();
            if (PhotonNetwork.isMasterClient == true)
            {
                timeLeft = timeLeft - Time.deltaTime;
            }
            outtime = timeLeft;
            //ShowTime(outtime);
        }
        Debug.Log(state);
        FunctionDone timeup = new FunctionDone(game.EndingState);
        timeup(state);
        //game.EndingState(state);
        //TimeUP();

    }

    //public bool TimeUP()
    //{
    //    if(timeLeft <= 0)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //shows the time remaining
    public void ShowTime(float timeLeft)
    {
        timer.text = Mathf.Round(timeLeft).ToString();
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
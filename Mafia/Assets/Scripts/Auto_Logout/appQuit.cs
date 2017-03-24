using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appQuit : MonoBehaviour {

    //the timer variable
    //float timeLeft = 10.0f;
    // bool isPaused = false;
    public LogoutAccount logout;
    public static appQuit instance = null;


    //Awake is always called before any Start functions
    void Awake()
    {
       //Application.runInBackground = true;
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this) //If instance already exists and it's not this:
        {
            //Then destroy this because it enforces there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
       /* //why is this code not running?
        if(isPaused)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                logout.onLogout();
                isPaused = false;
            }
        }*/
    }

    //unity function that checks to see if the application is paused
    void OnApplicationPause()
    {
        /*if (!pauseState)
        {
            Debug.Log(pauseState);
            //start the timer
            StartCoroutine(StartCountdown());
            logout.onLogout();
        }
        Application.runInBackground = false;*/
        /*isPaused = pauseStatus;
        Debug.Log("Pause status: " + isPaused);*/
<<<<<<< Updated upstream
       logout.onLogout();
=======
<<<<<<< HEAD
        logout.onLogout();
=======
       logout.onLogout();
>>>>>>> origin/master
>>>>>>> Stashed changes
    }

    //unity function that checks to see if the application is paused(for windows and when the keyboard is pulled up)
    void OnApplicationFocus()
    {
        //if (pauseState)
        //{
        //Debug.Log(pauseState);
        //start the timer
        //StartCoroutine(StartCountdown());
        //logout.onLogout();
        // }
        /*isPaused = !hasFocus;*/
        logout.onLogout();
    }

    //unity function that checks to see if the application quit
    void OnApplicationQuit()
    {
        logout.onLogout();
    }

    /*//the timer
    private void StartCountdown(int currCountdownValue)
    {
        //currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            //yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        logout.onLogout();
    }*/


    //code for the timer
    /*   public Text counterText;
         private int counterValue;
 
         // Use this for initialization
         void Start ()
         {
                 Application.runInBackground=true;
                 StartCoroutine ("StartCounter");
         }
     
         IEnumerator StartCounter ()
         {
                 yield return new WaitForSeconds (1f);
                 counterText.text = "Counter : " + counterValue.ToString ();
                 counterValue++;
                 StartCoroutine ("StartCounter");
         }*/
}

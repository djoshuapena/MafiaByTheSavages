using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appPaused : MonoBehaviour {

    public bool isPaused;

    // Use this for initialization
    void Start () {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public function to get the isPaused status
    public bool getPauseStatus()
    {
        return isPaused;
    }

    //public function for scripts to call OnApplicationFocus
    public void callAppFocus(bool focus)
    {
        OnApplicationFocus(focus);
    }

    //public function for scripts to call OnApplicationPause
    public void callAppPause(bool pause)
    {
        OnApplicationPause(pause);
    }

    //prints to the screen that the game is paused
    void OnGUI()
    {
        if (isPaused)
            //GUI.Label(new Rect(100, 100, 50, 30), "Game paused");
            Debug.Log("Game paused");
    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        //isPaused = pauseStatus;
        Debug.Log(pauseStatus);
    }
}

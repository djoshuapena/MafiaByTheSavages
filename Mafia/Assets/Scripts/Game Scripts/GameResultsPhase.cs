using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultsPhase : MonoBehaviour {

    public GameObject gameResultsPanel;
    public Button endButton;
    public GameController game;

    /* ==========================================================
     *              FUTURE EXTENSION TO THIS CLASS
     *  Get the games story from the GameController class.
     *  Game Results Phase will have a scrolling box that shows
     *  when people were killed, saved, arrested, sent to trial
     *  ect. Possible extension to this is to show who voted for
     *  who.
     * ==========================================================
    */

    /// <summary>
    /// Make sure that the panel is loaded and the button is on
    /// the panel.
    /// </summary>
    /// <returns>Whether or not it works.</returns>
    public bool initializeGameResults()
    {
        if (gameResultsPanel == null)
            throw new Exception("I do not have Game Results Panel");
        if (endButton == null)
            throw new Exception("I do not have a button to end the game with");

        return true;
    }

    /// <summary>
    /// Activate the GameResults panel
    /// </summary>
    /// <returns>Whether or not it works</returns>
    public bool StartGameResults()
    {
        gameResultsPanel.SetActive(true);
        if (gameResultsPanel.activeInHierarchy)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Tell the game controller to end the game.
    /// </summary>
    public void OnEndButtonPress()
    {
        game.Endgame();
    }
}

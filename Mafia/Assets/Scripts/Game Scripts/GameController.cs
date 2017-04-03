using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : Photon.MonoBehaviour
{

    public EndedController end;
    public AssignRoles assign;
    public EndGameController endGame;
    //public ChatGraphicsController chat;
    public VoteController vote;
    public TimerGraphicsController timer;
    //public GameResultsPhase gameResults; //Need to build this
    public OverlayGraphicsController overlay;
    public TrialGraphicsController trial;
    public FlavorText flavorText;
    public NightDayController dayNight; //Waiting for this

    private Dictionary<string, string> text;
    private List<string> trialplayers;
    private string state = "";
    public Text output;
    private string playerVotedFor;

    // Use this for initialization
    void Start()
    {
        InitializeGameStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// The master client will assign the players roles and names, and get the flavor text for the games theme.
    /// It will then send a message out to all other players to start the game.
    /// </summary>
    private void InitializeGameStart()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if(!assign.InitializeRoles())
            {
                throw new Exception("Could not initialize roles");
            }

            text = flavorText.InitializeFlavorTextDict();
            if (text != null)
            {
                photonView.RPC("SaveFlavorText", PhotonTargets.All, text);
            }
            else
            {
                throw new Exception("Dictionary is empty, cannot initialize");
            }
            state = Global.States.Dusk;
            photonView.RPC("ChangeGameState", PhotonTargets.All, state);
        }
    }

    /// <summary>
    /// Change to the new game state.
    /// </summary>
    /// <param name="state">Game state to change to.</param>
    [PunRPC]
    public void ChangeGameState(string state)
    {
        if (state == "")
        {
            throw new Exception("Change Game state didnt get anything.");
        }
        Debug.Log("I am changing the state to" + state);
        PreStateInitialization(state);
    }

    [PunRPC]
    public void SaveFlavorText(Dictionary<string, string> text)
    {
        if(text == null)
        {
            throw new Exception("Save flavor text did not get any dictionary");
        }
        flavorText.flavorTextDict = text;
    }

    private void PreStateInitialization(string state)
    {
        bool initialized = false;
        if (state.Equals(""))
            throw new Exception("I did not get a state to initialize");

        else if (state == Global.States.Night)
        {
            timer.InitializeTime(45);
            initialized = dayNight.InitializeView(Global.States.Night);
        }
        //else if (state == Global.States.Day)
        //{
        //    timer.InitializeTime(45);
        //    initialized = dayNight.InitializeView(Global.States.Day);
        //}
        //else if (state == Global.States.Trial)
        //{
        //    timer.InitializeTime(30);
        //    initialized = trial.InitializeTrial(trialplayers);
        //}
        else
        {
            initialized = overlay.InitializeOverlay(state);
        }
        if (!initialized)
            throw new Exception("I did not initialize any prestates.");
    }

    public void NowStartState(string state)
    {
        StartState(state);
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.player.CustomProperties[Global.CustomProperties.Name].ToString());
        GUILayout.Label(PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles].ToString());
        GUILayout.Label(PhotonNetwork.player.CustomProperties[Global.CustomProperties.VotedFor].ToString());
        GUILayout.Label(PhotonNetwork.player.CustomProperties[Global.CustomProperties.Dead].ToString());
    }

    private void StartState(string state)
    {
        bool started = false;
        if (state.Equals(""))
            throw new Exception("I did not get a state to initialize");

        else if (state == Global.States.Night)
        {
            vote.InitializeVotes();
            started = dayNight.StartView(Global.States.Night);
        }
        else if (state == Global.States.Day)
        {
            vote.InitializeVotes();
            started = dayNight.StartView(Global.States.Day);
        }
        else if (state == Global.States.Trial)
        {
            vote.InitializeVotes();
            trial.StartTrial();
        }
        else
        {
            started = overlay.ShowOverlay(state);
            //if (PhotonNetwork.isMasterClient)
            //    photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(state));
        }
        if (!started)
            throw new Exception("I did not initialize any prestates.");

    }

    public void EndingState(string state)
    {
        if (state == "")
            throw new Exception("I did not get a state to end");

        else if(state == Global.States.Night)
        {
            return;
        }
        else if(state == Global.States.Trial)
        {
        }
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(state));
                EndedState(state);
            }
        }
    }

    private void EndedState(string state)
    {
        state = Global.NextStates.Next(state);
        StartState(state);
    }

    public void Endgame()
    {
        end.ActivateEnd();   
    }
    #region code_graveyard
    //public CanvasController canvas; //Dont need this
    //private void BroadcastMessageToAll(string message, object item)
    //{
    //    photonView.RPC(message, PhotonTargets.All, item);
    //}

    //if (PhotonNetwork.isMasterClient)
    //{
    //    for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
    //    {
    //        Debug.Log(PhotonNetwork.playerList[i].CustomProperties["Name"]);
    //        Debug.Log(PhotonNetwork.playerList[i].CustomProperties["roles"]);
    //        Debug.Log(PhotonNetwork.playerList[i].CustomProperties["VotedFor"]);
    //        Debug.Log(PhotonNetwork.playerList[i].CustomProperties["Dead"]);
    //        Debug.Log("");
    //    }
    //}
    //output.text = "My name is " + PhotonNetwork.player.CustomProperties["Name"] + "\nMy role is " + PhotonNetwork.player.CustomProperties["roles"] + "\nI voted for " + PhotonNetwork.player.CustomProperties["VotedFor"] + "\nAnd I am dead?" + PhotonNetwork.player.CustomProperties["Dead"];
    #endregion
}
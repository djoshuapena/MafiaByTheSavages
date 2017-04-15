using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : Photon.MonoBehaviour
{

    public EndedController end;
    public AssignRoles assign;
    public EndGameController isEndGame;
    public VoteController vote;
    public TimerGraphicsController timer;
    public OverlayGraphicsController overlay;
    public TrialGraphicsController trial;
    public FlavorText flavorText;
    public NightDayController dayNight;
    //public ChatGraphicsController chat;
    //public GameResultsPhase gameResults; //Need to build this

    private Dictionary<string, string> text;
    private List<string> trialplayers;
    private string state = "";
    private string playerVotedFor;
    private float time = 2f;

    // Use this for initialization
    void Start()
    {
        InitializeGameStart();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //stream.SendNext(text);
        }

        else
        {
        //{
        //    text = (dicti)stream.ReceiveNext();
        //    ShowTime();
        //    if (timeLeft < 1)
        //    {
        //        //Debug.Log("Time is up");
        //    }
        }
    }

    /// <summary>
    /// The master client will assign the players roles and names, 
    /// get the flavor text for the game version and send it to
    /// all players to initilize. It will then tell all players to
    /// initilize their own NightDay game state.
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
            //if (text != null)
            //{
            //    photonView.RPC("SaveFlavorText", PhotonTargets.AllBuffered, text);
            //}
            //else
            //{
            //    throw new Exception("Dictionary is empty, cannot initialize");
            //}
        }
        photonView.RPC("SaveFlavorText", PhotonTargets.AllBuffered, text);
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


    /// <summary>
    /// Save the flavor text pulled from the database in the
    /// flavorTextDict dictionary.
    /// </summary>
    /// <param name="text"></param>
    [PunRPC]
    public void SaveFlavorText(Dictionary<string, string> text)
    {
        if(text == null)
        {
            throw new Exception("Save flavor text did not get any dictionary");
        }
        flavorText.flavorTextDict = text;
        StartCoroutine(justwaitamoment());
    }

    IEnumerator justwaitamoment()
    {
        yield return new WaitForSeconds(time);
        if(PhotonNetwork.isMasterClient)
            photonView.RPC("InitializeNightDay", PhotonTargets.AllBuffered);
    }

    /// <summary>
    /// Initialize the view of each state to prepare them before they
    /// show on the GUI. Get the time that is needed for the state ready.
    /// If the previous state had votes, get those vote results.
    /// </summary>
    /// <param name="state"></param>
    private void PreStateInitialization(string state)
    {
        bool initialized = false;
        if (state.Equals(""))
            throw new Exception("I did not get a state to initialize");

        else if (state == Global.States.Night)
        {
            trialplayers = vote.GetVote(1);
            //timer.InitializeTime(45);
            initialized = dayNight.InitializeView(Global.States.Night, trialplayers);
        }
        else if (state == Global.States.Day)
        {
            trialplayers = vote.GetSheriffArrest();
            if (vote.GetMafiaKill() != "")
            {
                if (vote.GetNurseSave() != vote.GetMafiaKill())
                    trialplayers.Add(vote.GetMafiaKill());
            }
            //timer.InitializeTime(45);
            initialized = dayNight.InitializeView(Global.States.Day, trialplayers);
        }
        else if (state == Global.States.Trial)
        {
            trialplayers = vote.GetVote(2);
            initialized = trial.InitializeTrial(trialplayers);
        }
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
        GUILayout.Label((text == null).ToString());
    }

    public void StartState(string state)
    {
        bool started = false;
        if (state.Equals(""))
            throw new Exception("I did not get a state to initialize");

        else if (state == Global.States.Night)
        {
            vote.InitializeVotes();
            trialplayers = null;
            started = dayNight.StartView(state);
        }
        else if (state == Global.States.Day)
        {
            vote.InitializeVotes();
            trialplayers = null;
            started = dayNight.StartView(state);
        }
        else if (state == Global.States.Trial)
        {
            vote.InitializeVotes();
            trial.StartTrial();
        }
        else
        {
            started = overlay.ShowOverlay(state);
        }
        if (!started)
            throw new Exception("I did not initialize any prestates.");

    }

    public void EndingState(string state)
    {
        if (state == "")
            throw new Exception("I did not get a state to end");

        // if the state is morning and all civilians are dead, end the game.
        else if (state == Global.States.Morning)
        {
            PhotonPlayer player;

            //get the list of players killed by sheriff
            List<string> playersKilled = vote.GetSheriffArrest();

            //get the player that was killed by mafia unless
            //that person was saved by the nurse.
            if (vote.GetMafiaKill() != "")
            {
                if (vote.GetNurseSave() != vote.GetMafiaKill())
                    playersKilled.Add(vote.GetMafiaKill());
            }

            //set all players in the playersKilled list to dead.
            if (PhotonNetwork.isMasterClient)
            {
                for (int pos = 0; pos < playersKilled.Count; pos++)
                {
                    player = findPlayer(playersKilled[pos]);
                    player.CustomProperties[Global.CustomProperties.Dead] = true;
                }
            }

            //if all civilians or all mafia are dead, end the game
            if (!isEndGame.CivilianAlive() || !isEndGame.MafiaAlive())
                Endgame();

            else
            {
                if (PhotonNetwork.isMasterClient)
                {
                    photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(state));
                    EndedState(state);
                }
            }
        }
        else if (state == Global.States.PreTrial)
        {
            if(vote.GetVote(1).Count == 0)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(Global.States.Trial));
                    EndedState(Global.States.Trial);
                }
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
        // if the state is post trial, check if someone was hanged in the trial.
        // If that player is a sheriff, check if they are the last sheriff. If
        // so, then end the game. If the player is the last mafia, or the last
        // civilian end the game. Otherwise continue the next state.
        else if (state == Global.States.PostTrial)
        {
            string playerKilled;
            PhotonPlayer player = null;
            //Find out who was killed
            if (vote.GetVote(1).Count != 0)
            {
                playerKilled = vote.GetVote(1)[0];
                player = findPlayer(playerKilled);
                if (PhotonNetwork.isMasterClient)
                    player.CustomProperties[Global.CustomProperties.Dead] = true;
            }

            //Find the photon player that is connected to that name
            //PhotonPlayer player = findPlayer(playerKilled);

            //Set the photon player to dead
            //if(PhotonNetwork.isMasterClient)
            //    player.CustomProperties[Global.CustomProperties.Dead] = true;

            //check if last sheriff.
            if ((player != null) && ((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Sheriff) && !isEndGame.SheriffAlive())
            {
                Endgame();
            }

            //check if last Mafia, or Civilian
            else if (!isEndGame.MafiaAlive() || !isEndGame.CivilianAlive())
            {
                Endgame();
            }

            //Continue to next state.
            else
            {
                if (PhotonNetwork.isMasterClient)
                {
                    photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(state));
                    EndedState(state);
                }
            }
        }

        //Continue to the next state.
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(state));
                EndedState(state);
            }
        }
    }

    private PhotonPlayer findPlayer(string player)
    {
        for (int playerNum = 0; playerNum < PhotonNetwork.playerList.Length; playerNum++)
        {
            if ((string)PhotonNetwork.playerList[playerNum].CustomProperties[Global.CustomProperties.Name] == player)
            {
                return PhotonNetwork.playerList[playerNum];
            }
        }
        // if no player is found, return -1.
        return null;
    }


    #region NightDay_Initialization
    /// <summary>
    /// Initialize the NightDay panel for each individual
    /// players game states.
    /// </summary>
    [PunRPC]
    private void InitializeNightDay()
    {
        StartCoroutine(WaitForInitialization());
    }

    /// <summary>
    /// Wait for the NightDay panel to finish initialization.
    /// </summary>
    /// <returns>False if initilization is not yet finished.</returns>
    IEnumerator WaitForInitialization()
    {
        yield return !dayNight.StartGameInitialize();
        if(PhotonNetwork.isMasterClient)
        {
            state = Global.States.Dusk;
            photonView.RPC("ChangeGameState", PhotonTargets.AllBuffered, state);
        }
    }
    #endregion

    private void EndedState(string state)
    {
        Debug.Log(state + " is ended.");
        state = Global.NextStates.Next(state);
        StartState(state);
    }

    /// <summary>
    /// 
    /// </summary>
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
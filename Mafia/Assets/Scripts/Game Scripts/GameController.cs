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
    public GameObject myname;
    public GameObject myrole;

    //public Connect chat;
    //public ChatGraphicsController chat;
    //public GameResultsPhase gameResults; //Need to build this

    private Dictionary<string, string> text;
    private List<string> playersKilled = new List<string>();
    //private List<string> trialplayers;
    //private string state = "";
    //private string playerVotedFor;
    //private float time = 2f;

    // Use this for initialization
    void Start()
    {
        InitializeGameStart();
        //chat.Co
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
                //throw new Exception("Could not initialize roles");
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
            photonView.RPC("SaveFlavorText", PhotonTargets.All, text);
            photonView.RPC("InitializeNightDay", PhotonTargets.All);
            photonView.RPC("InitializeGameState", PhotonTargets.All, Global.States.Dusk);
        }
       // myname.GetComponent<Text>().text = (string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Name];
       // myrole.GetComponent<Text>().text = (string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles];
    }

    /// <summary>
    /// Change to the new game state.
    /// </summary>
    /// <param name="state">Game state to change to.</param>
    [PunRPC]
    public void InitializeGameState(string state)
    {
        if(trial.trialPanel.GetActive())
        {
            trial.SetTrialtoInactive();
        }
        if (state == "")
        {
            //throw new Exception("Change Game state didnt get anything.");
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
        //if(text == null)
        //{
        //    throw new Exception("Save flavor text did not get any dictionary");
        //}
        flavorText.flavorTextDict = text;
        //StartCoroutine(justwaitamoment());
    }

    //IEnumerator justwaitamoment()
    //{
    //    yield return new WaitForSeconds(time);
    //    if(PhotonNetwork.isMasterClient)
    //        photonView.RPC("InitializeNightDay", PhotonTargets.All);
    //}

    /// <summary>
    /// Initialize the view of each state to prepare them before they
    /// show on the GUI. Get the time that is needed for the state ready.
    /// If the previous state had votes, get those vote results.
    /// </summary>
    /// <param name="state"></param>
    private void PreStateInitialization(string state)
    {
        bool initialized = false;
        //if (state.Equals(""))
            //throw new Exception("I did not get a state to initialize");

        if (state == Global.States.Night)
        {
            //Get the player who was killed in the trial. Will return an
            //empty list if there was none.
            //List<string> playerKilledInTrial = vote.GetVote(1);
            ////timer.InitializeTime(45);
            //initialized = dayNight.InitializeView(Global.States.Night, playerKilledInTrial);
            //Get the player who was killed in the trial. Will return an
            //empty list if there was none.
            //playersKilled.AddRange(vote.GetVote(1));
            //timer.InitializeTime(45);
            initialized = dayNight.InitializeView(Global.States.Night, playersKilled);
        }
        else if (state == Global.States.Day)
        {
            //Get the list of players who were arrested by the sheriff, and
            //killed by the mafia.
            //List<string> selectedPlayers = vote.GetSheriffArrest();
            //playersKilled.AddRange(vote.GetSheriffArrest());
            //if (vote.GetMafiaKill() != "")
            //{
            //    if (vote.GetNurseSave() != vote.GetMafiaKill())
            //        playersKilled.Add(vote.GetMafiaKill());
            //        //selectedPlayers.Add(vote.GetMafiaKill());
            //}
            //timer.InitializeTime(45);
            initialized = dayNight.InitializeView(Global.States.Day, playersKilled);
        }
        else if (state == Global.States.Trial)
        {
            //Get the list of players who will go to trial.
            List<string> trialplayers = vote.GetVote(2);
            Debug.Log("This is the list of players going to trial " + string.Join(",", trialplayers.ToArray()));
            initialized = trial.InitializeTrial(trialplayers);
        }
        else
        {
            initialized = overlay.InitializeOverlay(state);

        }
        //if (!initialized)
        //  throw new Exception("I did not initialize any prestates.");
    }

    public void NowStartState(string state)
    {
        StartState(state);
    }

    //private void OnGUI()
    //{
    //    GUILayout.Label(PhotonNetwork.player.CustomProperties[Global.CustomProperties.Name].ToString());
    //    GUILayout.Label(PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles].ToString());
    //    GUILayout.Label(PhotonNetwork.player.CustomProperties[Global.CustomProperties.VotedFor].ToString());
    //    GUILayout.Label(PhotonNetwork.player.CustomProperties[Global.CustomProperties.Dead].ToString());
    //    //GUILayout.Label((text == null).ToString());
    //}

    [PunRPC]
    public void StartState(string state)
    {
        bool started = false;
        //if (state.Equals(""))
            //throw new Exception("I did not get a state to initialize");

        if (state == Global.States.Night)
        {
            vote.InitializeVotes();
            //trialplayers = null;
            started = dayNight.StartView(state);
        }
        else if (state == Global.States.Day)
        {
            vote.InitializeVotes();
            //trialplayers = null;
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
        //if (!started)
            //throw new Exception("I did not initialize any prestates.");

    }

    public void EndingState(string state)
    {
        //if (state == "")
            //throw new Exception("I did not get a state to end");

        // if the state is morning and all civilians are dead, end the game.
        if (state == Global.States.Morning)
        {
            //PhotonPlayer player;

            //get the list of players killed by sheriff
            List<string> playersKilledbysher = vote.GetSheriffArrest(); //this is temporary
            playersKilled.AddRange(vote.GetSheriffArrest());
            Debug.Log("This is the list of players arrested by sheriff " + string.Join(",", playersKilledbysher.ToArray()));

            //get the player that was killed by mafia unless
            //that person was saved by the nurse.
            if (vote.GetMafiaKill() != "")
            {
                if (vote.GetNurseSave() != vote.GetMafiaKill())
                {
                    playersKilled.Add(vote.GetMafiaKill());
                    playersKilledbysher.Add(vote.GetMafiaKill()); //this is temporary
                }
            }

            //set all players in the playersKilled list to dead.
            //if (PhotonNetwork.isMasterClient)
            //{
            Debug.Log("This is the list of players getting killed " + string.Join(",", playersKilledbysher.ToArray()));
            for (int pos = 0; pos < playersKilled.Count; pos++)
            {
                //player = findPlayer(playersKilled[pos]);
                //player.CustomProperties[Global.CustomProperties.Dead] = true;
                photonView.RPC("PlayerIsDead", PhotonTargets.All, playersKilled[pos]);
            }
            //}

            //if all civilians or all mafia are dead, end the game
            if (!isEndGame.CivilianAlive() || !isEndGame.MafiaAlive())
                Endgame();

            else
            {
                if (PhotonNetwork.isMasterClient)
                {
                    //photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(state));
                    EndedState(state);
                }
            }
        }

        // check if we need to skip the tiral state.
        else if (state == Global.States.PreTrial)
        {
            // if there is not at most two people with equal majority, skip the trial state.
            if(vote.GetVote(2).Count == 0)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    //photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(Global.States.Trial));
                    EndedState(Global.States.Trial);
                }
            }
            // else go to trial.
            else
            {
                if (PhotonNetwork.isMasterClient)
                {
                   // photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(state));
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
            string playerKilled = "";
            PhotonPlayer player = null;
            //Find out who was killed
            if (vote.GetVote(1).Count != 0)
            {
                playerKilled = vote.GetVote(1)[0];
                playersKilled.Add(playerKilled);
                photonView.RPC("PlayerIsDead", PhotonTargets.All, playerKilled);
                player = findPlayer(playerKilled);
                //if (PhotonNetwork.isMasterClient)
                //player.CustomProperties[Global.CustomProperties.Dead] = true;
            }

            //Find the photon player that is connected to that name
            //PhotonPlayer player = findPlayer(playerKilled);

            //Set the photon player to dead
            //if(PhotonNetwork.isMasterClient)
            //    player.CustomProperties[Global.CustomProperties.Dead] = true;

            //check if last sheriff.
            if ((player != null) && ((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Sheriff) && !isEndGame.SheriffAlive())
            {
                photonView.RPC("Endgame", PhotonTargets.All);
            }

            //check if last Mafia, or Civilian
            else if (!isEndGame.MafiaAlive() || !isEndGame.CivilianAlive())
            {
                photonView.RPC("Endgame", PhotonTargets.All);
            }

            //Continue to next state.
            else
            {
                if (PhotonNetwork.isMasterClient)
                {
                    //photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(state));
                    EndedState(state);
                }
            }
        }

        //Continue to the next state.
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                //photonView.RPC("ChangeGameState", PhotonTargets.All, Global.NextStates.Next(state));
                EndedState(state);
            }
        }
    }
    
    [PunRPC]
    void PlayerIsDead(string player)
    {
        //for (int pos = 0; pos < playersKilled.Count; pos++)
        //{
        if(!playersKilled.Contains(player))
        {
            playersKilled.Add(player);
        }
        PhotonPlayer photonPlayer = findPlayer(player);
        photonPlayer.CustomProperties[Global.CustomProperties.Dead] = true;
        //}
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


    //private void 

    #region NightDay_Initialization
    /// <summary>
    /// Initialize the NightDay panel for each individual
    /// players game states.
    /// </summary>
    [PunRPC]
    private void InitializeNightDay()
    {
        dayNight.StartGameInitialize();
        myname.GetComponent<Text>().text = (string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Name];
        myrole.GetComponent<Text>().text = (string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles];
    }

    ///// <summary>
    ///// Wait for the NightDay panel to finish initialization.
    ///// </summary>
    ///// <returns>False if initilization is not yet finished.</returns>
    //IEnumerator WaitForInitialization()
    //{
    //    yield return !dayNight.StartGameInitialize();
    //    if(PhotonNetwork.isMasterClient)
    //    {
    //        state = Global.States.Dusk;
    //        photonView.RPC("ChangeGameState", PhotonTargets.AllBuffered, state);
    //    }
    //}
    #endregion

    /// <summary>
    /// Only the master client will run this function. It will tell
    /// other players to initilize their next states, and then change
    /// their states for them.
    /// </summary>
    /// <param name="state"></param>
    private void EndedState(string state)
    {
        Debug.Log("The Photon Client " + PhotonNetwork.player.NickName + " is here");
        //InitializeGameState(Global.NextStates.Next(state));
        //Tells all players to intialize the next state
        photonView.RPC("InitializeGameState", PhotonTargets.All, Global.NextStates.Next(state));


        Debug.Log(state + " is ended.");


        state = Global.NextStates.Next(state);

        //StartState(state);
        photonView.RPC("StartState", PhotonTargets.All, state);
    }

    /// <summary>
    /// 
    /// </summary>
    [PunRPC]
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
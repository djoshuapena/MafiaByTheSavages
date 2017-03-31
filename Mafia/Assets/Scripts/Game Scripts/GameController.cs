using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Photon.MonoBehaviour
{

    public EndedController end;
    //public CanvasController canvas; //Dont need this
    public AssignRoles assign;
    public EndGameController endGame;
    //public ChatGraphicsController chat;
    public VoteController vote;
    public TimerGraphicsController timer;
    //public GameResultsPhase gameResults;
    public OverlayGraphicsController overlay;
    //public TrialGraphicsController trial;
    public FlavorText flavorText;
    //public DayNightGraphicsController dayNight;

    private Dictionary<string, string> text;
    //private Dictionary<string, ExitGames.Client.Photon.Hashtable> playerSetup;
    private string state = "";
    public Text output;

    // Use this for initialization
    void Start()
    {
        //myPhotonView = PhotonView.Get(this);
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
            assign.InitializeRoles();
            text = flavorText.InitializeFlavorTextDict();
            photonView.RPC("SaveFlavorText", PhotonTargets.All, text);
            state = "dusk";
            photonView.RPC("ChangeGameState", PhotonTargets.All, state);
        }
    }

    //[PunRPC]
    //public void setCustomProperties(Dictionary<string, ExitGames.Client.Photon.Hashtable> playerSetup)
    //{
    //    assign.PlayerCustomProperties(playerSetup);
    //}

    /// <summary>
    /// Change to the new game state.
    /// </summary>
    /// <param name="state">Game state to change to.</param>
    [PunRPC]
    public void ChangeGameState(string state)
    {
        if (state == "")
        {
            Debug.Log("I didnt get anything");
            return;
        }
        Debug.Log("I got here");
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
        PreStateInitialization(state);
    }

    [PunRPC]
    public void SaveFlavorText(Dictionary<string, string> text)
    {
        Debug.Log("I got here too.");
        flavorText.flavorTextDict = text;
        if (PhotonNetwork.isMasterClient)
        {
            output.text = "this is me";
        }
        else
        {
            output.text = "I am changing to this";
        }
    }

    private void PreStateInitialization(string state)
    {
        //if(state == "")
        //{
        //    Debug.Log("I dont have a state to initialize");
        //    return;
        //}
        //else if(state == "DayState")
        //{
        //    dayNight.Initialize("Day");
        //}
        //else if(state == "NightState")
        //{
        //    dayNight.Initialize("Night");
        //}
        //else if(state == "TrialState")
        //{
        //    trial.InitializeTrial();
        //}
        //else
        //{
        //    overlay.InitializeOverlay(state);
        //}

        //StartState(state);
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.player.CustomProperties["Name"].ToString());
        GUILayout.Label(PhotonNetwork.player.CustomProperties["roles"].ToString());
        GUILayout.Label(PhotonNetwork.player.CustomProperties["VotedFor"].ToString());
        GUILayout.Label(PhotonNetwork.player.CustomProperties["Dead"].ToString());


    }

    private void StartState(string state)
    {

    }

    private void EndingState(string state)
    {

    }

    private void EndedState(string state)
    {

    }

    //private void BroadcastMessageToAll(string message, object item)
    //{
    //    photonView.RPC(message, PhotonTargets.All, item);
    //}
}
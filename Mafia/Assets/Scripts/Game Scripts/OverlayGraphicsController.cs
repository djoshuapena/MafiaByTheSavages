using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;


public class OverlayGraphicsController : MonoBehaviour {

	//public Text textfield;
	public FlavorText flavorText;
	//public TimerGraphicsController timer;
	public VoteController voteResult; //This needs to come from the game controller
    public GameController game;
	private float time = 5;
	public GameObject[] overlayPanels;
    public int morningShfType; // initialize by game controller
    public int preTrialType; // initialize by game controller

	private int duskIntro        = 0;
	private int duskRole         = 1;
	private int morningMaf       = 2;
	private int morningShfOne    = 3;
    private int morningShfTwo    = 4;
    private int morningShfThree  = 5;
	private int morningDr        = 6;
	private int preTrialOne      = 7;
    private int preTrialTwo      = 8;
	private int postTrial        = 9;

	//public GameObject duskIntroPanel;
	//Panel[] OverlayPanel;

	/// <summary>
    /// Initialize the overlay that is required for the state.
    /// </summary>
    /// <param name="phase">State of the overlay to initilize</param>
    /// <returns>Whether or not it worked.</returns>
	public bool InitializeOverlay(string phase)
	{
		
		switch (phase){
			case Global.States.Dusk:
				overlayPanels [duskIntro].GetComponentInChildren<Text> ().text = flavorText.GetFlavorText (Global.FlavorTextKeys.Dusk);
				//image should stay same

				//check currentPlayer role
				//initialize text and image based on role
				InitRole();
                FunctionDone change = new FunctionDone(game.NowStartState);
                change(Global.States.Dusk);
                return true;

			case Global.States.Morning:
				InitMafiaResults();
				InitSherrifResults();
				InitDoctorResults();
                //game.StartState(phase);
				return true;

            case Global.States.PreTrial:
                InitPreTrial();
                //game.StartState(phase);
                return true;


			case Global.States.PostTrial:
				InitTrialResults();
                //game.StartState(phase);
				return true;
		}
		return false;
	}

    /// <summary>
    /// Show the overlay that is required by the state.
    /// </summary>
    /// <param name="phase">state of the overlay to show</param>
    /// <returns>Whether or not it worked</returns>
	public bool ShowOverlay(string phase)
	{
		foreach (GameObject panel in overlayPanels) {
			panel.SetActive(false);
		}

		switch (phase) {
		    case Global.States.Dusk:
				overlayPanels [duskIntro].SetActive (true);
                StartCoroutine(DuskHelper());
				return true;

		    case Global.States.Morning:
			    overlayPanels [morningMaf].SetActive (true);
                StartCoroutine(MorningHelper(morningMaf));
			    return true;

            case Global.States.PreTrial:
                overlayPanels[PreTrialType(preTrialType)].SetActive(true);
                StartCoroutine(WaitForFewSeconds(Global.States.PreTrial));              
                return true;

            case Global.States.PostTrial:
                overlayPanels[postTrial].SetActive(true);
                StartCoroutine(WaitForFewSeconds(Global.States.PostTrial));
                return true;
        }
        return false;
	}

    /// <summary>
    /// Second half of dusk phase.
    /// </summary>
    private void DuskPart2()
    {
        overlayPanels[duskIntro].SetActive(false);
        overlayPanels[duskRole].SetActive(true);
        StartCoroutine(WaitForFewSeconds(Global.States.Dusk));
    }

    #region IEnumerators
    IEnumerator DuskHelper()
    {
        yield return new WaitForSeconds(time);
        DuskPart2();
    }

    IEnumerator MorningHelper(int panel)
    {
        yield return new WaitForSeconds(time);
        if (panel == morningMaf)
        {
            overlayPanels[panel].SetActive(false);
            overlayPanels[MorningSherrifType(morningShfType)].SetActive(true);
            StartCoroutine(MorningHelper(MorningSherrifType(morningShfType)));
        }
        else
        {
            overlayPanels[panel].SetActive(false);
            overlayPanels[morningDr].SetActive(true);
            StartCoroutine(WaitForFewSeconds(Global.States.Morning));
        }
    }

    IEnumerator WaitForFewSeconds(string state)
    {
        yield return new WaitForSeconds(time);
        for(int pos = 0; pos < overlayPanels.Length; pos++)
            overlayPanels[pos].SetActive(false);
        game.EndingState(state);
    }
    #endregion

    #region Panel_Initializers
    private void InitRole()
	{
		switch ((string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles]) {
			case Global.Role.Civilian:
				overlayPanels[duskRole].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.CivilianWin); //Needs to be Civilian
				break;

			case Global.Role.Mafia:
				overlayPanels[duskRole].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.MafiaWin);
				break;

			case Global.Role.Sheriff:
				overlayPanels[duskRole].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.SheriffWin);
				break;

			case Global.Role.Nurse:
				overlayPanels[duskRole].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.NurseWin);
				break;
		}
	}

	private void InitMafiaResults()
	{
        string role, kill = voteResult.GetMafiaKill();
        int player = findPlayer(kill);
        if (player > 0)
            role = (string)PhotonNetwork.playerList[player].CustomProperties[Global.CustomProperties.Roles];
        else
            role = "";

        switch (role)/*get voted player's role*/
        {
		    case Global.Role.Civilian:
			    overlayPanels[morningMaf].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.MafiaKills); // Specific Kill based on person
			    break;

		    case Global.Role.Sheriff:
			    overlayPanels[morningMaf].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.MafiaKills);
			    break;
		
		    case Global.Role.Nurse:
			    overlayPanels[morningMaf].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.MafiaKills);
			    break;

            case Global.Role.Mafia:
                overlayPanels[morningMaf].GetComponentInChildren<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.MafiaKills);
                break;

            case "":
			    overlayPanels[morningMaf].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.NoKill);
			    break;
		}
	}

	private void InitSherrifResults()
	{
        List<string> sheriffArrest = voteResult.GetSheriffArrest();

        switch (sheriffArrest.Count)
        {
            case 0:
                overlayPanels[morningShfOne].GetComponentInChildren<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.SheriffCapture); //need to add sheriffCapture conditions
                break;
            case 1:
                overlayPanels[morningShfOne].GetComponentInChildren<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.SheriffCapture);
                overlayPanels[morningShfOne].transform.FindChild("Name").GetComponent<Text>().text = sheriffArrest[0];
                break;
            case 2:
                overlayPanels[morningShfTwo].GetComponentInChildren<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.SheriffCapture);
                overlayPanels[morningShfTwo].transform.FindChild("Name1").GetComponent<Text>().text = sheriffArrest[0];
                overlayPanels[morningShfTwo].transform.FindChild("Name2").GetComponent<Text>().text = sheriffArrest[1];
                break;
            case 3:
                overlayPanels[morningShfThree].GetComponentInChildren<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.SheriffCapture);
                overlayPanels[morningShfThree].transform.FindChild("Name1").GetComponent<Text>().text = sheriffArrest[0];
                overlayPanels[morningShfThree].transform.FindChild("Name2").GetComponent<Text>().text = sheriffArrest[1];
                overlayPanels[morningShfThree].transform.FindChild("Name3").GetComponent<Text>().text = sheriffArrest[2];
                break;
        }

	}

    /// <summary>
    /// Find out if the doctor saved anyone. If they did, initilize the protect success panel.
    /// If they failed, initilize the protect failed panel.
    /// </summary>
	private void InitDoctorResults()
	{
        //int tmprole = 1;
        //if(voteResult.getVote(1) !null) get players role
        string name, saved = voteResult.GetNurseSave();
        int playerNum = findPlayer(saved);

        if (playerNum > 0)
        {
            name = (string)PhotonNetwork.playerList[playerNum].CustomProperties[Global.CustomProperties.Name];
            overlayPanels[morningDr].GetComponentInChildren<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.NurseProtect);
        }
        else
		    overlayPanels[morningDr].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.NoProtect);
	}

    /// <summary>
    /// Find out who was accused to be put on trial. If one or two were accused, show their names.
    /// If no one was accused, show that no one will go to trial.
    /// </summary>
    private void InitPreTrial()
    {
        List<string> accuesed = voteResult.GetVote(2);

        switch (accuesed.Count)
        {
            case 0:
                overlayPanels[preTrialOne].GetComponentInChildren<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.TrialReport); //need to add pre trial conditions
                break;
            case 1:
                overlayPanels[preTrialOne].GetComponentInChildren<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.TrialReport);
                overlayPanels[preTrialOne].transform.FindChild("Name").GetComponent<Text>().text = accuesed[0];
                break;
            case 2:
                overlayPanels[preTrialTwo].GetComponentInChildren<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.TrialReport);
                overlayPanels[preTrialTwo].transform.FindChild("Name1").GetComponent<Text>().text = accuesed[0];
                overlayPanels[preTrialTwo].transform.FindChild("Name2").GetComponent<Text>().text = accuesed[1];
                break;
        }
        
    }

    /// <summary>
    /// Find out if the trial was a success. If it was, show who was killed and their role.
    /// If it was a fail, then show that no one was killed.
    /// </summary>
	private void InitTrialResults()
	{
        //int tmprole = 1;
        List<string> trialResult = voteResult.GetVote(1);
        string role, name = "";
        if (trialResult.Count != 0)
        {
            name = trialResult[0];
        }
        int playerRole = findPlayer(name);
        if (playerRole > 0)
            role = (string)PhotonNetwork.playerList[playerRole].CustomProperties[Global.CustomProperties.Roles];
        else
            role = "";


        switch (role){
            case Global.Role.Civilian:
			    overlayPanels[postTrial].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.TrialReport); // needs to add guilty by role.
			    break;

		    case Global.Role.Mafia:
			    overlayPanels[postTrial].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.TrialReport);
			    break;

		    case Global.Role.Sheriff:
			    overlayPanels[postTrial].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.TrialReport);
			    break;

		    case Global.Role.Nurse:
			    overlayPanels[postTrial].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.TrialReport);
			    break;

		    case "":
			    overlayPanels[postTrial].GetComponentInChildren<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.TrialReport);
			    break;
		}
	}
    #endregion

    #region Helper Funcitons

    /// <summary>
    /// Find the PhotonNetwork index of a player with the name in string 'player'.
    /// If that player does not exist in the index, then return -1.
    /// </summary>
    /// <param name="player">String to compare photonNetwork player name to.</param>
    /// <returns>Position of player if found, -1 if not found.</returns>
    private int findPlayer(string player)
    {
        for (int playerNum = 0; playerNum < PhotonNetwork.playerList.Length; playerNum++)
        {
            if ((string)PhotonNetwork.playerList[playerNum].CustomProperties[Global.CustomProperties.Name] == player)
            {
                return playerNum;
            }
        }
        // if no player is found, return -1.
        return -1;
    }

    private int PreTrialType(int trialNUmber)
    {
        if (trialNUmber == 2)
            return preTrialTwo;
        else
            return preTrialOne;
    }

    private int MorningSherrifType(int morningShfType)
    {
        if (morningShfType == 3)
            return morningShfThree;
        else if (morningShfType == 2)
            return morningShfTwo;
        else
            return morningShfOne;
    }

    #endregion


    #region code_graveyard
    //case "Trial":
    //	overlayPanels[trial].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText("Trial");
    //	//image should stay the same
    //	return true;
    //	//b/reak;

    //case Global.States.Endgame:
    //	InitEndGame();
    //	return true;
    //	//break;
    //timer.InitializeTime(5);
    //timer.Deactivate();
    //timer.Countdown();
    //while (!timer.TimeUP()) ;
    //overlayPanels[duskIntro].SetActive(true);
    //overlayPanels[duskIntro].SetActive (false);
    //overlayPanels [duskRole].SetActive (true);
    //StartCoroutine(WaitForFewSeconds());
    ////timer.InitializeTime (time);
    ////timer.Deactivate ();
    ////timer.Countdown ();
    //overlayPanels [duskRole].SetActive (false);
    //case Global.States.Endgame:
    //    overlayPanels[endGame].SetActive(true);
    //    StartCoroutine(WaitForFewSeconds(Global.States.Endgame));
    //    //timer.InitializeTime(time);
    //    //timer.Deactivate();
    //    //timer.Countdown();
    //    return true;
    //    //break;

    //case "Night":
    //	overlayPanels[night].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.Night);
    //	//image should stay same
    //	return true;
    //	///break;
    /*
private bool ChangePanel()
{
    return false;
}
*/

    //private void InitEndGame()
    //{
    //	int tmprole = 1;

    //	switch(tmprole/*get current player's role*/){
    //	case 1/*you win*/:
    //		overlayPanels[endGame].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("YouWin");
    //		break;

    //	case 2/*you lose*/:
    //		overlayPanels[endGame].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("YouLose");
    //		break;
    //	}
    //}

    ///*
    //	public delegate bool CallBack()
    //	{
    //		return false;
    //	}
    //*/

    //	public void testOne()
    //	{
    //		InitializeOverlay ("Dusk");
    //		//ShowOverlay ("Dusk");
    //	}
    //	public void testTwo()
    //	{
    //		ShowOverlay ("Dusk");
    //	}

    #endregion
}

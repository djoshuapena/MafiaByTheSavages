using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class OverlayGraphicsController : MonoBehaviour {

	public Text textfield;
	public FlavorText flavorText;
	public TimerGraphicsController timer;
	public VoteController voteResult;
    public GameController game;
	public float time = 5;
	public GameObject[] overlayPanels;
	private int duskIntro  = 0;
	private int duskRole   = 1;
	private int morningMaf = 2;
	private int morningShf = 3;
	private int morningDr  = 4;
	private int preTrial   = 5;
	private int postTrial   = 6;
	private int endGame    = 7;
	//private int gameRes    = 9;

	//public GameObject duskIntroPanel;
	//Panel[] OverlayPanel;

	// Use this for initialization
	public bool InitializeOverlay(string phase)
	{

		switch (phase){
			case Global.States.Dusk:
				overlayPanels [duskIntro].transform.FindChild ("flavor_text").GetComponent<Text> ().text = flavorText.GetFlavorText (Global.FlavorTextKeys.Dusk);
				//image should stay same

				//check currentPlayer role
				//initialize text and image based on role
				InitRole();
                FunctionDone change = new FunctionDone(game.NowStartState);
                change(Global.States.Dusk);
				return true;
				//break;

			//case "Night":
			//	overlayPanels[night].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText(Global.FlavorTextKeys.Night);
			//	//image should stay same
			//	return true;
			//	///break;

			case Global.States.Morning:
				InitMafiaResults();
				InitSherrifResults();
				InitDoctorResults();
				return true;
            //break;

            case Global.States.PreTrial:
                return true;

			//case "Trial":
			//	overlayPanels[trial].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText("Trial");
			//	//image should stay the same
			//	return true;
			//	//b/reak;

			case Global.States.PostTrial:
				InitTrialResults();
				return true;
				//break;

			case Global.States.Endgame:
				InitEndGame();
				return true;
				//break;
		}
		return false;
	}

	public bool ShowOverlay(string phase)
	{
		foreach (GameObject panel in overlayPanels) {
			panel.SetActive(false);
		}

		switch (phase) {
		    case Global.States.Dusk:
				//GameObject panel = Instantiate (overlayPanels [0]);
				overlayPanels [duskIntro].SetActive (true);
                StartCoroutine(DuskHelper());
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
				return true;
				///break;

		    case Global.States.Morning:
			    overlayPanels [morningMaf].SetActive (true);
                StartCoroutine(MorningHelper(morningMaf));
			//timer.InitializeTime (time);
			//timer.Deactivate ();
			//timer.Countdown ();
			//overlayPanels [morningMaf].SetActive (false);
			    //overlayPanels [morningShf].SetActive (true);
			//timer.InitializeTime (time);
			//timer.Deactivate ();
			//timer.Countdown ();
			//overlayPanels [morningShf].SetActive (false);
			    //overlayPanels [morningDr].SetActive (true);
                //StartCoroutine(WaitForFewSeconds(Global.States.Morning));
			//timer.InitializeTime (time);
			//timer.Deactivate ();
			//timer.Countdown ();
			//overlayPanels [morningDr].SetActive (false);
			    return true;
            //break;

            case Global.States.PreTrial:
                overlayPanels[preTrial].SetActive(true);
                StartCoroutine(WaitForFewSeconds(Global.States.PreTrial));
                //timer.InitializeTime(time);
                //timer.Deactivate();
                //timer.Countdown();
                
                return true;
            ///break;

            case Global.States.PostTrial:
                overlayPanels[postTrial].SetActive(true);
                StartCoroutine(WaitForFewSeconds(Global.States.PostTrial));
                //timer.InitializeTime(time);
                //timer.Deactivate();
                //timer.Countdown();
                return true;
            //break;

            case Global.States.Endgame:
                overlayPanels[endGame].SetActive(true);
                StartCoroutine(WaitForFewSeconds(Global.States.Endgame));
                //timer.InitializeTime(time);
                //timer.Deactivate();
                //timer.Countdown();
                return true;
                //break;
        }
        return false;
	}

    private void DuskPart2()
    {
        overlayPanels[duskIntro].SetActive(false);
        overlayPanels[duskRole].SetActive(true);
        StartCoroutine(WaitForFewSeconds(Global.States.Dusk));
        //timer.InitializeTime (time);
        //timer.Deactivate ();
        //timer.Countdown ();
    }

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
            overlayPanels[morningShf].SetActive(true);
            MorningHelper(morningShf);
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

	/*
	private bool ChangePanel()
	{
		return false;
	}
	*/

	private void InitRole()
	{
		switch ((string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles]) {
			case Global.Role.Civilian:
				overlayPanels[duskRole].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.CivilianWin); //Needs to be Civilian
				break;

			case Global.Role.Mafia:
				overlayPanels[duskRole].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.MafiaWin);
				break;

			case Global.Role.Sheriff:
				overlayPanels[duskRole].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.SheriffWin);
				break;

			case Global.Role.Nurse:
				overlayPanels[duskRole].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.NurseWin);
				break;
		}
	}

	private void InitMafiaResults()
	{
        string role, kill = voteResult.GetMaifaKill();
        int player = findPlayer(kill);
        if (player > 0)
            role = (string)PhotonNetwork.playerList[player].CustomProperties[Global.CustomProperties.Roles];
        else
            role = "";

        switch (role)/*get voted player's role*/
        {
		case Global.Role.Civilian:
			overlayPanels[morningMaf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.MafiaKills);
			break;

		case Global.Role.Sheriff:
			overlayPanels[morningMaf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.MafiaKills);
			break;
		
		case Global.Role.Nurse:
			overlayPanels[morningMaf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.MafiaKills);
			break;

		case "":
			overlayPanels[morningMaf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText (Global.FlavorTextKeys.NoKill);
			break;
		}
	}

	private void InitSherrifResults()
	{
  //      string role;
  //      List<string> arrest = voteResult.GetSheriffArrest();
  //      //int player = findPlayer(arrest);
  //      if (player > 0)
  //          role = (string)PhotonNetwork.playerList[player].CustomProperties[Global.CustomProperties.Roles];
  //      else
  //          role = "";

  //      switch (role){
		//case Global.Role.:
		//	overlayPanels[morningShf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningFindCivilian");
		//	break;

		//case 2/*sherrif*/:
		//	overlayPanels[morningShf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningFindSherrif");
		//	break;

		//case 3/*doctor*/:
		//	overlayPanels[morningShf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningFindDoctor");
		//	break;

		//case 4/*mafia*/:
		//	overlayPanels[morningShf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningFindMafia");
		//	break;
		//}
	}

    private int findPlayer(string player)
    {
        for (int playerNum = 0; playerNum < PhotonNetwork.playerList.Length; playerNum++)
        {
            if ((string)PhotonNetwork.playerList[playerNum].CustomProperties[Global.CustomProperties.Name] == player)
            {
               return playerNum;
            }
        }
        return -1;
    }

	private void InitDoctorResults()
	{
		int tmprole = 1;
		/* if(voteResult.getVote(1) !null) get players role
		 * 
		 */
		switch(tmprole/*get voted player's role*/){
		case 1/*protect successful*/:
			overlayPanels[morningDr].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningProtectSuccess");
			break;

		case 2/*protect unsuccesful*/:
			overlayPanels[morningDr].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningProtectFail");
			break;
		}
	}

	private void InitTrialResults()
	{
		int tmprole = 1;
		/* if(voteResult.getVote(1) !null) get players role
		 * 
		 */
		switch(tmprole/*get voted player's role*/){
		case 1/*guilty civilian*/:
			overlayPanels[postTrial].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("GuiltyCivilian");
			break;

		case 2/*guilty mafia*/:
			overlayPanels[postTrial].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("GuiltyMafia");
			break;

		case 3/*guilty sherrif*/:
			overlayPanels[postTrial].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("GuiltySherrif");
			break;

		case 4/*guilty doctor*/:
			overlayPanels[postTrial].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("GuiltyDoctor");
			break;

		case 5/*not guilty*/:
			overlayPanels[postTrial].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("NotGuilty");
			break;
		}
	}

	private void InitEndGame()
	{
		int tmprole = 1;

		switch(tmprole/*get current player's role*/){
		case 1/*you win*/:
			overlayPanels[endGame].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("YouWin");
			break;

		case 2/*you lose*/:
			overlayPanels[endGame].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("YouLose");
			break;
		}
	}


/*
	public delegate bool CallBack()
	{
		return false;
	}
*/

	public void testOne()
	{
		InitializeOverlay ("Dusk");
		//ShowOverlay ("Dusk");
	}
	public void testTwo()
	{
		ShowOverlay ("Dusk");
	}


}

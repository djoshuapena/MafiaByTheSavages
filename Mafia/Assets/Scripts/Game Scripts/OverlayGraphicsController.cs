using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class OverlayGraphicsController : MonoBehaviour {

	public Text textfield;
	public FlavorText flavorText;
	public Timer timer;
	public VoteController voteResult;
	public int time = 5;
	public GameObject[] overlayPanels;
	private int duskIntro  = 0;
	private int duskRole   = 1;
	private int night      = 2;
	private int morningMaf = 3;
	private int morningShf = 4;
	private int morningDr  = 5;
	private int trial      = 6;
	private int trialRes   = 7;
	private int endGame    = 8;
	private int gameRes    = 9;

	//public GameObject duskIntroPanel;
	//Panel[] OverlayPanel;

	// Use this for initialization
	void Start () {
		
	}

	public bool InitializeOverlay(string phase)
	{

		switch (phase){
			case "Dusk":
				overlayPanels [duskIntro].transform.FindChild ("flavor_text").GetComponent<Text> ().text = flavorText.GetFlavorText ("Intro");
				//image should stay same

				//check currentPlayer role
				//initialize text and image based on role
				InitRole();
				return true;
				break;

			case "Night":
				overlayPanels[night].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText("Night");
				//image should stay same
				return true;
				break;

			case "Morning":
				InitMafiaResults();
				InitSherrifResults();
				InitDoctorResults();
				return true;
				break;

			case "Trial":
				overlayPanels[trial].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText("Trial");
				//image should stay the same
				return true;
				break;

			case "TrialResults":
				InitTrialResults();
				return true;
				break;

			case "EndGame":
				InitEndGame();
				return true;
				break;
		}
		return false;
	}

	public bool ShowOverlay(string phase)
	{
		/*
		foreach (GameObject panel in overlayPanels) {
			panel.SetActive(false);
		}
		*/
		switch (phase) {
		case "Dusk":
				//GameObject panel = Instantiate (overlayPanels [0]);
				overlayPanels [duskIntro].SetActive (true);
				timer.InitializeTime (time);
				timer.Deactivate ();
				timer.Countdown ();
				overlayPanels [duskIntro].SetActive (false);
				overlayPanels [duskRole].SetActive (true);
				timer.InitializeTime (time);
				timer.Deactivate ();
				timer.Countdown ();
				overlayPanels [duskRole].SetActive (false);
				return true;
				break;

		case "Night":
			overlayPanels [night].SetActive (true);
			timer.InitializeTime (time);
			timer.Deactivate ();
			timer.Countdown ();
			overlayPanels [night].SetActive (false);
			return true;
			break;

		case "Morning":
			overlayPanels [morningMaf].SetActive (true);
			timer.InitializeTime (time);
			timer.Deactivate ();
			timer.Countdown ();
			overlayPanels [morningMaf].SetActive (false);
			overlayPanels [morningShf].SetActive (true);
			timer.InitializeTime (time);
			timer.Deactivate ();
			timer.Countdown ();
			overlayPanels [morningShf].SetActive (false);
			overlayPanels [morningDr].SetActive (true);
			timer.InitializeTime (time);
			timer.Deactivate ();
			timer.Countdown ();
			overlayPanels [morningDr].SetActive (false);
			return true;
			break;

		case "Trial":
			overlayPanels [trial].SetActive (true);
			timer.InitializeTime (time);
			timer.Deactivate ();
			timer.Countdown ();
			overlayPanels [trial].SetActive (false);
			return true;
			break;

		case "TrialResults":
			overlayPanels [trialRes].SetActive (true);
			timer.InitializeTime (time);
			timer.Deactivate ();
			timer.Countdown ();
			overlayPanels [trialRes].SetActive (false);
			return true;
			break;

		case "EndGame":
			overlayPanels [endGame].SetActive (true);
			timer.InitializeTime (time);
			timer.Deactivate ();
			timer.Countdown ();
			overlayPanels [endGame].SetActive (false);
			return true;
			break;
		}
		return false;
	}

	/*
	private bool ChangePanel()
	{
		return false;
	}
	*/

	private void InitRole()
	{
		int tmprole = 1;
		switch (tmprole/*photonplayer role*/) {
			case 1/*civilian*/:
				overlayPanels[duskRole].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("RoleCivilian");
				break;

			case 2/*mafia*/:
				overlayPanels[duskRole].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("RoleMafia");
				break;

			case 3/*sherrif*/:
				overlayPanels[duskRole].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("RoleSherrif");
				break;

			case 4/*doctor*/:
				overlayPanels[duskRole].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("RoleDoctor");
				break;
		}
	}

	private void InitMafiaResults()
	{
		int tmprole = 1;
		/* if(voteResult.getVote(1) !null) get players role
		 * 
		 */
		switch(tmprole/*get voted player's role*/){
		case 1/*civilian*/:
			overlayPanels[morningMaf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningKillCivilian");
			break;

		case 2/*sherrif*/:
			overlayPanels[morningMaf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningKillSherrif");
			break;
		
		case 3/*doctor*/:
			overlayPanels[morningMaf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningKillDoctor");
			break;

		case 4/*none*/:
			overlayPanels[morningMaf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningKillNone");
			break;
		}
	}

	private void InitSherrifResults()
	{
		int tmprole = 1;
		/* if(voteResult.getVote(1) !null) get players role
		 * 
		 */
		switch(tmprole/*get voted player's role*/){
		case 1/*civilian*/:
			overlayPanels[morningShf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningFindCivilian");
			break;

		case 2/*sherrif*/:
			overlayPanels[morningShf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningFindSherrif");
			break;

		case 3/*doctor*/:
			overlayPanels[morningShf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningFindDoctor");
			break;

		case 4/*mafia*/:
			overlayPanels[morningShf].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("MorningFindMafia");
			break;
		}
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
			overlayPanels[trialRes].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("GuiltyCivilian");
			break;

		case 2/*guilty mafia*/:
			overlayPanels[trialRes].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("GuiltyMafia");
			break;

		case 3/*guilty sherrif*/:
			overlayPanels[trialRes].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("GuiltySherrif");
			break;

		case 4/*guilty doctor*/:
			overlayPanels[trialRes].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("GuiltyDoctor");
			break;

		case 5/*not guilty*/:
			overlayPanels[trialRes].transform.FindChild("flavor_text").GetComponent<Text>().text = flavorText.GetFlavorText ("NotGuilty");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
public class TrialGraphicsController : MonoBehaviour {

    //public Image guiltyBoxOriginal; //Needed?
    //public Sprite[] characterImages;

    public Button loneBtn;
    public GameObject loneNotGulty;
    public GameObject oneTrialPanel;

    public GameObject trialPanel;
    public Button pairBtnHidden1;
    public Button pairBtnHidden2;
    public Button pairBtn1;
    public Button pairBtn2;
    public GameObject twoTrialPanel;

    public GameObject GuiltyBoxLone;
    public Button loneButtonHidden;

    public GameObject GuiltyBoxPair;
    public GameObject pairNotGuilty1;
    public GameObject pairNotGuilty2;

    private bool onePersonTrial = false;
    private bool loneSelected = false;
    private bool playerOneSelected = false;
    private bool playerTwoSelected = false;
//private bool startTrialDone = false;

    //private List<string> playerlist = new List<string>();
    private string name1;
    private string name2;

    public VoteController votec;
    public GameController game;

    //TIMER STUFF
    //question: do we need to check initilization of this in InitializeTrial()
    public TimerGraphicsController trialTimer;

    //returns false if one or more gameObjects fail to initialize.
    public bool InitializeTrial(List<string> paramlist) //List<string> playerList //return bool
    {
        //Set the local playerlist equal to the
        //list passed to InitializeTrial() as a 
        //parameter from the GameController
        //playerlist.AddRange(paramlist);

        GetNumDefendants(paramlist);
        SetBtnNames(paramlist);
        Debug.Log("Player 1: " + name1);
        Debug.Log("Player 2: " + name2 + " (if blank, 1 person trial)");

        bool initTrialSuccess = true;

        //if (guiltyBoxOriginal == null)
        //{
        //    Debug.Log("Failed to initialize guiltyBoxOriginal.");
        //    initTrialSuccess = false;
        //}
        //if (!CheckArrayInit())
        //{
        //    Debug.Log("Failed to initialize one or more images in characterImages[]");
        //    initTrialSuccess = false;
        //}
        if (loneBtn == null)
        {
            Debug.Log("Failed to initialize loneBtn.");
            initTrialSuccess = false;
        }
        if (loneNotGulty == null)
        {
            Debug.Log("Failed to initialize loneBtnHidden.");
            initTrialSuccess = false;
        }
        if (oneTrialPanel == null)
        {
            Debug.Log("Failed to initialize oneTrialPanel.");
            initTrialSuccess = false;
        }
        if (pairBtnHidden1 == null)
        {
            Debug.Log("Failed to initialize pairBtnHidden1.");
            initTrialSuccess = false;
        }
        if (pairBtnHidden2 == null)
        {
            Debug.Log("Failed to initialize pairBtnHidden2.");
            initTrialSuccess = false;
        }
        if (pairBtn1 == null)
        {
            Debug.Log("Failed to initialize pairBtn1.");
            initTrialSuccess = false;
        }
        if (pairBtn2 == null)
        {
            Debug.Log("Failed to initialize pairBtn2.");
            initTrialSuccess = false;
        }
        if (twoTrialPanel == null)
        {
            Debug.Log("Failed to initialize twoTrialPanel.");
            initTrialSuccess = false;
        }
        if (GuiltyBoxLone == null)
        {
            Debug.Log("Failed to initialize GuiltyBoxLone.");
            initTrialSuccess = false;
        }
        if (loneButtonHidden == null)
        {
            Debug.Log("Failed to initialize loneNotGuilty.");
            initTrialSuccess = false;
        }
        if (GuiltyBoxPair == null)
        {
            Debug.Log("Failed to initialize GuiltyBoxPair.");
            initTrialSuccess = false;
        }
        if (pairNotGuilty1 == null)
        {
            Debug.Log("Failed to initialize pairNotGuilty1.");
            initTrialSuccess = false;
        }
        if (pairNotGuilty2 == null)
        {
            Debug.Log("Failed to initialize pairNotGuilty2.");
            initTrialSuccess = false;
        }

        //game.StartState(Global.States.Trial);
        return initTrialSuccess;
    }
		
    public void StartTrial()
    {
        trialPanel.SetActive(true);
        //until timer fixed
        //startTrialDone = true;

        SelectTrialPanel();

        //trialTimer.InitializeTime(30);
        if (PhotonNetwork.isMasterClient)
        {
            trialTimer.Activate();
            trialTimer.Countdown(Global.States.Trial, 45f);
        }

        //InvokeRepeating("checkTimer", 0.1f, 1.0f);


        //if (test if timer done somehow)
        // {
        //     trialTimer.Deactivate();
        //     startTrialDone = true;
        // }
        // if (!startTrialDone)
        //     Debug.Log("Problem with trial phase timer.");
//return startTrialDone;
    }

    ////public void checkTimer()
    ////{
    ////    Debug.Log("Checking");
    ////    if (trialTimer.TimeUP())
    ////        CancelInvoke();
    ////    game.EndingState(Global.States.Trial);
    ////}

    private void ChangeGuiltyBoxImage ()
    {
       if (onePersonTrial)
       {
           ChangeGuiltyHelperLone();
       }
       else
       {
            ChangeGuiltyHelperPair();            
       }
    }

    private void ChangeGuiltyHelperLone()
    {
        if (!loneSelected)
        {
            loneNotGulty.gameObject.SetActive(true);
            loneButtonHidden.gameObject.SetActive(true);
            GuiltyBoxLone.gameObject.SetActive(false);
            loneBtn.gameObject.SetActive(false);
            loneSelected = true;
        }
        else
        {
            loneNotGulty.gameObject.SetActive(false);
            loneButtonHidden.gameObject.SetActive(false);
            GuiltyBoxLone.gameObject.SetActive(true);
            loneBtn.gameObject.SetActive(true);
            loneSelected = false;
        }
    }

    private void ChangeGuiltyHelperPair()
    {
        if(!playerOneSelected && !playerTwoSelected)
        {
            GuiltyBoxPair.gameObject.SetActive(true);

            pairBtnHidden1.gameObject.SetActive(false);
            pairBtnHidden2.gameObject.SetActive(false);
            pairBtn1.gameObject.SetActive(true);
            pairBtn2.gameObject.SetActive(true);
            pairNotGuilty1.gameObject.SetActive(false);
            pairNotGuilty2.gameObject.SetActive(false);
        }
        else if(playerOneSelected)
        {
            GuiltyBoxPair.gameObject.SetActive(false);

            pairBtn1.gameObject.SetActive(false);
            pairBtnHidden1.gameObject.SetActive(true);
            
            pairNotGuilty1.gameObject.SetActive(true);

            pairBtn2.gameObject.SetActive(true);
            pairBtnHidden2.gameObject.SetActive(false);
            pairNotGuilty2.gameObject.SetActive(false);

            
        }
        else
        {
            GuiltyBoxPair.gameObject.SetActive(false);

            pairBtn1.gameObject.SetActive(true);
            pairBtnHidden1.gameObject.SetActive(false);
            pairNotGuilty1.gameObject.SetActive(false);

            pairBtn2.gameObject.SetActive(false);
            pairBtnHidden2.gameObject.SetActive(true);
            pairNotGuilty2.gameObject.SetActive(true);

        }
    }

    public void SelectPlayerOne()
    {
        Debug.Log("You selected this player.");
		//onePersonTrial = true;
        if (onePersonTrial)
        {
            Debug.Log("This is a one person trial.");
            playerOneSelected = true;
            votec.ChangeVote(name1);
			ChangeGuiltyBoxImage();
        }
        else
        {
            Debug.Log("This is a two person trial.");
            playerOneSelected = true;
            playerTwoSelected = false;
            votec.ChangeVote(name1);
            ChangeGuiltyBoxImage();
        }
    }

    public void DeselectPlayerOne()
	{
        Debug.Log("you deslected player one");
        playerOneSelected = false;
        votec.ChangeVote("");
        //+
		ChangeGuiltyBoxImage();
    }

    public void SelectPlayerTwo()
	{
        Debug.Log("You selected this player.");
        playerTwoSelected = true;
        playerOneSelected = false;
        votec.ChangeVote(name2);
        ChangeGuiltyBoxImage();
    }

    public void DeselectPlayerTwo()
    {
        Debug.Log("You deselected player two.");
        playerTwoSelected = false;
        votec.ChangeVote("");
        ChangeGuiltyBoxImage();
    }

    private void SelectTrialPanel()
    {
        loneNotGulty.gameObject.SetActive(false);
        pairBtnHidden1.gameObject.SetActive(false);
        pairBtnHidden2.gameObject.SetActive(false);
        loneButtonHidden.gameObject.SetActive(false);
        pairNotGuilty1.gameObject.SetActive(false);
        pairNotGuilty2.gameObject.SetActive(false);

        if (onePersonTrial)
        {
            loneBtn.gameObject.SetActive(true);
            GuiltyBoxLone.gameObject.SetActive(true);
            DisableForDead();
            twoTrialPanel.SetActive(false);
            oneTrialPanel.SetActive(true);
        }
        else
        {
            pairBtn1.gameObject.SetActive(true);
            pairBtn2.gameObject.SetActive(true);
            GuiltyBoxPair.gameObject.SetActive(true);
            DisableForDead();
            oneTrialPanel.SetActive(false);
            twoTrialPanel.SetActive(true);
        }
    }

    private void DisableForDead()
    {
        if((bool)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Dead] == true)
        {
            loneBtn.GetComponent<Button>().interactable = false;
            pairBtn1.GetComponent<Button>().interactable = false;
            pairBtn2.GetComponent<Button>().interactable = false;
        }
    }
    private void GetNames(List<string> playerlist)
    {
        if(onePersonTrial)
        {
            name1 = playerlist[0];
        }
        else
        {
            name1 = playerlist[0];
            name2 = playerlist[1];
        }
    }
    private void SetBtnNames(List<string> playerlist)
    {
        GetNames(playerlist);

        if (onePersonTrial)
        {
            loneBtn.GetComponentInChildren<Text>().text = name1;
            loneNotGulty.GetComponentInChildren<Text>().text = name1;
        }
        else
        {
            pairBtn1.GetComponentInChildren<Text>().text = name1;
            pairBtnHidden1.GetComponentInChildren<Text>().text = name1;
            pairBtn2.GetComponentInChildren<Text>().text = name2;
            pairBtnHidden2.GetComponentInChildren<Text>().text = name2;

        }
    }

    public void SetTrialtoInactive()
    {
        trialPanel.SetActive(false);
    }

    private void GetNumDefendants(List<string> playerlist)
    {
        if(playerlist.Count == 1)
        {
            onePersonTrial = true;
            Debug.Log("1 person trial");
        }
        else
        {
            onePersonTrial = false;
            print("2 person trial");
        }
    }

    //private bool CheckArrayInit()
    //{
    //    bool arrayInitSuccess = true;
    //    for (int i = 0; i < characterImages.Length; i++)
    //    {
    //        if (characterImages[i] == null)
    //        {
    //            Debug.Log("characterImages["+i+"] failed to initialize.");
    //            arrayInitSuccess = false;
    //        }
    //    }
    //    return arrayInitSuccess;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightDayController : MonoBehaviour {

	public VoteController voteController;
	public FlavorText flavorText;
    public GameController game;
    public TimerGraphicsController timer;
    public GameObject NightDayPanel;
    public GameObject CharacterPanel;
    public GameObject BackgroundImage;
    public Sprite NightBackground;
    public Sprite DayBackground;
    public Sprite Civilian;
    public Sprite Mafia;
    public Sprite Nurse;
    public Sprite Sheriff;
    public Sprite DeadCivilian;
    public Sprite DeadMafia;
    public Sprite DeadNurse;
    public Sprite DeadSheriff;

    public GameObject characterIconPrefab;
    public List<GameObject> characterIconPrefabs;
    public GameObject characterSelectedIcon;

	//public Text text;

	//UpdateVote(name) updates the vote of name
	public void UpdateVote(string name){
		voteController.ChangeVote (name);
	}

    /// <summary>
    /// Return the sprite that represents the role of 'player'
    /// </summary>
    /// <param name="player">The PhotonPlayer that needs thier role sprite</param>
    /// <returns>The sprite that represents the players role </returns>
    private Sprite PlayerRole(PhotonPlayer player)
    {
        if ((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Mafia)
            return Mafia;
        else if ((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Sheriff)
            return Sheriff;
        else if (((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Nurse))
            return Nurse;
        else if ((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Civilian)
            return Civilian;
        throw new System.Exception("Player Not Assigned Role");
    }

    /// <summary>
    /// Return the dead sprite that represents the role of 'player'
    /// </summary>
    /// <param name="player">The PhotonPlayer that needs thier role sprite</param>
    /// <returns>The dead sprite that represents the players role </returns>
    private Sprite DeadPlayerRole(PhotonPlayer player)
    {
        if ((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Mafia)
            return DeadMafia;
        else if ((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Sheriff)
            return DeadSheriff;
        else if (((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Nurse))
            return DeadNurse;
        else if ((string)player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Civilian)
            return DeadCivilian;
        throw new System.Exception("Player Not Assigned Role");
    }

    /// <summary>
    /// At the very start of the game, each player in the PhotonNetwork will
    /// build thier list of player names and icons. The local player will
    /// be able to see the sprite that represents their role. If the local
    /// player is a mafia, then they can see the other mafia players.
    /// </summary>
    /// <returns>Whether or not it works.</returns>
    public bool StartGameInitialize()
    {
        for(int pos = 0; pos < PhotonNetwork.playerList.Length; pos++)
        {
            GameObject newButton = Instantiate(characterIconPrefab);
            newButton.transform.SetParent(CharacterPanel.transform);
            newButton.GetComponentInChildren<Text>().text = (string)PhotonNetwork.playerList[pos].CustomProperties[Global.CustomProperties.Name];
            if (newButton.GetComponentInChildren<Text>().text == (string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Name])
               newButton.GetComponent<Image>().sprite = PlayerRole(PhotonNetwork.player);
            else if (((string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Mafia) && ((string)PhotonNetwork.playerList[pos].CustomProperties[Global.CustomProperties.Roles] == Global.Role.Mafia))
                newButton.GetComponent<Image>().sprite = Mafia;
            else
                newButton.GetComponent<Image>().sprite = Civilian;
            newButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            int tempPos = pos;
            newButton.GetComponent<Button>().onClick.AddListener(() => VoteThisPlayerButton(tempPos));
            characterIconPrefabs.Add(newButton);
            //newPlayer.GetComponent<NightDayController>().characterSelectedIcon = characterSelectedIcon;
            //characterIconPrefabs.Add(newPlayer);
            //GameObject newPlayer = Instantiate(characterIconPrefab);
            //characterIconPrefabs[pos].transform.SetParent(CharacterPanel.transform);
            //characterIconPrefabs[pos].GetComponentInChildren<Text>().text = (string)PhotonNetwork.playerList[pos].CustomProperties[Global.CustomProperties.Name];
            //if (characterIconPrefabs[pos].GetComponentInChildren<Text>().text == (string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Name])
            //    characterIconPrefabs[pos].GetComponent<Image>().sprite = PlayerRole(PhotonNetwork.player);
            //else if (((string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Mafia) && ((string)PhotonNetwork.playerList[pos].CustomProperties[Global.CustomProperties.Roles] == Global.Role.Mafia))
            //    characterIconPrefabs[pos].GetComponent<Image>().sprite = Mafia;
            //else
            //    characterIconPrefabs[pos].GetComponent<Image>().sprite = Civilian;
            //characterIconPrefabs[pos].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //int tempPos = pos;
            //characterIconPrefabs[pos].GetComponent<Button>().onClick.AddListener(() => VoteThisPlayerButton(tempPos));
            //newPlayer.GetComponent<NightDayController>().characterSelectedIcon = characterSelectedIcon;
            //characterIconPrefabs.Add(newPlayer);
        }
        //if (characterIconPrefabs == null)
        //    return false;
        return true;
    }

    /// <summary>
    /// Set the background image based on 'state'. If there are
    /// any names in the 'deadPlayers' list, then disable those players,
    /// and change their icons.
    /// </summary>
    /// <param name="state">Current state of the game</param>
    /// <param name="deadPlayers">Players that will be disabled.</param>
    /// <returns></returns>
    /// 
    /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
     *                  FUTURE EXTENSION
     *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
     *  deadPlayers needs to be a list of PhotonPlayers, this needs
     *  to be changed in VoteController. VoteController should return
     *  list of PhotonPlayers, not strings.
     */
	public bool InitializeView(string state, List<string> deadPlayers)
    {
        if (state == Global.States.Night)
            BackgroundImage.GetComponent<Image>().sprite = NightBackground;
        else if (state == Global.States.Day)
            BackgroundImage.GetComponent<Image>().sprite = DayBackground;
        else
            throw new System.Exception("State " + state + " not recognized by NightDayController.");

        characterSelectedIcon.SetActive(false);

        // If the state is night, and the player is a civilian, all buttons are disabled.
        if((state == Global.States.Night) && ((string)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Roles] == Global.Role.Civilian))
        {
            for(int pos = 0; pos < characterIconPrefabs.Count; pos++)
            {
                characterIconPrefabs[pos].GetComponent<Button>().enabled = false;
            }
        }
        else
        {
            for (int pos = 0; pos < characterIconPrefabs.Count; pos++)
            {
                characterIconPrefabs[pos].GetComponent<Button>().enabled = true;
            }
        }

        if((bool)PhotonNetwork.player.CustomProperties[Global.CustomProperties.Dead])
        {
            for (int pos = 0; pos < characterIconPrefabs.Count; pos++)
            {
                characterIconPrefabs[pos].GetComponent<Button>().enabled = false;
            }
        }

        // David hates this stupid loop-de-loop. James seconds Davids hatred.
        // Once you (whoever you are) chages the vote controller to be lovely
        // Change this bulls**t.
        if (deadPlayers.Count > 0)
        {
            for (int pos = 0; pos < characterIconPrefabs.Count; pos++)
            {
                if (deadPlayers.Contains(characterIconPrefabs[pos].GetComponentInChildren<Text>().text))
                {
                    characterIconPrefabs[pos].GetComponent<Button>().enabled = false;
                    for (int player = 0; player < PhotonNetwork.playerList.Length; player++)
                    {
                        if ((string)PhotonNetwork.playerList[player].CustomProperties[Global.CustomProperties.Name] == characterIconPrefabs[pos].GetComponentInChildren<Text>().text)
                        {
                            characterIconPrefabs[pos].GetComponent<Image>().sprite = DeadPlayerRole(PhotonNetwork.playerList[player]);
                        }
                    }
                }
            }
        }
        //This causes mutiple threads to run for no reason. Clogging the message Pipe.
        //game.StartState(state);
        return true;
	}

    /// <summary>
    /// Start the day or night panel with all objects
    /// already initialized.
    /// </summary>
    /// <returns></returns>
    public bool StartView(string state)
    {
        //timer.InitializeTime(30);
        NightDayPanel.SetActive(true);
        if(PhotonNetwork.isMasterClient)
            timer.Countdown(state, 45f);
        //StartCoroutine(checkTimer(state));
        return true;
    }

    //IEnumerator checkTimer(string state)
    //{
    //    yield return !timer.TimeUP();
    //    NightDayPanel.SetActive(false);
    //    game.EndingState(state);
    //}

    private GameObject thisButton(GameObject findme)
    {
        for(int i = 0; i < characterIconPrefabs.Count; i++)
        {
            if (findme == characterIconPrefabs[i])
                return characterIconPrefabs[i];
        }
        throw new System.Exception("That button doesn't exist");
    }

	public void VoteThisPlayerButton(int button){
        characterSelectedIcon.GetComponent<Image>().sprite = characterIconPrefabs[button].GetComponent<Image>().sprite;
        Debug.Log(characterIconPrefabs[button].GetComponentInChildren<Text>().text);
        characterSelectedIcon.GetComponentInChildren<Text>().text = characterIconPrefabs[button].GetComponentInChildren<Text>().text;
        characterSelectedIcon.SetActive(true);
        UpdateVote(characterIconPrefabs[button].GetComponentInChildren<Text>().text);
        //PhotonNetwork.player.CustomProperties[Global.CustomProperties.VotedFor] = characterIconPrefab.GetComponentInChildren<Text>().text;
        //characterSelectedIcon.SetActive(true);
    }

    public void CancelVote()
    {
        PhotonNetwork.player.CustomProperties[Global.CustomProperties.VotedFor] = "";
        UpdateVote("");
        characterSelectedIcon.SetActive(false);
    }


	//DisplayFlavorText(scene) displays te correct flavor text based on the scene parameter
	/*private void DisplayFlavorText(string scene){
		if (scene.Equals ("Night"))
		{
			if((string)PhotonNetwork.player.CustomProperties["roles"].Equals("Mafia"))
				text=flavorText.GetFlavorText("NightMafia");
			else if((string)PhotonNetwork.player.CustomProperties["roles"].Equals("Sheriff"))
				text=flavorText.GetFlavorText("NightSherrif");
			else if((string)PhotonNetwork.player.CustomProperties["roles"].Equals("Healer"))
				text=flavorText.GetFlavorText("NightHealer");
			else
				text=flavorText.GetFlavorText("NightNothing");
		}
		else if (scene.Equals ("Day"))
		{
			text=flavorText.GetFlavorText("Day");
		}
	}*/
}

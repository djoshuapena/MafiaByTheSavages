using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightDayController : MonoBehaviour {

	public VoteController voteController;
	public FlavorText flavorText;

	public Text text;

	//UpdateVote(name) updates the vote of name
	public void UpdateVote(string name){
		voteController.ChangeVote (name);
	}

	public bool InitializeView(string nightDay){
        return true;

	}

    public bool StartView(string nightDay)
    {
        return true;
    }

	private void ChangeBackground(){

	}

	public void UpdateIcons(){

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

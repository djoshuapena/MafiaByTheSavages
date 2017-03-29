using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorText : MonoBehaviour {

    private string flavorTextPHP = "mafiasav.com/flavorText.php";//Does not exist
    public Dictionary<string, string> flavorTextDict = new Dictionary<string, string>();


    //allows other classes to initialize flavorTextDict
    //when they wish to use it
    public void InitializeFlavorTextDict()
    {
       Dictionary<string, string> flavorDict = new Dictionary<string, string>();
    }

    //gets the flavor text from the dictionary
    public string GetFlavorText(string scene)
    {
        KeyValuePair<string, string> juicyFruit = new KeyValuePair<string, string>();//the flavor text

        if (scene.Equals("dusk"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("morning"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("day"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("night"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("trialReport"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("mafiaWin"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("mafiaLoses"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("sheriffWins"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("sheriffLoses"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("nurseWins"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("nurseLoses"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("civiliansWin"))
        {
            return juicyFruit.Value;
        }
        if (scene.Equals("civiliansLose"))
        {
            return juicyFruit.Value;
        }
        return "Whoops Something went wrong :(";

    }

    // Use this for initialization
    void Start () {
        //soley for testing
        flavorTextDict.Add("dusk","Welcome to mafia! Be careful at night I hear this is a dangerous town.");
        flavorTextDict.Add("morning","Good morning. Now let's find out who survived the night shall we?");
        flavorTextDict.Add("night","Alright mischief makers time to vote, time to kill.");
        flavorTextDict.Add("day", "Well we now who died. Now who do you think did it?");
        flavorTextDict.Add("trialReport", "Aww poor saps. Looks like the mafia lived this round. To bad for you that Bob was actually innocent.");
        flavorTextDict.Add("mafiaWin", "Congrats! You murdered everyone the town is yours!");
        flavorTextDict.Add("mafiaLoses", "Shame, shame you should have been more sneaky you got caught by the police.");
        flavorTextDict.Add("sheriffWin", "You caught all the mafia. Have you thought about being a cop?");
        flavorTextDict.Add("sheriffLoses", "Uh oh looks like the one who got caught was you. Maybe you should paint instead?");
        flavorTextDict.Add("nurseWin", "Thanks to your hard work the sheriff that nasty mafia guy. Good job helping our hero not die.");
        flavorTextDict.Add("nurseLoses", "The town is dead. Well so are you. Guess being a nurse wasn't your calling?");
        flavorTextDict.Add("civilianWin", "Woot!! You should throw a party you helped catch all those nasty mafia guys!!");
        flavorTextDict.Add("civilianLoses", "I'll bring some flowers. After all you died. Next time elect a better a sheriff.");
        //end of testing entries hopefully sorry they're so long
    }

    // Update is called once per frame
    void Update () {
		
	}

}

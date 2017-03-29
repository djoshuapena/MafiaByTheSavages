using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FlavorText : MonoBehaviour {

    private string flavorTextPHP = "mafiasav.com/flavorText.php";//Does not exist
    public Dictionary<string, string> flavorTextDict = new Dictionary<string, string>();


    //allows other classes to initialize flavorTextDict
    //when they wish to use it
    public Dictionary<string,string> InitializeFlavorTextDict()
    {
        Dictionary<string, string> flavorDict = new Dictionary<string, string>();
        flavorDict.Add("dusk", "Welcome to mafia! Be careful at night I hear this is a dangerous town.");
        flavorDict.Add("morning", "Good morning. Now let's find out who survived the night shall we?");
        flavorDict.Add("night", "Alright mischief makers time to vote, time to kill.");
        flavorDict.Add("day", "Well we now who died. Now who do you think did it?");
        flavorDict.Add("trialReport", "Aww poor saps. Looks like the mafia lived this round. To bad for you that Bob was actually innocent.");
        flavorDict.Add("mafiaWin", "Congrats! You murdered everyone the town is yours!");
        flavorDict.Add("mafiaLoses", "Shame, shame you should have been more sneaky you got caught by the police.");
        flavorDict.Add("sheriffWin", "You caught all the mafia. Have you thought about being a cop?");
        flavorDict.Add("sheriffLoses", "Uh oh looks like the one who got caught was you. Maybe you should paint instead?");
        flavorDict.Add("nurseWin", "Thanks to your hard work the sheriff that nasty mafia guy. Good job helping our hero not die.");
        flavorDict.Add("nurseLoses", "The town is dead. Well so are you. Guess being a nurse wasn't your calling?");
        flavorDict.Add("civilianWin", "Woot!! You should throw a party you helped catch all those nasty mafia guys!!");
        flavorDict.Add("civilianLoses", "I'll bring some flowers. After all you died. Next time elect a better a sheriff.");
        flavorDict.Add("mafiaKills", "Poor Josh the mafia thought he was a knife block.");
        flavorDict.Add("noKill", "Mafia fails to kill.");
        flavorDict.Add("sheriffCapture", "Sheriff captures a mafia member.");
        flavorDict.Add("noCapture", "The investigation led nowhere.");
        flavorDict.Add("nurseProtects", "The nurse protected someone.");
        flavorDict.Add("noProtect", "The nurse protected the wrong person.");

        return flavorDict;
    }

    //gets the flavor text from the dictionary
    public string GetFlavorText(string scene)
    {
        if(scene == null)
        {
            throw new Exception("I didnt get anything");
        }

        if(!flavorTextDict.ContainsKey(scene))
        {
            throw new Exception("Does not exist");
        }
        return flavorTextDict[scene];
    }
}

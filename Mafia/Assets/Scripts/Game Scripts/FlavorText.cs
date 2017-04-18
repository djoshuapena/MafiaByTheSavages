using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FlavorText : MonoBehaviour {

    //private string flavorTextPHP = "mafiasav.com/flavorText.php";//Does not exist
    public Dictionary<string, string> flavorTextDict = new Dictionary<string, string>();


    //allows other classes to initialize flavorTextDict
    //when they wish to use it
    public Dictionary<string,string> InitializeFlavorTextDict()
    {
        Dictionary<string, string> flavorDict = new Dictionary<string, string>();
        flavorDict.Add(Global.FlavorTextKeys.MorningStart, "Good morning. Now let's find out who survived the night shall we?");
        flavorDict.Add(Global.FlavorTextKeys.MorningMafiaKill, "Mafia killed player");
        flavorDict.Add(Global.FlavorTextKeys.MorningMafiaFail, "Mafia did not kill player");
        flavorDict.Add(Global.FlavorTextKeys.MorningSheriffArrest1, "One sheriff arrested player");
        flavorDict.Add(Global.FlavorTextKeys.MorningSheriffArrest2, "Two sheriff arrested player");
        flavorDict.Add(Global.FlavorTextKeys.MorningSheriffArrest3, "Three sheriff arrested player");
        flavorDict.Add(Global.FlavorTextKeys.MorningSheriffFail, "Sheriff failed to arrest someone");
        flavorDict.Add(Global.FlavorTextKeys.MorningNurseSave, "Nurse saved someone");
        flavorDict.Add(Global.FlavorTextKeys.MorningNurseFail, "Nurse didn't save anyone");
        flavorDict.Add(Global.FlavorTextKeys.GameStart, "Welcome to Mafia!");
        flavorDict.Add(Global.FlavorTextKeys.AssignRoleCiv, "You are a civilian");
        flavorDict.Add(Global.FlavorTextKeys.AssignRoleSheriff, "You are a sheriff");
        flavorDict.Add(Global.FlavorTextKeys.AssignRoleMafia, "You are a mafia");
        flavorDict.Add(Global.FlavorTextKeys.AssignRoleNurse, "You are a nurse");
        flavorDict.Add(Global.FlavorTextKeys.PreTrial1, "One player accused");
        flavorDict.Add(Global.FlavorTextKeys.PreTrial2, "Two players accused");
        flavorDict.Add(Global.FlavorTextKeys.PreTrialNone, "No players accused");
        flavorDict.Add(Global.FlavorTextKeys.PostTrialSuccess, "Player hanged");
        flavorDict.Add(Global.FlavorTextKeys.PostTrialFail, "No player hanged");
        flavorDict.Add(Global.FlavorTextKeys.CivilianWin, "Woot!! You should throw a party you helped catch all those nasty mafia guys!!");
        flavorDict.Add(Global.FlavorTextKeys.MafiaWin, "Congrats! You murdered everyone the town is yours!");
        flavorDict.Add(Global.FlavorTextKeys.SheriffWin, "You caught all the mafia. Have you thought about being a cop?");
        flavorDict.Add(Global.FlavorTextKeys.NurseWin, "Thanks to your hard work the sheriff that nasty mafia guy. Good job helping our hero not die.");
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

/*      flavorDict.Add(Global.FlavorTextKeys.Dusk, "Welcome to mafia! Be careful at night I hear this is a dangerous town.");
        flavorDict.Add(Global.FlavorTextKeys.Night, "Alright mischief makers time to vote, time to kill.");
        flavorDict.Add(Global.FlavorTextKeys.Day, "Well we now who died. Now who do you think did it?");
        flavorDict.Add(Global.FlavorTextKeys.TrialReport, "Aww poor saps. Looks like the mafia lived this round. To bad for you that Bob was actually innocent.");
        flavorDict.Add(Global.FlavorTextKeys.MafiaWin, "Congrats! You murdered everyone the town is yours!");
        flavorDict.Add(Global.FlavorTextKeys.MafiaLoses, "Shame, shame you should have been more sneaky you got caught by the police.");
        flavorDict.Add(Global.FlavorTextKeys.SheriffWin, "You caught all the mafia. Have you thought about being a cop?");
        flavorDict.Add(Global.FlavorTextKeys.SheriffLoses, "Uh oh looks like the one who got caught was you. Maybe you should paint instead?");
        flavorDict.Add(Global.FlavorTextKeys.NurseWin, "Thanks to your hard work the sheriff that nasty mafia guy. Good job helping our hero not die.");
        flavorDict.Add(Global.FlavorTextKeys.NurseLoses, "The town is dead. Well so are you. Guess being a nurse wasn't your calling?");
        flavorDict.Add(Global.FlavorTextKeys.CivilianWin, "Woot!! You should throw a party you helped catch all those nasty mafia guys!!");
        flavorDict.Add(Global.FlavorTextKeys.CivilianLoses, "I'll bring some flowers. After all you died. Next time elect a better a sheriff.");
        flavorDict.Add(Global.FlavorTextKeys.MafiaKills, "Poor Josh the mafia thought he was a knife block.");
        flavorDict.Add(Global.FlavorTextKeys.NoKill, "Mafia fails to kill.");
        flavorDict.Add(Global.FlavorTextKeys.SheriffCapture, "Sheriff captures a mafia member.");
        flavorDict.Add(Global.FlavorTextKeys.NoCapture, "The investigation led nowhere.");
        flavorDict.Add(Global.FlavorTextKeys.NurseProtect, "The nurse protected someone.");
        flavorDict.Add(Global.FlavorTextKeys.NoProtect, "The nurse protected the wrong person.");*/

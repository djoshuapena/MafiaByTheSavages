using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FunctionDone(string status);
//public delegate void TimerDone();
public delegate bool TimerDone();

public static class Global {

    public static class Role
    {
        public const string Mafia = "Mafia";
        public const string Civilian = "Civilian";
        public const string Nurse = "Nurse";
        public const string Sheriff = "Sheriff";
    }

    public static class CustomProperties
    {
        public static string Name = "Name";
        public static string Roles = "Role";
        public static string Dead = "Dead";
        public static string VotedFor = "VotedFor";
    }

    public static class Chats
    {
        public static int Civilian = 0;
        public static int Mafia = 1;
        public static int Dead = 2;
    }

    public static class FlavorTextKeys
    {
        public static string Dusk = "dusk";
        public static string Morning = "morning";
        public static string Night = "night";
        public static string Day = "day";
        public static string TrialReport = "trialReport";
        public static string MafiaWin = "mafiaWin";
        public static string MafiaLoses = "mafiaLoses";
        public static string SheriffWin = "sheriffWin";
        public static string SheriffLoses = "sheriffLoses";
        public static string NurseWin = "nurseWin";
        public static string NurseLoses = "nurseLoses";
        public static string CivilianWin = "civilianWin";
        public static string CivilianLoses = "civilianLoses";
        public static string MafiaKills = "mafiaKills";
        public static string NoKill = "noKill";
        public static string SheriffCapture = "sheriffCapture";
        public static string NoCapture = "noCapture";
        public static string NurseProtect = "nurseProtects";
        public static string NoProtect = "noProtect";
    }

    public static class States
    {
        public const string Dusk = "Dusk";
        public const string Night = "Night";
        public const string Morning = "Morning";
        public const string Day = "Day";
        public const string PreTrial = "PreTrial";
        public const string Trial = "Trial";
        public const string PostTrial = "PostTrial";
        public const string Endgame = "Endgame";
    }

    public static class NextStates
    {
        public static string Next(string state)
        {
            if (state == States.Dusk)
                return States.Night;
            else if (state == States.Night)
                return States.Morning;
            else if (state == States.Morning)
                return States.Day;
            else if (state == States.Day)
                return States.PreTrial;
            else if (state == States.PreTrial)
                return States.Trial;
            else if (state == States.Trial)
                return States.PostTrial;
            return States.Night;
        }
        //public static string Dusk = States.Night;
        //public static string Night = States.Morning;
        //public static string Morning = States.Day;
        //public static string Day = States.PreTrial;
        //public static string PreTrial = States.Trial;
        //public static string Trial = States.PostTrial;
        //public static string PostTrial = States.Night;
    }


}


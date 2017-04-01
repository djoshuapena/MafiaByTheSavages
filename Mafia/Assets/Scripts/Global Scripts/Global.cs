using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global {

    public static class Role
    {
        public static string Mafia = "Mafia";
        public static string Civilian = "Civilian";
        public static string Nurse = "Nurse";
        public static string Sheriff = "Sheriff";
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

}


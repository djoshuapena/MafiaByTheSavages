using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using Menus;

public class Stats : MonoBehaviour {

    // scripts set in the inspector.
    public Menus setMenu;
    public Account accountInfo;

    // string that will hold user stats
    private string[] items;
    
    // php script in web domain.
    private string updateStatsUrl = "mafiasav.com/updateStats.php";
	private string getStatsUrl = "mafiasav.com/getstats.php";

    // public objects set in inspector
    // Game stats
    public GameObject totalGW;
	public GameObject totalGL;
    // Civilian stats
	public GameObject civilianW;
	public GameObject civilianL;
    // Mafia stats
	public GameObject mafiaW;
	public GameObject mafiaL;
	public GameObject mafiaK;
    // Doctor stats
	public GameObject doctorW;
	public GameObject doctorL;
	public GameObject doctorS;
    // Sherrif stats
	public GameObject sherrifW;
	public GameObject sherrifL;
	public GameObject sherrifC;

    /// <summary>
    /// Temporary function to fill stats array.
    /// </summary>
    /// <param name="stats">Array to fill.</param>
    private void FillStatsArray(ref int[] stats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = i;
        }
    }

    /// <summary>
    /// Insert data in to items array.
    /// </summary>
    public void InsertData()
	{
		//Total game 
		Text totalGameWin = totalGW.GetComponent<Text>(); //get the text component in the gameobject
		totalGameWin.text = Seperate(items[0], "TotalGameWin"); //set the text in the text component

		Text totalGameLoss = totalGL.GetComponent<Text>(); 
		totalGameLoss.text = Seperate (items [1], "TotalGameLoss"); 

		//Civilian 
		Text civilianWin = civilianW.GetComponent<Text> ();
		civilianWin.text = Seperate(items[2], "CivilianWin");

		Text civilianLoss = civilianL.GetComponent<Text> ();
		civilianLoss.text = Seperate(items[3], "CivilianLoss");

		//mafia
		Text mafiaWin = mafiaW.GetComponent<Text> ();
		mafiaWin.text = Seperate(items[4], "MafiaWin");

		Text mafiaLoss = mafiaL.GetComponent<Text> ();
		mafiaLoss.text = Seperate(items[5], "MafiaLoss");

		Text mafiaKill = mafiaK.GetComponent<Text> ();
		mafiaKill.text = Seperate(items[6], "MafiaKill");

		//doctor
		Text doctorWin = doctorW.GetComponent<Text> ();
		doctorWin.text = Seperate(items[7], "DoctorWin");

		Text doctorLoss = doctorL.GetComponent<Text> ();
		doctorLoss.text = Seperate(items[8], "DoctorLoss");

		Text doctorSave = doctorS.GetComponent<Text> ();
		doctorSave.text = Seperate(items[9], "DoctorSave");

		//sherrif
		Text sherrifWin = sherrifW.GetComponent<Text> ();
		sherrifWin.text = Seperate(items[10], "SherrifWin");

		Text sherrifLoss = sherrifL.GetComponent<Text> ();
		sherrifLoss.text = Seperate(items[11], "SherrifLoss");

		Text sherrifCaught = sherrifC.GetComponent<Text> ();
		sherrifCaught.text = Seperate(items[12], "SherrifCaught");
	}

    /// <summary>
    /// this function will seperate string into individual stats.
    /// </summary>
    /// <param name="data">is data which is the full string</param>
    /// <param name="index">the string that will be removed from the original string</param>
    /// <returns></returns>
    string Seperate(string data, string index){
		string value = data.Substring (data.IndexOf(index) + index.Length);
		return value;
	}

    /// <summary>
    /// Outputs the users stats.
    /// </summary>
    public void StatsOn()
    {
        StartCoroutine(UserStat());
    }

    /// <summary>
    /// Updates the users stats.
    /// </summary>
    public void UpdateStat()
    {
        StartCoroutine(UpdateStats());
    }


    /// <summary>
    /// Gets the users stats from the stats database.
    /// </summary>
    /// <returns>Data from the php script, when connected.</returns>
    IEnumerator UserStat()
    {
        WWWForm Form = new WWWForm();
        //pulls the username from menus script
        Form.AddField("Username", accountInfo.GetUsername());
		WWW itemsData = new WWW(getStatsUrl, Form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        //print (itemsDataString);
        items = itemsDataString.Split('|');
        //print (GetDataValue (items [0], "TotalGameWin"));
        InsertData();
        setMenu.StatsOn();
    }


    /// <summary>
    /// Updates the users stats in the stats database.
    /// </summary>
    /// <returns>Data from the php script, when connected.</returns>
    IEnumerator UpdateStats()
    {
        //fillStatsArray (ref stats);
        //statsString = convertToString (stats);
        WWWForm Form = new WWWForm();
        string statsString = "1 12 5 6 4 0 25 6 200 54 1 6 18";
        Form.AddField("Username", accountInfo.GetUsername());
        Form.AddField("stats", statsString);
        WWW UpdateStatsWWW = new WWW(updateStatsUrl, Form);
        yield return UpdateStatsWWW; //wait for php

        if (UpdateStatsWWW.error != null)
        {
            Debug.LogError("Cannot connect to account Creation");
        }
        else {
            Debug.Log(UpdateStatsWWW.text);
            string UpdateStatsReturn = UpdateStatsWWW.text;
            Debug.Log(UpdateStatsReturn);
            if (UpdateStatsReturn == "Success")
            {
                Debug.Log("Success: Stats Updated");
                //MainOn();
                setMenu.MainOn();
            }
        }
    }
}

/*
                Code Graveyard

    //public Menus x; //get menu script
    //public GameObject textgameobject;
    //private string [] tt;

    //GameObject element = GameObject.Find("_Scripts");
	//Menus menuScript = element.GetComponent<Menus>();
	//tt = menuScript.items;
	//print (tt.Length);
*/

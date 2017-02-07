using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using Menus;

public class stats : MonoBehaviour {

    //creates an instance of the class menus.cs
    private Menus menu;

    //string that will hold your stats
    private string[] items;
    private string UpdateStatsUrl = "http://giramdev.000webhostapp.com/updateStats.php";

    //create a game object that will allow you to drag infromation into the field in unity
    public GameObject totalGW;
	public GameObject totalGL;

	public GameObject civilianW;
	public GameObject civilianL;

	public GameObject mafiaW;
	public GameObject mafiaL;
	public GameObject mafiaK;

	public GameObject doctorW;
	public GameObject doctorL;
	public GameObject doctorS;

	public GameObject sherrifW;
	public GameObject sherrifL;
	public GameObject sherrifC;

    //temporary function to fill stats array (debug)
    public void fillStatsArray(ref int[] stats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = i;
        }
    }

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

	//this function will seperate a string, the first paramater is data which is the full string
	//and the index string is the string that will be romoved from the original string
	string Seperate(string data, string index){
		string value = data.Substring (data.IndexOf(index) + index.Length);
		return value;
	}

	// Use this for initialization
	void Start () {
        menu = FindObjectOfType(typeof(Menus)) as Menus;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //start the corutine for the the userstasts
    //it will get all the infromtaion from the php code and put everything in the 
    //correct fields in the UI then it will take you to the stats page 
    public void statOn()
    {
        StartCoroutine(UserStat());
    }

    //UpdateStats fills stats with temporary values for testing,
    //converts the stats array into a string to send to php and
    //calls the IEnumerator UpdateStats to post the string to the database.
    public void updateStat()
    {
        StartCoroutine(UpdateStat());
    }


    //this enumerator will take the username and connect to the php which will get all the stats about the
    //user and put it in the items string array
    IEnumerator UserStat()
    {
        WWWForm Form = new WWWForm();
        //pulls the username from menus script
        Form.AddField("Username", Menus.usernamestats);
        WWW itemsData = new WWW("https://giramdev.000webhostapp.com/getstats.php", Form);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        //print (itemsDataString);
        items = itemsDataString.Split('|');
        //print (GetDataValue (items [0], "TotalGameWin"));
        InsertData();
        menu.callSetMenu("Stats");
    }


    //this enumerator will update the stats it will take your wins or loss and store it in a string
    //the string is then passed into the php and it will be posted in the database
    IEnumerator UpdateStat()
    {
        //fillStatsArray (ref stats);
        //statsString = convertToString (stats);
        WWWForm Form = new WWWForm();
        string statsString = "1 12 5 6 4 0 25 6 200 54 1 6 18";
        Form.AddField("Username", Menus.usernamestats);
        Form.AddField("stats", statsString);
        WWW UpdateStatsWWW = new WWW(UpdateStatsUrl, Form);
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
                menu.callSetMenu("MainMenu");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getdata : MonoBehaviour {

	//public Menus x; //get menu script
	//public GameObject textgameobject;
	private string [] tt;

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

	public void InsertData()
	{
		GameObject element = GameObject.Find("_Scripts"); //place holder
		Menus menuScript = element.GetComponent<Menus>();
		tt = menuScript.items;
//		print (tt.Length);

		//Total game 
		Text totalGameWin = totalGW.GetComponent<Text>(); //get the text component in the gameobject
		totalGameWin.text = Seperate(tt[0], "TotalGameWin"); //set the text in the text component

		Text totalGameLoss = totalGL.GetComponent<Text>(); 
		totalGameLoss.text = Seperate (tt [1], "TotalGameLoss"); 

		//Civilian 
		Text civilianWin = civilianW.GetComponent<Text> ();
		civilianWin.text = Seperate(tt[2], "CivilianWin");

		Text civilianLoss = civilianL.GetComponent<Text> ();
		civilianLoss.text = Seperate(tt[3], "CivilianLoss");

		//mafia
		Text mafiaWin = mafiaW.GetComponent<Text> ();
		mafiaWin.text = Seperate(tt[4], "MafiaWin");

		Text mafiaLoss = mafiaL.GetComponent<Text> ();
		mafiaLoss.text = Seperate(tt[5], "MafiaLoss");

		Text mafiaKill = mafiaK.GetComponent<Text> ();
		mafiaKill.text = Seperate(tt[6], "MafiaKill");

		//doctor

		Text doctorWin = doctorW.GetComponent<Text> ();
		doctorWin.text = Seperate(tt[7], "DoctorWin");

		Text doctorLoss = doctorL.GetComponent<Text> ();
		doctorLoss.text = Seperate(tt[8], "DoctorLoss");

		Text doctorSave = doctorS.GetComponent<Text> ();
		doctorSave.text = Seperate(tt[9], "DoctorSave");

		//sherrif

		Text sherrifWin = sherrifW.GetComponent<Text> ();
		sherrifWin.text = Seperate(tt[10], "SherrifWin");

		Text sherrifLoss = sherrifL.GetComponent<Text> ();
		sherrifLoss.text = Seperate(tt[11], "SherrifLoss");

		Text sherrifCaught = sherrifC.GetComponent<Text> ();
		sherrifCaught.text = Seperate(tt[12], "SherrifCaught");

	}

	string Seperate(string data, string index){
		string value = data.Substring (data.IndexOf(index) + index.Length);
		return value;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

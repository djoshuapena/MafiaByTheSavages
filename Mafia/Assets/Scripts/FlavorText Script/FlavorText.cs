using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorText : MonoBehaviour {

    private string flavorTextPHP = "mafiasav.com/flavorText.php";//Not there yet
    private Dictionary<TBD, string> flavorTextDict;//TBD will probably be a player prefab


    //allows other classes to initialize flavorTextDict
    //when they wish to use it
    public void InitializeFlavorTextDict()
    {
       Dictionary<TBD, string> flavorTextDict = new Dictionary<TBD, string>();//TBD will probably be a player prefab
    }


    //gets the flavor text from the dictionary
    public string GetFlavorText()
    {
        KeyValuePair<TBD, string> juicyFruit = new KeyValuePair<TBD, string>();//the flavor text


        //need to ask if I will be doing the filtering or the PHP??

        /*
        if (mafia)
        {

        }
        if (sheriff)
        {

        }
        if (nurse)
        {

        }
        if (civilian)
        {

        }
        if (dusk)
        {

        }*/
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

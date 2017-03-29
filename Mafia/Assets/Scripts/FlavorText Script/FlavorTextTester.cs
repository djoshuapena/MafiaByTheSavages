using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorTextTester : MonoBehaviour {
    public FlavorText story;
	// Use this for initialization
	void Start () {
        Dictionary<string, string> flavorTextDict = story.InitializeFlavorTextDict();
        story.flavorTextDict = flavorTextDict;
        Debug.Log(story.GetFlavorText("dusk"));
        Debug.Log(story.GetFlavorText("trialReport"));
        Debug.Log(story.GetFlavorText("night"));
        Debug.Log(story.GetFlavorText("mafiaWin"));
        Debug.Log(story.GetFlavorText("nurseLoses"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

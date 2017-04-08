using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialGraphicsTest : MonoBehaviour {

	public TrialGraphicsController Trial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void checkStartTime()
	{
		Trial.StartTrial ();
	}
}

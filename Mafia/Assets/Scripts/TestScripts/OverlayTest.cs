using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTest : MonoBehaviour {

	public OverlayGraphicsController overlay;
	public VoteTesting vote;

	public void testInitializeDusk()
	{
		print(overlay.InitializeOverlay ("Dusk"));
	}

	public void testInitilizeMorning()
	{
		vote.getSheriffpick ();
		print(overlay.InitializeOverlay ("Morning"));
	}

	public void testInitilizePreT()
	{
		print(overlay.InitializeOverlay ("PreTrial"));
	}

	public void testInitilizePostT()
	{
		print(overlay.InitializeOverlay ("PostTrial"));
	}
}

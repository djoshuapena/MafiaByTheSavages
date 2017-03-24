using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhaseController : MonoBehaviour
{
 
    //public objects set in inspector
    public Canvas DuskPhaseCanvas;
    public Canvas NightPhaseCanvas;
    public Canvas MorningPhaseCanvas;
    public Canvas DayPhaseCanvas;
    public Canvas TrialPhaseCanvas;

    /// <summary>
    /// Check that each canvas is initialized.
    /// </summary>
    private void Awake()
    {
        if (DuskPhaseCanvas == null)
            Debug.Log("Failed to initialize the DuskPhaseCanvas.");
        if (NightPhaseCanvas == null)
            Debug.Log("Failed to initialize the NightPhaseCanvas.");
        if (MorningPhaseCanvas == null)
            Debug.Log("Failed to initialize the MorningPhaseCanvas.");
        if (DayPhaseCanvas == null)
            Debug.Log("Failed to initialize the DayPhaseCanvas.");
        if (TrialPhaseCanvas == null)
            Debug.Log("Failed to initialize the TrialPhaseCanvas.");
    }

    /// <summary>
    /// Set the current canvas to the matching 'phase'.
    /// </summary>
    /// <param name="phase">String to specify the phase canvas to be switched to.</param>
    public void SetPhase(string phase)
    {
        DuskPhaseCanvas.gameObject.SetActive(phase == "DuskPhaseCanvas");
        NightPhaseCanvas.gameObject.SetActive(phase == "NightPhaseCanvas");
        MorningPhaseCanvas.gameObject.SetActive(phase == "MorningPhaseCanvas");
        DayPhaseCanvas.gameObject.SetActive(phase == "DayPhaseCanvas");
        TrialPhaseCanvas.gameObject.SetActive(phase == "TrialPhaseCanvas");

        Debug.Log("Switched to " + phase); //Leave in when cleaning up
    }
}

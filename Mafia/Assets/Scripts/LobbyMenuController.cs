using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenuController : MonoBehaviour {
    //public object(s) set in inspector
    public Canvas RoomCanvas;
    public Canvas JoinGameCanvas;

    /// <summary>
    /// Check that each canvas is initialized.
    /// </summary>
    void Awake() {
        if (JoinGameCanvas == null)
            Debug.Log("Could not initialize JoinGameCanvas.");
        if (RoomCanvas == null)
            Debug.Log("Could not initialize RoomCanvas.");
    }

	// Use this for initialization
    // The lobby menu system should start from the JoinGameCanvas,
    // so we change to the JoinGameCanvas if we haven't.
	void Start () {
        JoinGameCanvasOn();
	}

    /// <summary>
    /// Set the current canvas to the JoinGameCanvas
    /// </summary>
    public void JoinGameCanvasOn() {
        ChangeCanvas("JoinGameCanvas");
    }

    /// <summary>
    /// Set the current canvas to the RoomCanvas.
    /// </summary>
    public void RoomCanvasOn() {
        ChangeCanvas("RoomCanvas");
    }

    /// <summary>
    /// Set the current canvas that is the same as 'menu'.
    /// </summary>
    /// <param name="menu">String to set the displayed canvas to.</param>
    public void ChangeCanvas(string menu) {
        JoinGameCanvas.gameObject.SetActive(menu == "JoinGameCanvas");
        RoomCanvas.gameObject.SetActive(menu == "RoomCanvas");
    }
}

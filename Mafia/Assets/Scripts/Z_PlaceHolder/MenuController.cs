using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    //public objects set in inspector
    public Canvas StartScreen;
    public Canvas LoginMenu;
    public Canvas CreateAccount;
    public Canvas MainMenu;
    public Canvas OptionCanvas;
    public Canvas Stats;
    //public Canvas JoinGameCanvas;

    /// <summary>
    /// Check that each canvas is initialized.
    /// </summary>
    private void Awake()
    {
        if (StartScreen == null)
            Debug.Log("Could not initialize StartScreen.");
        if (LoginMenu == null)
            Debug.Log("Could not initialize LoginMenu.");
        if (CreateAccount == null)
            Debug.Log("Could not initialize CreateAccount.");
        if (MainMenu == null)
            Debug.Log("Could not initialize MainMenu.");
        if (OptionCanvas == null)
            Debug.Log("Could not initialize OptionCanvas.");
        if (Stats == null)
            Debug.Log("Could not initialize Stats.");
        //if (JoinGameCanvas == null)
            //Debug.Log("Could not initialize JoinGameCanvas.");
    }

    /// <summary>
    /// Set the current canvas that is the same as 'menu'.
    /// </summary>
    /// <param name="menu">String to set the displayed canvas to.</param>
    public void SetMenu(string menu)
    {
        StartScreen.gameObject.SetActive(menu == "StartScreen");
        LoginMenu.gameObject.SetActive(menu == "LoginMenu");
        CreateAccount.gameObject.SetActive(menu == "CreateAccount");
        MainMenu.gameObject.SetActive(menu == "MainMenu");
        OptionCanvas.gameObject.SetActive(menu == "OptionCanvas");
        Stats.gameObject.SetActive(menu == "Stats");
        //JoinGameCanvas.gameObject.SetActive(menu == "JoinGameCanvas");
    }
}

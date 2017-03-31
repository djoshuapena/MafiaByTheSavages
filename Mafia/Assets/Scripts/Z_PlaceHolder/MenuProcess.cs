using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuProcess : MonoBehaviour {

    public MenuController menuCont;

    /// <summary>
    /// Start the scene into the proper menu screen.
    /// </summary>
    public void MenuStart()
    {
        if (PhotonNetwork.connectionStateDetailed.ToString().Equals("ConnectedToMaster")) //aaron; this is here to bypass the log in screen when returning to menu
        {
            menuCont.SetMenu("MainMenu");
        }
        else
        {
            menuCont.SetMenu("StartScreen"); //standard start
        }
    }

    /// <summary>
    /// Set the display canvas to 'menu'.
    /// </summary>
    /// <param name="menu">Menu to set the canvas to.</param>
    public void setNewMenu(string menu)
    {
        menuCont.SetMenu(menu);
    }
}

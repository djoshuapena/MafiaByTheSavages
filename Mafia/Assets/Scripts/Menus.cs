using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour {
    public Canvas StartScreen;
    public Canvas LoginMenu;
    public Canvas CreateAccount;
    public Canvas MainMenu;
    public Canvas OptionCanvas;

    void Start()
    {
        OptionCanvas.enabled = false;
        LoginMenu.enabled = false;
        CreateAccount.enabled = false;
        MainMenu.enabled = false;
    }
    
    public void LoginOn()
    {
        LoginMenu.enabled = true;
        StartScreen.enabled = false;
    }

    public void CreateOn()
    {
        CreateAccount.enabled = true;
        StartScreen.enabled = false;
    }

    public void MainOn()
    {
        MainMenu.enabled = true;
        if (LoginMenu.enabled)
            LoginMenu.enabled = false;
        else if (CreateAccount.enabled)
            CreateAccount.enabled = false;
    }

    public void Cancel()
    {
        StartScreen.enabled = true;
        if (LoginMenu.enabled)
            LoginMenu.enabled = false;
        else if (CreateAccount.enabled)
            CreateAccount.enabled = false;
    }

    public void OptionsOn()
    {
        OptionCanvas.enabled = true;
        MainMenu.enabled = false;
    }

    public void ExitOptions()
    {
        MainMenu.enabled = true;
        OptionCanvas.enabled = false;
    }
    public void Logout()
    {
        MainMenu.enabled = false;
        StartScreen.enabled = true;
    }

    public void StartLobby()
    {
        Application.LoadLevel(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour {
    //static public bool backOn = false;
    public Canvas StartScreen;
    public Canvas LoginMenu;
    public Canvas CreateAccount;
    public Canvas MainMenu;
    public Canvas OptionCanvas;
    //public Canvas LobbyManager;
    //public GameObject MainPanel;
    //public GameObject ServerList;

    void Awake()
    {
        if (StartScreen == null)
        {
            Debug.Log("Unable to detect StartScreen");
        }
        if (LoginMenu == null)
        {
            Debug.Log("Unable to detect LoginMenu");
        }
        if (CreateAccount == null)
        {
            Debug.Log("Unable to detect CreateAccount");
        }
        if (MainMenu == null)
        {
            Debug.Log("Unable to detect MainMenu");
        }
        if (OptionCanvas == null)
        {
            Debug.Log("Unable to detect OptionCanvas");
        }
        //if (LobbyManager == null)
        //{
        //    Debug.Log("Unable to detect LobbyManager");
        //}
    }

    public void SetMenu(Canvas theOneIWant)
    {
        StartScreen.gameObject.SetActive(theOneIWant == StartScreen);
        OptionCanvas.gameObject.SetActive(theOneIWant == OptionCanvas);
        LoginMenu.gameObject.SetActive(theOneIWant == LoginMenu);
        CreateAccount.gameObject.SetActive(theOneIWant == CreateAccount);
        MainMenu.gameObject.SetActive(theOneIWant == MainMenu);
        //LobbyManager.gameObject.SetActive(theOneIWant == LobbyManager);
    }

    //public void NetworkMenu(GameObject menu)
    //{
    //    MainPanel.SetActive(menu == MainPanel);
    //    ServerList.SetActive(menu == ServerList);
    //}

    public void Start()
    {
        SetMenu(StartScreen);
    }
    
    public void StartScreenOn()
    {
        SetMenu(StartScreen);
    }

    public void LoginOn()
    {
        SetMenu(LoginMenu);
    }

    public void CreateOn()
    {
        SetMenu(CreateAccount);
    }

    public void MainOn()
    {
        SetMenu(MainMenu);
    }

    public void OptionsOn()
    {
        SetMenu(OptionCanvas);
    }

    public void startServerList()
    {
        SceneManager.LoadScene(1);
    }
    //public void ListLobbyServers()
    //{
    //    backOn = true;
    //    SetMenu(LobbyManager);
    //    NetworkMenu(ServerList);
    //}

    //public void StartLobby()
    //{
    //    backOn = false;
    //    SetMenu(LobbyManager);
    //    NetworkMenu(MainPanel);
    //}
}

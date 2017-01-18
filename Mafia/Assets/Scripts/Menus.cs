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
    }

    private void SetMenu(Canvas menu)
    {
        StartScreen.gameObject.SetActive(menu == StartScreen);
        LoginMenu.gameObject.SetActive(menu == LoginMenu);
        CreateAccount.gameObject.SetActive(menu == CreateAccount);
        MainMenu.gameObject.SetActive(menu == MainMenu);
        OptionCanvas.gameObject.SetActive(menu == OptionCanvas);
    }

    void Start()
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
}

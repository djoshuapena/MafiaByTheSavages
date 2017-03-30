using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTester : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        PhotonPlayer sheriff = new PhotonPlayer(true, 1, "Bob");
        sheriff.SetCustomProperties("role", "sheriff");
        sheriff = sheriff.SetCustomProperties("dead", false);

        PhotonPlayer mafia = new PhotonPlayer(true, 2, "Jerry");
        mafia.SetCustomProperties("role", "mafia");
        mafia = mafia.SetCustomProperties("dead", true);

        Debug.Log("Sheriff alive");
        Debug.Log("Mafia dead");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

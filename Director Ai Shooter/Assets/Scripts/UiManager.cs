using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Text killsText;
    [SerializeField] private Text generatorsOnlineText;

    private void Update()
    {
        killsText.text            = "KILLS: "             + player.GetKillCount();
        generatorsOnlineText.text = "GENERATORS: " + Generator.GeneratorsOnline;
    }
}


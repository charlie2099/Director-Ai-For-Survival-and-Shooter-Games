using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DirectorDebug : MonoBehaviour
{
    [SerializeField] private Text enemyPopCountText;
    [SerializeField] private Text enemySpawnTimeText;
    [SerializeField] private Text directorStateText;
    [SerializeField] private Text perceivedIntensityText;
    [SerializeField] private Text elapsedTimeText;

    [Space]
    [SerializeField] private UnityEvent unityEvent;

    private void Update()
    {
        enemyPopCountText.text      = "Enemy Population Count: " + Director.Instance.GetEnemyPopulationCount();
        enemySpawnTimeText.text     = "Enemy Spawn Timer: "      + "";
        directorStateText.text      = "Director State: "         + Director.Instance.GetTempo();
        perceivedIntensityText.text = "Perceived Intensity: "    + Director.Instance.GetPerceivedIntensity().ToString("F2");
        elapsedTimeText.text        = "Elapsed Time: "           + Time.time.ToString("F2");
    }
}


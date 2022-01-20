using System;
using UnityEngine;
using UnityEngine.Events;

public class Director : MonoBehaviour
{
    public static Director Instance;

    public enum Tempo 
    {
        BuildUp,
        Peak,
        Respite
    }

    [Header("Debug")]
    [SerializeField] private bool debugMode = false;
    [SerializeField] private GameObject debugPanel;

    [Header("Attributes?")]
    [SerializeField] private float perceivedIntensity;

    [Header("Tempo Control")]
    [SerializeField] [Range(70, 100)] private int peakThreshold;
    [SerializeField] [Range(30, 70)] private int buildUpThreshold;
    [SerializeField] [Range(0, 30)] private int respiteThreshold;
    private Tempo _currentTempo;

    [Header("Entity Data")]
    //[SerializeField] private PlayerData[] players;
    //[SerializeField] private EnemyData[] enemies;
    //[SerializeField] private ItemData[] items;
    //[SerializeField] private GameObject[] players;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] items;

    [Header("Entity Spawn Locations")]
    [SerializeField] private int maxItemSpawns;
    [SerializeField] private int maxEnemySpawns;
    [SerializeField] private GameObject[] spawnpointContainers;

    [Header("Events")] 
    [Space]
    [SerializeField] private UnityEvent unityEvent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentTempo = Tempo.BuildUp;
        debugPanel.SetActive(debugMode);
    }

    public float GetEnemyPopulationCount()
    {
        return ActiveAreaSet.EnemyPopulationCount;
    }

    public float GetPerceivedIntensity()
    {
        return perceivedIntensity;
    }

    public Tempo GetTempo()
    {
        return _currentTempo;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void IncreaseIntensity(float amount)
    {
        perceivedIntensity += amount;
    }
    
    public void DecreaseIntensity()
    {
        perceivedIntensity--;
    }
}


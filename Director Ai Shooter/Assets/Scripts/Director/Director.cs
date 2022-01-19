using System;
using UnityEngine;
using UnityEngine.Events;

public class Director : MonoBehaviour
{
    public static Director Instance;
    
    [Header("Attributes?")]
    [SerializeField] private float perceivedIntensity;
    [SerializeField] private int activeEntities;

    [Header("Director State")]
    [SerializeField] [Range(70, 100)] private int peakThreshold;
    [SerializeField] [Range(30, 70)] private int buildUpThreshold;
    [SerializeField] [Range(0, 30)] private int respiteThreshold;
    //[SerializeField] private DirectorState activeState;
    [SerializeField] private DirectorState.Phase activePhase;

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
    [Tooltip("All the possible spawn locations for an entity such as items, powerups, weapons etc. Enemies however are spawned with the ActiveAreaSet so do not include them here.")]
    [SerializeField] private GameObject[] spawnpointContainers;

    [Header("Events")] 
    [SerializeField] private UnityEvent _unityEvent;

    private void Awake()
    {
        Instance = this;
    }

    public float GetPerceivedIntensity()
    {
        return perceivedIntensity;
    }
    
    public GameObject GetPlayer()
    {
        return player;
    }
}


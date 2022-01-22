using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [HideInInspector] public List<GameObject> activeEnemies = new List<GameObject>();
    [HideInInspector] public int maxPopulationCount;
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

    private void Update()
    {
        CheckDistanceFromPlayer();
        IntensityFSM();
        TempoFSM();
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
        if (perceivedIntensity > 100)
        {
            perceivedIntensity = 100;
        }
    }
    
    public void DecreaseIntensity(float amount)
    {
        perceivedIntensity -= amount;
        if (perceivedIntensity < 0)
        {
            perceivedIntensity = 0;
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }
    
    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public void CheckDistanceFromPlayer()
    {
        Vector2 playerPos = player.transform.position;

        foreach (var enemy in activeEnemies)
        {
            if (enemy != null)
            {
                Vector2 enemyPos = enemy.transform.position;
                if (Vector2.Distance(playerPos, enemyPos) < 3) // TODO: Variable
                {
                    enemy.GetComponentInChildren<SpriteRenderer>().color = Color.magenta;
                    IncreaseIntensity(0.001f * Time.time);
                }
                else
                {
                    enemy.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }

    public void TempoFSM()
    {
        switch (_currentTempo)
        {
            case Tempo.BuildUp:
                maxPopulationCount = 10;
                break;
            case Tempo.Peak:
                maxPopulationCount = 20;
                break;
            case Tempo.Respite:
                maxPopulationCount = 1;
                break;
            default:
                break;
        }
    }

    public void IntensityFSM()
    {
        if (perceivedIntensity > 0 && perceivedIntensity < 70)
        {
            _currentTempo = Tempo.BuildUp;
        }
        else if (perceivedIntensity >= 70)
        {
            _currentTempo = Tempo.Peak;
        }
        else
        {
            _currentTempo = Tempo.Respite;
        }
    }

    public IEnumerator CheckIfIntensityShouldBeginDeclining()
    {
        float intensityCheck = perceivedIntensity;
        yield return new WaitForSeconds(5.0f);
        if (intensityCheck == perceivedIntensity)
        {
            DecreaseIntensity(0.001f * Time.time);
        }
        
        // 5.0    10 seconds later    10.0     
    }
}


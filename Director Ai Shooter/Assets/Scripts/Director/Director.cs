using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Director : MonoBehaviour
{
    public static Director Instance;

    public enum Tempo 
    {
        BuildUp,
        Peak,
        PeakFade,
        Respite
    }

    [Header("DEBUG")]
    [SerializeField] private bool debugMode = false;
    [SerializeField] private GameObject debugPanel;

    //[Header("Attributes?")]
    private float _perceivedIntensity;

    [Header("INTENSITY ADJUSTMENT")] 
    [SerializeField] private float distanceFromPlayer;

    [Header("TEMPO")]
    [SerializeField] [Range(70, 100)] private int peakIntensityThreshold;
    [Space]
    
    [Tooltip("Default value, but can be dynamically altered by the Director")]
    [SerializeField] private float defaultPeakDuration;
    [SerializeField] private float defaultRespiteDuration;
    [Space]
    [SerializeField] private int maxBuildUpPopulation;
    [SerializeField] private int maxPeakPopulation;
    [SerializeField] private int maxRespitePopulation;
    private Tempo _currentTempo;

    [Header("PLAYER DATA")]
    [SerializeField] private GameObject player;
    //[SerializeField] private PlayerData[] playerData;

    [Header("ENEMY DATA")]
    //[SerializeField] private EnemyData enemies;
    [HideInInspector] public List<GameObject> activeEnemies = new List<GameObject>();
    [HideInInspector] public int maxPopulationCount;
    
    [Header("ITEMS")]
    [SerializeField] private int maxItemSpawns;
    [SerializeField] private GameObject[] items;
    //[SerializeField] private ItemData[] items;

    [Header("RANDOMISE ON PLAY")]
    [SerializeField] private GameObject[] objectContainers;

    [Header("Events")] 
    [Space]
    [SerializeField] private UnityEvent inProximityToEnemy;

    private float _timeSpentInPeak;
    private float _timeSpentInRespite;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timeSpentInPeak = defaultPeakDuration;
        _timeSpentInRespite = defaultRespiteDuration;
        
        debugPanel.SetActive(debugMode);
        
        RandomiseSpawnOnPlay();
    }

    private void Update()
    {
        CheckDistanceFromPlayer();
        IntensityFSM();
        TempoFSM();

        // if PEAK tempo and X amount of time has passed, next state 
        if(_timeSpentInPeak <= 0 && _currentTempo == Tempo.Peak)
        {
            _perceivedIntensity = 0;
            _currentTempo = Tempo.PeakFade;
            _timeSpentInPeak = defaultPeakDuration;
        }

        if (_currentTempo == Tempo.PeakFade && GetEnemyPopulationCount() == 0)
        {
            _currentTempo = Tempo.Respite;
        }
        
        // if RESPITE tempo and X amount of time has passed, next state
        if (_timeSpentInRespite <= 0 && _currentTempo == Tempo.Respite)
        {
            _perceivedIntensity = 0.1f;
            _currentTempo = Tempo.BuildUp;
            _timeSpentInRespite = defaultRespiteDuration;
        }
    }

    public float GetEnemyPopulationCount()
    {
        return ActiveAreaSet.EnemyPopulationCount;
    }

    public float GetPerceivedIntensity()
    {
        return _perceivedIntensity;
    }

    public Tempo GetTempo()
    {
        return _currentTempo;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public float GetPeakDuration()
    {
        return _timeSpentInPeak;
    }

    public float GetRespiteDuration()
    {
        return _timeSpentInRespite;
    }

    public void IncreaseIntensity(float amount)
    {
        //_perceivedIntensity += amount;
        _perceivedIntensity += amount * Time.deltaTime;
        if (_perceivedIntensity > 100)
        {
            _perceivedIntensity = 100;
        }
    }
    
    public void DecreaseIntensity(float amount)
    {
        _perceivedIntensity -= amount;
        if (_perceivedIntensity < 0)
        {
            _perceivedIntensity = 0;
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
        if (player != null && _currentTempo != Tempo.PeakFade)
        {
            Vector2 playerPos = player.transform.position;

            foreach (var enemy in activeEnemies)
            {
                if (enemy != null)
                {
                    Vector2 enemyPos = enemy.transform.position;
                    if (Vector2.Distance(playerPos, enemyPos) < distanceFromPlayer) // TODO: Variable
                    {
                        //enemy.GetComponentInChildren<SpriteRenderer>().color = Color.magenta;
                        inProximityToEnemy.Invoke();
                        //IncreaseIntensity(0.001f * Time.time);
                    }
                    else
                    {
                        //enemy.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    }
                }
            }
        }
    }

    public void TempoFSM()
    {
        switch (_currentTempo)
        {
            case Tempo.BuildUp:
                maxPopulationCount = maxBuildUpPopulation;
                break;
            case Tempo.Peak:
                maxPopulationCount = maxPeakPopulation;
                break;
            case Tempo.PeakFade:
                maxPopulationCount = 0;
                _perceivedIntensity = 0;
                break;
            case Tempo.Respite:
                maxPopulationCount = maxRespitePopulation;
                break;
        }
    }

    public void IntensityFSM()
    {
        if (_perceivedIntensity > 0 && _perceivedIntensity < peakIntensityThreshold)
        {
            _currentTempo = Tempo.BuildUp;
            //_perceivedIntensity += 0.1f * Time.deltaTime;
            IncreaseIntensity(0.1f);
        }
        else if (_perceivedIntensity >= peakIntensityThreshold)
        {
            _currentTempo = Tempo.Peak;
            _timeSpentInPeak -= Time.deltaTime;
        }
        else if(_currentTempo != Tempo.PeakFade)
        {
            _currentTempo = Tempo.Respite;
            _timeSpentInRespite -= Time.deltaTime;
        }
    }

    private void RandomiseSpawnOnPlay()
    {
        if (objectContainers != null)
        {
            // iterates through containers
            foreach (var entity in objectContainers)
            {
                // iterates through items within each container
                for (var j = 0; j < entity.transform.childCount; j++)
                {
                    var randBool = (Random.Range(0, 2) == 0);
                    entity.transform.GetChild(j).gameObject.SetActive(randBool);
                }
            }
        }
    }

    public IEnumerator CheckIfIntensityShouldBeginDeclining()
    {
        float intensityCheck = _perceivedIntensity;
        yield return new WaitForSeconds(5.0f);
        if (intensityCheck == _perceivedIntensity)
        {
            DecreaseIntensity(0.001f * Time.time);
        }
        
        // 5.0    10 seconds later    10.0     
    }
}


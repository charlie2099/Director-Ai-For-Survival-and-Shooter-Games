using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AiDirector.AAS;
using AiDirector.RulesSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AiDirector
{
    public class Director : MonoBehaviour
    {
        public static Director Instance;
        public DirectorState directorState = new DirectorState();
        public DirectorIntensityCalculator directorIntensityCalculator = new DirectorIntensityCalculator();
        public DirectorGameEventCalculator directorGameEventCalculator = new DirectorGameEventCalculator();

        private enum Difficulty
        {
            EASY, 
            NORMAL,
            HARD
        }
        [SerializeField] private Difficulty difficulty;

        [Header("INTENSITY")]
        [SerializeField] private float intensityCalculationRate = 1.0f;

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

        [Header("ENEMY DATA")]
        [HideInInspector] public List<GameObject> activeEnemies = new List<GameObject>();
        [HideInInspector] public int maxPopulationCount;
        
        [Header("PLAYER DATA")]
        [SerializeField] private Player player;

        [Header("ITEMS")]
        [SerializeField] private int maxItemSpawns;
        [SerializeField] private List<GameObject> items;

        [Header("RANDOMISE ON PLAY")]
        [SerializeField] private GameObject[] objectContainers;
        
        [Header("DEBUG")]
        [SerializeField] private bool debugMode = false;
        [SerializeField] private GameObject debugPanel;

        private ActiveAreaSet _activeAreaSet;
        private Dictionary<GameObject, Vector2> _itemSpawnLocationsDictionary = new Dictionary<GameObject, Vector2>();
        private Dictionary<string, GameObject> _itemPrefabDictionary = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject> _itemContainerDictionary = new Dictionary<string, GameObject>();
        private float _perceivedIntensity;
        private float _timeSpentInPeak;
        private float _timeSpentInRespite;
        private float _timer;
        private float _timePassed4;
        private float _timePassed5;
        private int _intensityScaler;

        private void OnEnable()
        {
            directorState.OnTempoChange += TriggerGameEvent;
        }

        private void OnDisable()
        {
            directorState.OnTempoChange -= TriggerGameEvent;
        }
        
        public void TriggerGameEvent()
        {
            directorGameEventCalculator.CalculateGameEventOutput(this);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Only one instance of the Director should exist at any one time");
            }

            _activeAreaSet = GetComponent<ActiveAreaSet>();
        }

        private void Start()
        {
            if (difficulty == Difficulty.EASY)
            {
                _intensityScaler = 40;
            }
            else if (difficulty == Difficulty.NORMAL)
            {
                _intensityScaler = 60;
            }
            else if (difficulty == Difficulty.HARD)
            {
                _intensityScaler = 80;
            }
            
            _timeSpentInPeak = defaultPeakDuration;
            _timeSpentInRespite = defaultRespiteDuration;
            _timePassed4 = intensityCalculationRate;
        
            debugPanel.SetActive(debugMode);
        
            RandomiseItemSpawnOnPlay();

            directorState.CurrentTempo = DirectorState.Tempo.Respite;
        }
        

        private void Update()
        {
            TempoFsm();

            if (_perceivedIntensity >= peakIntensityThreshold && directorState.CurrentTempo == DirectorState.Tempo.BuildUp)
            {
                directorState.CurrentTempo = DirectorState.Tempo.Peak;
            }

            // if PEAK tempo and X amount of time has passed, change state to PEAK-FADE
            if(_timeSpentInPeak <= 0 && directorState.CurrentTempo == DirectorState.Tempo.Peak)
            {
                _perceivedIntensity = 0;
                directorState.CurrentTempo = DirectorState.Tempo.PeakFade;
                _timeSpentInPeak = defaultPeakDuration; // unreachable
            }

            // If PEAK-FADE tempo and all enemies killed, change state to RESPITE 
            if (GetEnemyPopulationCount() == 0 && directorState.CurrentTempo == DirectorState.Tempo.PeakFade)
            {
                directorState.CurrentTempo = DirectorState.Tempo.Respite;
            }
        
            // if RESPITE tempo and X amount of time has passed, change state to BUILD-UP
            if (_timeSpentInRespite <= 0 && directorState.CurrentTempo == DirectorState.Tempo.Respite)
            {
                _perceivedIntensity = 0.1f;
                directorState.CurrentTempo = DirectorState.Tempo.BuildUp;
                _timeSpentInRespite = defaultRespiteDuration; // unreachable
            }
        }

        public float GetEnemyPopulationCount()
        {
            return _activeAreaSet.GetEnemyPopulationCount();
        }

        public float GetPerceivedIntensity()
        {
            return _perceivedIntensity;
        }

        public Player GetPlayer()
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

        public void IncreasePerceivedIntensityMetric()
        {
            // Calculates intensity every second
            if (Time.time > _timePassed4)
            {
                float intensity = directorIntensityCalculator.CalculatePerceivedIntensityOutput(this);
                //print("Current Intensity: <color=orange>" + intensity + "</color>");
                _perceivedIntensity += intensity * _intensityScaler * Time.deltaTime;
            
                if (_perceivedIntensity > 100)
                {
                    _perceivedIntensity = 100;
                }
                
                _timePassed4 = Time.time + intensityCalculationRate;
            }
        }

        public void DecreasePerceivedIntensityMetric(float amount)
        {
            _perceivedIntensity -= amount;
            if (_perceivedIntensity < 0)
            {
                _perceivedIntensity = 0;
            }
        }
        
        public float GetIntensityCalculationRate()
        {
            return intensityCalculationRate;
        }

        public int GetIntensityScalar()
        {
            return _intensityScaler;
        }

        public void AddEnemy(GameObject enemy)
        {
            activeEnemies.Add(enemy);
        }
    
        public void RemoveEnemy(GameObject enemy)
        {
            activeEnemies.Remove(enemy);
        }

        private void TempoFsm()
        {
            switch (directorState.CurrentTempo)
            {
                case DirectorState.Tempo.BuildUp:
                    maxPopulationCount = maxBuildUpPopulation;
                    IncreasePerceivedIntensityMetric();
                    break;
                
                case DirectorState.Tempo.Peak:
                    maxPopulationCount = maxPeakPopulation;
                    _timeSpentInPeak -= Time.deltaTime;
                    break;
                
                case DirectorState.Tempo.PeakFade:
                    maxPopulationCount = 0;
                    _perceivedIntensity = 0;
                    break;
                
                case DirectorState.Tempo.Respite:
                    maxPopulationCount = maxRespitePopulation;
                    _timeSpentInRespite -= Time.deltaTime;
                    break;
            }
        }

        public IEnumerator CheckIfIntensityShouldBeginDeclining()
        {
            float intensityCheck = _perceivedIntensity;
            yield return new WaitForSeconds(5.0f);
            if (intensityCheck == _perceivedIntensity)
            {
                DecreasePerceivedIntensityMetric(0.001f * Time.time);
            }
        
            // 5.0    10 seconds later    10.0     
        }

        public void SpawnBoss()
        {
            print("BOSS SPAWNED");
            _activeAreaSet.SpawnBoss();
        }

        public void TestMethod()
        {
            print("TEST METHOD CALLED");
        }
        
        private void RandomiseItemSpawnOnPlay()
        {
            if (objectContainers != null)
            {
                foreach (var container in objectContainers)
                {
                    // iterates through items within each container
                    for (var j = 0; j < container.transform.childCount; j++)
                    {
                        var randBool = (Random.Range(0, 2) == 0);
                        container.transform.GetChild(j).gameObject.SetActive(randBool);
            
                        var item = container.transform.GetChild(j).gameObject;
                        var itemPos = item.transform.position;
                        _itemSpawnLocationsDictionary.Add(item, itemPos);
                    }
                }

                foreach (var itemPrefab in items)
                {
                    _itemPrefabDictionary.Add(itemPrefab.name, itemPrefab);
                }

                foreach (var container in objectContainers)
                {
                    _itemContainerDictionary.Add(container.transform.GetChild(0).name, container);
                }
                
                /*foreach (var item in _itemSpawnLocationsDictionary.Keys) // generators, medkits, ammocrates
                {
                    _itemSpawnLocationsDictionary.Add(item, item.transform.position);
                }*/
            }
        }

        public void SpawnItem(string itemName)
        {
            foreach (var item in _itemSpawnLocationsDictionary.Keys) // generators, medkits, ammocrates
            {
                if (item == null)
                {
                    GameObject spawnedItem = Instantiate(_itemPrefabDictionary[itemName], _itemSpawnLocationsDictionary[item], Quaternion.identity);
                    spawnedItem.transform.parent = _itemContainerDictionary[itemName].transform;
                }
            }
        }
    }
}


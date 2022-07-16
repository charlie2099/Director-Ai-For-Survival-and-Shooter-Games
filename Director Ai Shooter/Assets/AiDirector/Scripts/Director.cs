using System.Collections.Generic;
using AiDirector.Scripts.AAS;
using AiDirector.Scripts.RulesSystem.RuleCalculators;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AiDirector.Scripts
{
    public class Director : MonoBehaviour
    {
        public static Director Instance;

        private enum Difficulty
        {
            Easy, 
            Normal,
            Hard
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

        private DirectorState _directorState = new DirectorState();
        private DirectorIntensityCalculator _directorIntensityCalculator = new DirectorIntensityCalculator();
        private DirectorGameEventCalculator _directorGameEventCalculator = new DirectorGameEventCalculator();
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
            _directorState.OnTempoChange += TriggerGameEvent;
        }

        private void OnDisable()
        {
            _directorState.OnTempoChange -= TriggerGameEvent;
        }

        private void TriggerGameEvent()
        {
            _directorGameEventCalculator.CalculateGameEventOutput(this);
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
            if (difficulty == Difficulty.Easy)
            {
                _intensityScaler = 30;
            }
            else if (difficulty == Difficulty.Normal)
            {
                _intensityScaler = 60;
            }
            else if (difficulty == Difficulty.Hard)
            {
                _intensityScaler = 90;
            }
            
            _timeSpentInPeak = defaultPeakDuration;
            _timeSpentInRespite = defaultRespiteDuration;
            _timePassed4 = intensityCalculationRate;

            RandomiseItemSpawnOnPlay();

            _directorState.CurrentTempo = DirectorState.Tempo.Respite;

            if (debugPanel == null)
            {
                Debug.LogError("The Debug Panel needs to been passed in!");
                return;
            }
            debugPanel.SetActive(debugMode);
        }

        private void Update()
        {
            TempoFsm();

            // if BUILD-UP tempo and perceived intensity has reached the peak threshold, change state to PEAK 
            if (_perceivedIntensity >= peakIntensityThreshold && _directorState.CurrentTempo == DirectorState.Tempo.BuildUp)
            {
                maxPopulationCount = maxPeakPopulation;
                _directorState.CurrentTempo = DirectorState.Tempo.Peak;
            }

            // if PEAK tempo and X amount of time has passed, change state to PEAK-FADE
            if(_timeSpentInPeak <= 0 && _directorState.CurrentTempo == DirectorState.Tempo.Peak)
            {
                maxPopulationCount = 0;
                _timeSpentInPeak = defaultPeakDuration; 
                _directorState.CurrentTempo = DirectorState.Tempo.PeakFade;
            }

            // If PEAK-FADE tempo and all enemies killed, change state to RESPITE 
            if (GetEnemyPopulationCount() == 0 && _directorState.CurrentTempo == DirectorState.Tempo.PeakFade)
            {
                maxPopulationCount = maxRespitePopulation;
                _directorState.CurrentTempo = DirectorState.Tempo.Respite;
            }
        
            // if RESPITE tempo and X amount of time has passed, change state to BUILD-UP
            if (_timeSpentInRespite <= 0 && _directorState.CurrentTempo == DirectorState.Tempo.Respite)
            {
                maxPopulationCount = maxBuildUpPopulation;
                _timeSpentInRespite = defaultRespiteDuration;
                _directorState.CurrentTempo = DirectorState.Tempo.BuildUp;
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

        private void IncreasePerceivedIntensityMetric()
        {
            // Calculates intensity every second
            if (Time.time > _timePassed4)
            {
                float intensity = _directorIntensityCalculator.CalculatePerceivedIntensityOutput(this);
                _perceivedIntensity += intensity * _intensityScaler * Time.deltaTime;
            
                if (_perceivedIntensity > 100)
                {
                    _perceivedIntensity = 100;
                }
                
                _timePassed4 = Time.time + intensityCalculationRate;
            }
        }

        private void DecreasePerceivedIntensityMetric()
        {
            // Calculates intensity every second
            if (Time.time > _timePassed4)
            {
                float intensity = _directorIntensityCalculator.CalculatePerceivedIntensityOutput(this);
                //print("Current Intensity: <color=orange>" + intensity + "</color>");
                _perceivedIntensity -= intensity * _intensityScaler * Time.deltaTime;
            
                if (_perceivedIntensity < 0)
                {
                    _perceivedIntensity = 0;
                }
                
                _timePassed4 = Time.time + intensityCalculationRate;
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
            switch (_directorState.CurrentTempo)
            {
                case DirectorState.Tempo.BuildUp:
                    IncreasePerceivedIntensityMetric();
                    break;
                
                case DirectorState.Tempo.Peak:
                    _timeSpentInPeak -= Time.deltaTime;
                    break;
                
                case DirectorState.Tempo.PeakFade:
                    DecreasePerceivedIntensityMetric();
                    break;
                
                case DirectorState.Tempo.Respite:
                    _timeSpentInRespite -= Time.deltaTime;
                    break;
            }
        }

        public void SpawnBoss()
        {
            print("BOSS SPAWNED");
            _activeAreaSet.SpawnBoss();
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

        public DirectorState GetDirectorState()
        {
            return _directorState;
        }
    }
}


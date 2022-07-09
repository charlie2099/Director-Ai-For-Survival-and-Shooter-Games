using System;
using System.Collections.Generic;
using Rules;
using UnityEngine;

namespace AiDirector
{
    public class Director : MonoBehaviour
    {
        public static Director Instance;

        //[SerializeField] private DirectorState _directorState;
        public enum Tempo 
        {
            BuildUp,
            Peak,
            PeakFade,
            Respite
        }
        
        //[Header("INTENSITY ADJUSTMENT")]

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
        [SerializeField] private PlayerTemplate playerTemplate;
        [SerializeField] private GameObject player;
        
        private Tempo _currentTempo;
        private float _perceivedIntensity;
        private int _killstreak;
        private float timer = 0.0f;
        private float _timeSpentInPeak;
        private float _timeSpentInRespite;

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
        }

        private void Start()
        {
            _timeSpentInPeak = defaultPeakDuration;
            _timeSpentInRespite = defaultRespiteDuration;
        }

        private void Update()
        {
            IntensityFsm();
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
            
            print("Perceived Intensity: <color=red>" + GetPerceivedIntensity()+ "</color>");
            //print("Director State: <color=magenta>" + GetTempo() + "</color>");
        }

        /*public float IncreaseIntensity(float intensity)
        {
            _intensity += intensity;
            return _intensity;
        }
        
        public float DecreaseIntensity(float intensity)
        {
            _intensity -= intensity;
            return _intensity;
        }*/

        public float GetEnemyPopulationCount()
        {
            return ActiveAreaSet.EnemyPopulationCount;
        }
        
        public float GetPerceivedIntensity()
        { 
            return _perceivedIntensity;
        }

        public GameObject GetPlayer()
        {
            return player;
        }

        public bool EnemyKilled()
        {
            return true;
        }

        public int GetKillStreak()
        {
            return _killstreak;
        }
        
        public Tempo GetTempo()
        {
            return _currentTempo;
        }
        
        public void AddEnemy(GameObject enemy)
        {
            activeEnemies.Add(enemy);
        }
    
        public void RemoveEnemy(GameObject enemy)
        {
            activeEnemies.Remove(enemy);
        }

        private void TempoFSM()
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

        private void IntensityFsm()
        {
            if (_perceivedIntensity > 0 && _perceivedIntensity < peakIntensityThreshold)
            {
                _currentTempo = Tempo.BuildUp;
                //_perceivedIntensity += 0.1f * Time.deltaTime;
                //IncreaseIntensity(0.1f);
                IncreaseIntensity();
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

        private void IncreaseIntensity()
        {
            float intensity = DirectorIntensityCalculator.Instance.CalculatePerceivedIntensityPercentage(playerTemplate, this);
            //print("Current Intensity: <color=orange>" + intensity + "</color>");
            _perceivedIntensity += intensity * Time.deltaTime;
            
            if (_perceivedIntensity > 100)
            {
                _perceivedIntensity = 100;
            }
        }

        private void DecreaseIntensity(float amount)
        {
            _perceivedIntensity -= amount;
            if (_perceivedIntensity < 0)
            {
                _perceivedIntensity = 0;
            }
        }
    }
}
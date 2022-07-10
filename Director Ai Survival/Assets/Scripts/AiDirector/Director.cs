using System;
using System.Collections.Generic;
using Rules;
using RulesSystem;
using UnityEngine;

namespace AiDirector
{
    public class Director : MonoBehaviour
    {
        public static Director Instance;
        public DirectorState directorState = new DirectorState();

        [Header("INTENSITY")]
        [SerializeField] private float intensityCalculationRate = 1.0f;
        [SerializeField] private int intensityScaler = 60;

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

        private float _perceivedIntensity;
        private int _killstreak;
        private float timer = 0.0f;
        private float _timeSpentInPeak;
        private float _timeSpentInRespite;
        private float _timePassed4;

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
            _timePassed4 = intensityCalculationRate;
        }

        private void Update()
        {
            IntensityFsm();
            TempoFSM();

            // if PEAK tempo and X amount of time has passed, next state 
            if(_timeSpentInPeak <= 0 && directorState.CurrentTempo == DirectorState.Tempo.Peak)
            {
                _perceivedIntensity = 0;
                directorState.CurrentTempo = DirectorState.Tempo.PeakFade;
                _timeSpentInPeak = defaultPeakDuration;
            }

            if (directorState.CurrentTempo == DirectorState.Tempo.PeakFade && GetEnemyPopulationCount() == 0)
            {
                directorState.CurrentTempo = DirectorState.Tempo.Respite;
            }
        
            // if RESPITE tempo and X amount of time has passed, next state
            if (_timeSpentInRespite <= 0 && directorState.CurrentTempo == DirectorState.Tempo.Respite)
            {
                _perceivedIntensity = 0.1f;
                directorState.CurrentTempo = DirectorState.Tempo.BuildUp;
                _timeSpentInRespite = defaultRespiteDuration;
            }
            
            print("Perceived Intensity: <color=red>" + GetPerceivedIntensity()+ "</color>");
            //print("Director State: <color=magenta>" + directorState.CurrentTempo + "</color>");
        }

        public float GetEnemyPopulationCount()
        {
            return ActiveAreaSet.EnemyPopulationCount;
        }
        
        public float GetPerceivedIntensity()
        { 
            return _perceivedIntensity;
        }

        public Player GetPlayer()
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
            switch (directorState.CurrentTempo)
            {
                case DirectorState.Tempo.BuildUp:
                    maxPopulationCount = maxBuildUpPopulation;
                    break;
                case DirectorState.Tempo.Peak:
                    maxPopulationCount = maxPeakPopulation;
                    break;
                case DirectorState.Tempo.PeakFade:
                    maxPopulationCount = 0;
                    _perceivedIntensity = 0;
                    break;
                case DirectorState.Tempo.Respite:
                    maxPopulationCount = maxRespitePopulation;
                    break;
            }
        }

        private void IntensityFsm()
        {
            if (_perceivedIntensity > 0 && _perceivedIntensity < peakIntensityThreshold)
            {
                directorState.CurrentTempo = DirectorState.Tempo.BuildUp;
                //_perceivedIntensity += 0.1f * Time.deltaTime;
                //IncreaseIntensity(0.1f);
                IncreaseIntensity(); // TODO: Define intensity calculation rate (currently every frame!)
            }
            else if (_perceivedIntensity >= peakIntensityThreshold)
            {
                directorState.CurrentTempo = DirectorState.Tempo.Peak;
                _timeSpentInPeak -= Time.deltaTime;
            }
            else if(directorState.CurrentTempo != DirectorState.Tempo.PeakFade)
            {
                directorState.CurrentTempo = DirectorState.Tempo.Respite;
                _timeSpentInRespite -= Time.deltaTime;
            }
        }

        private void IncreaseIntensity()
        {
            // Calculates intensity every second
            if (Time.time > _timePassed4)
            {
                float intensity = DirectorIntensityCalculator.Instance.CalculatePerceivedIntensityPercentage(this);
                //print("Current Intensity: <color=orange>" + intensity + "</color>");
                _perceivedIntensity += intensity * intensityScaler * Time.deltaTime;
            
                if (_perceivedIntensity > 100)
                {
                    _perceivedIntensity = 100;
                }
                
                _timePassed4 = Time.time + intensityCalculationRate;
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

        public float GetIntensityCalculationRate()
        {
            return intensityCalculationRate;
        }

        public int GetIntensityScalar()
        {
            return intensityScaler;
        }
    }
}
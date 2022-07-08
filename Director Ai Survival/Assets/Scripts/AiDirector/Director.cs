using System;
using Rules;
using UnityEngine;

namespace AiDirector
{
    public class Director : MonoBehaviour
    {
        [SerializeField] private PlayerTemplate playerTemplate;
        [SerializeField] private Transform[] enemies;
        private float _intensity;
        private int _enemyPopCount;
        private int _killstreak;

        private float timer = 0.0f;

        private void Update()
        {
            if (Time.time > timer)
            {
                print("Current Intensity: " + DirectorIntensityCalculator.Instance.CalculatePerceivedIntensityPercentage(playerTemplate, this));
                //timer = Time.time + 1.0f;
            }
        }

        public float IncreaseIntensity(float intensity)
        {
            _intensity += intensity;
            return _intensity;
        }
        
        public float DecreaseIntensity(float intensity)
        {
            _intensity -= intensity;
            return _intensity;
        }

        public int GetEnemyPopulation()
        {
            // ActiveAreaSet.EnemyPopulationCount;
            return _enemyPopCount;
        }
        
        public float GetPerceivedIntensity()
        { 
            return _intensity;
        }

        public Transform[] GetEnemyPositions()
        {
            return enemies;
        }

        public bool EnemyKilled()
        {
            return true;
        }

        public int GetKillStreak()
        {
            return _killstreak;
        }
    }
}
using AiDirector.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.RulesSystem.Rules
{
    public class DistanceFromEnemyRule : IDirectorIntensityRule
    {
        private float _intensity;
        private float _distance;

        public DistanceFromEnemyRule(float distance, float intensity)
        {
            _distance = distance;
            _intensity = intensity;
        }

        public float CalculatePerceivedIntensity(Director director)
        {
            Vector2 currentPos = director.GetPlayer().transform.position;
            
            foreach (var enemy in director.activeEnemies)
            {
                float distanceFromPlayerToEnemy = Vector2.Distance(currentPos, enemy.transform.position);

                if (distanceFromPlayerToEnemy < _distance)
                {
                    return _intensity;
                }
            }
            return 0;
        }
    }
}

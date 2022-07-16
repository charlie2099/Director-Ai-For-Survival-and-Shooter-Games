using AiDirector.Scripts.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.Scripts.RulesSystem.Rules.IntensityRules
{
    public class KillsAtADistanceRule : IDirectorIntensityRule
    {
        private readonly int _kills;
        private readonly float _distance;
        private readonly float _intensity;

        public KillsAtADistanceRule(int kills, float distance, float intensity)
        {
            _kills = kills;
            _distance = distance;
            _intensity = intensity;

            //DistanceFromEnemyRule distanceFromEnemyRule = new DistanceFromEnemyRule(10, 1);
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
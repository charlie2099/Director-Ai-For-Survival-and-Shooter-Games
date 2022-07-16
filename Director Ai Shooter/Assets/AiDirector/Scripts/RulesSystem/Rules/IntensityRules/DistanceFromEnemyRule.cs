using AiDirector.Scripts.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.Scripts.RulesSystem.Rules.IntensityRules
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
            // Avoid using LINQ expressions in update as it produces some garbage
            /*Vector2 playerPos = director.GetPlayer().transform.position;
            var closestEnemy = director.activeEnemies
                .OrderBy(t => Vector3.Distance(playerPos, t.transform.position))
                .FirstOrDefault();*/

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

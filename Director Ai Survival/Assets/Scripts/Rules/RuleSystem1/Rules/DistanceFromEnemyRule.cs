using System;
using AiDirector;
using Unity.VisualScripting;
using UnityEngine;

namespace Rules
{
    public class DistanceFromEnemyRule : IDirectorIntensityRule
    {
        private float _intensity;
        private float _distance;
        private float _distanceToClosestEnemy;

        public DistanceFromEnemyRule(float distance, float intensity)
        {
            _distance = distance;
            _intensity = intensity;
        }

        private float GetClosestEnemy(PlayerTemplate player, Director director)
        {
            foreach (var enemy in director.GetEnemyPositions())
            {
                float distanceFromPlayerToEnemy = Vector2.Distance(player.transform.position, enemy.position); 
                //Debug.Log("Distance to enemy [" + enemy.GetInstanceID() + "] :" + distanceFromPlayerToEnemy);
                _distanceToClosestEnemy = Math.Max(distanceFromPlayerToEnemy, _distanceToClosestEnemy);
                //Debug.Log("Distance to closest Enemy: " + _distanceToClosestEnemy);
            }
            //Debug.Log("Distance to closest Enemy: " + _distanceToClosestEnemy);
            return _distanceToClosestEnemy;
        }

        public float CalculatePerceivedIntensity(PlayerTemplate player, Director director)
        {
            //Debug.Log("Enemy positions container size: " + director.GetEnemyPositions().Length);
            if (GetClosestEnemy(player, director) > _distance)
            {
                _intensity = director.IncreaseIntensity(_intensity);
            }
            return _intensity;
        }
    }
}

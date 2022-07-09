using AiDirector;
using UnityEngine;

namespace RulesSystem.Rules
{
    public class DistanceFromEnemyRule : IDirectorIntensityRule
    {
        private float _intensity;
        private float _distance;
        private Transform _closestEnemy;

        public DistanceFromEnemyRule(float distance, float intensity)
        {
            _distance = distance;
            _intensity = intensity;
        }

        private float GetClosestEnemy(PlayerTemplate player, Director director)
        {
            float distanceToClosestEnemy = 0;
            Vector2 currentPos = player.transform.position;
            
            foreach (var enemy in director.activeEnemies)
            {
                float distanceFromPlayerToEnemy = Vector2.Distance(currentPos, enemy.transform.position);
                
                if (distanceFromPlayerToEnemy < _distance)
                {
                    distanceToClosestEnemy = distanceFromPlayerToEnemy;
                    _closestEnemy = enemy.transform;
                    _closestEnemy.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                }
                else 
                {
                    enemy.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                }
            }

            Debug.Log("Closest enemy [" + _closestEnemy.GetInstanceID() + "]: " + distanceToClosestEnemy);
            return distanceToClosestEnemy;
            // returns ZERO if not close to enemy!
            
            /*foreach (var enemy in director.GetEnemyPositions())
            {

                /*Vector2 directionToTarget = enemy.position - player.transform.position;
                float squaredDirectionToTarget = directionToTarget.sqrMagnitude;

                if (squaredDirectionToTarget < _distance)
                {
                    _distanceToClosestEnemy = squaredDirectionToTarget;
                }#1#
                //float distanceFromPlayerToEnemy = Vector2.Distance(player.transform.position, enemy.position);
                //_distanceToClosestEnemy = enemy.OrderBy(enemy => (transform.position - enemy.coords).sqrMagnitude);
                //_distanceToClosestEnemy = Mathf.Min(distanceFromPlayerToEnemy);
                //Debug.Log("Distance to enemy [" + enemy.GetInstanceID() + "] :" + distanceFromPlayerToEnemy);
                //_distanceToClosestEnemy = Math.Min(distanceFromPlayerToEnemy, _distanceToClosestEnemy);
                //_distanceToClosestEnemy = Math.Min(distanceFromPlayerToEnemy, _distanceToClosestEnemy);
                //discount = Math.Max(rule.CalculateCustomerDiscount(customer), discount);
                //Debug.Log("Distance to closest Enemy: " + _distanceToClosestEnemy);
            }
            Debug.Log("Distance to closest Enemy: " + _distanceToClosestEnemy);
            return _distanceToClosestEnemy;*/
        }

        public float CalculatePerceivedIntensity(PlayerTemplate player, Director director)
        {
            float distanceToClosestEnemy = 0;
            Vector2 currentPos = player.transform.position;
            
            foreach (var enemy in director.activeEnemies)
            {
                float distanceFromPlayerToEnemy = Vector2.Distance(currentPos, enemy.transform.position);
                
                if (distanceFromPlayerToEnemy < _distance)
                {
                    distanceToClosestEnemy = distanceFromPlayerToEnemy;
                    _closestEnemy = enemy.transform;
                    _closestEnemy.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    return _intensity;
                }
                else 
                {
                    enemy.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                }
            }
            return 0;
            
            /*//Debug.Log("Distance: " + _distance);
            //Debug.Log("Distance to closest enemy: " + GetClosestEnemy(player, director));

            // If closest enemy to player is less than the specified rule distance, increase intensity
            if (GetClosestEnemy(player, director) < _distance)
            {
                return _intensity; /* = director.IncreaseIntensity(_intensity)#2#
            }
            else
            {
                return 0;
            }*/
        }
    }
}

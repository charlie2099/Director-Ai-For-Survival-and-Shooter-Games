using AiDirector.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.RulesSystem.Rules
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
        }

        public float CalculatePerceivedIntensity(Director director)
        {
            foreach (var enemy in director.activeEnemies)
            {
                /*if (Vector2.Distance(director.GetPlayer().transform.position, enemy.transform.position) > _distance &&
                    director.EnemyKilled())
                {
                    //return director.IncreaseIntensity(_intensity);
                }*/
            }
            return _intensity;

            /*foreach (var enemy in director.GetEnemyPositions())
            {
                if (player.DistanceFrom(enemy) > _distance && director.EnemyKilled())
                {
                    return director.IncreaseIntensity(_intensity);
                }
            }
            return _intensity;*/

            /*if (player.killsAtADistance(_distance) > _kills)
            {
                return director.IncreaseIntensity(_intensity);
            }*/
        }
    }
}
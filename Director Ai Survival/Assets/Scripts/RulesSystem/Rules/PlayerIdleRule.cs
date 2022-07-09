using AiDirector;
using UnityEngine;

namespace RulesSystem.Rules
{
    public class PlayerIdleRule : IDirectorIntensityRule
    {
        private float _timeSpentIdle;
        private float _intensity;
        private float _clock;
        private Vector2 lastKnownPlayerPosition; 
        
        public PlayerIdleRule(float timeSpentIdle, float intensity)
        {
            _timeSpentIdle = timeSpentIdle;
            _intensity = intensity;
        }

        private bool PlayerIsIdle(PlayerTemplate player, Director director)
        {
            _clock += 1 * director.GetIntensityCalculationRate();

            if (_clock <= 1.0f)
            {
                lastKnownPlayerPosition = player.transform.position;
            }
            else if (_clock >= 5)
            {
                Vector2 newPlayerPosition = player.transform.position;
                if (newPlayerPosition == lastKnownPlayerPosition)
                {
                    Debug.Log("IDLE!");
                    return true;
                }
                _clock = 0;
            }
            return false;
        }

        public float CalculatePerceivedIntensity(PlayerTemplate player, Director director)
        {
            if (PlayerIsIdle(player, director))
            {
                return _intensity;
            }
            return 0;
        }
    }
}
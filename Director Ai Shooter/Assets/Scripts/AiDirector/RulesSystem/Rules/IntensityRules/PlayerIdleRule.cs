using AiDirector.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.RulesSystem.Rules.IntensityRules
{
    public class PlayerIdleRule : IDirectorIntensityRule
    {
        private float _timeSpentIdle;
        private float _intensity;
        private float _clock;
        private Vector2 _lastKnownPlayerPosition; 
        
        public PlayerIdleRule(float timeSpentIdle, float intensity)
        {
            _timeSpentIdle = timeSpentIdle;
            _intensity = intensity;
        }

        private bool PlayerIsIdle(Director director)
        {
            _clock += 1 * director.GetIntensityCalculationRate();

            if (_clock <= 1.0f)
            {
                _lastKnownPlayerPosition = director.GetPlayer().transform.position;
            }
            else if (_clock >= _timeSpentIdle)
            {
                Vector2 newPlayerPosition = director.GetPlayer().transform.position;
                if (newPlayerPosition == _lastKnownPlayerPosition)
                {
                    Debug.Log("IDLE!");
                    return true;
                }
                _clock = 0;
            }
            return false;
        }

        public float CalculatePerceivedIntensity(Director director)
        {
            if (PlayerIsIdle(director))
            {
                return _intensity;
            }
            return 0;
        }
    }
}
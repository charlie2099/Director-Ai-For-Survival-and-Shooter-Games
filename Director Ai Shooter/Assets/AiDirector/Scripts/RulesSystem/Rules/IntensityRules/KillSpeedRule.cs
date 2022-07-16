using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.IntensityRules
{
    public class KillSpeedRule : IDirectorIntensityRule
    {
        private readonly int _killsTarget;
        private readonly float _timeToAchieveKills;
        private readonly float _intensity;

        private int _killsCounter;
        private float _clock;

        public KillSpeedRule(int killsTarget, float timeToAchieveKills, float intensity)
        {
            _killsTarget = killsTarget;
            _timeToAchieveKills = timeToAchieveKills;
            _intensity = intensity;

            _killsCounter = killsTarget;
        }
        
        public float CalculatePerceivedIntensity(Director director)
        {
            _clock += 1 * director.GetIntensityCalculationRate();

            if (_clock <= 1.0f)
            {
                _killsCounter = director.GetPlayer().GetKillCount() + _killsTarget;
            }
            
            if (_clock >= _timeToAchieveKills)
            {
                _clock = 0;
            }
            
            if (director.GetPlayer().GetKillCount() >= _killsCounter)
            {
                return _intensity;
            }

            return 0;
        }
    }
}
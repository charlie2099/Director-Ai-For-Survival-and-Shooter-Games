using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.IntensityRules
{
    public class HealthLowRule : IDirectorIntensityRule
    {
        private readonly float _lowHealth;
        private readonly float _intensity;
        
        public HealthLowRule(float lowHealth, float intensity)
        {
            _lowHealth = lowHealth;
            _intensity = intensity;
        }

        public float CalculatePerceivedIntensity(Director director)
        {
            if (director.GetPlayer().GetCurrentHealth() <= _lowHealth)
            {
                return _intensity;
            }
            return 0;
        }
    }
}
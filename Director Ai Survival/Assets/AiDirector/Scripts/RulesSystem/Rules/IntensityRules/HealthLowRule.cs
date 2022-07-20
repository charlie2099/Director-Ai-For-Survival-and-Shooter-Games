using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.IntensityRules
{
    public class HealthLowRule : IDirectorIntensityRule
    {
        private float _lowHealth;
        private float _intensity;
        
        public HealthLowRule(float lowHealth, float intensity)
        {
            _lowHealth = lowHealth;
            _intensity = intensity;
        }

        public float CalculatePerceivedIntensity(Director director)
        {
            if (director.GetPlayer().GetHealth() <= _lowHealth)
            {
                return _intensity;
            }
            return 0;
        }
    }
}
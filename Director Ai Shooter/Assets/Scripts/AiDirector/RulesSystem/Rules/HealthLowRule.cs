using AiDirector.RulesSystem.Interfaces;

namespace AiDirector.RulesSystem.Rules
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
            if (director.GetPlayer().GetCurrentHealth() <= _lowHealth)
            {
                return _intensity;
            }
            return 0;
        }
    }
}
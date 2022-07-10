using AiDirector;
using RulesSystem.Interfaces;

namespace RulesSystem.Rules
{
    public class ResourcesSpentRule : IDirectorIntensityRule
    {
        private float _intensity;
        private int _resourcesGathered;

        public ResourcesSpentRule(int resourcesGathered, float intensity)
        {
            _resourcesGathered = resourcesGathered;
            _intensity = intensity;
        }
        
        public float CalculatePerceivedIntensity(Director director)
        {
            if (director.GetPlayer().GetTotalResourcesGathered() > _resourcesGathered)
            {
                return _intensity;
            }
            return 0;
        }
    }
}
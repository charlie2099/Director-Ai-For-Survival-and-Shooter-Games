using System;
using System.Collections.Generic;
using AiDirector;

namespace Rules
{
    public class DirectorIntensityRuleEngine
    {
        List<IDirectorIntensityRule> _rules = new List<IDirectorIntensityRule>();

        public DirectorIntensityRuleEngine(IEnumerable<IDirectorIntensityRule> rules)
        {
            _rules.AddRange(rules); // Appends items to the end of array
        }

        public float CalculatePerceivedIntensityPercentage(PlayerTemplate player, Director director) 
        {
            float intensity = 0; 
            foreach (var rule in _rules)
            {
                intensity = Math.Max(intensity, rule.CalculatePerceivedIntensity(player, director));
            }
            return intensity;
            // ^ Applies the one which outputs the greatest intensity value
        }
    }
}
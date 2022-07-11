using System.Collections.Generic;
using AiDirector.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.RulesSystem.RuleEngine
{
    public class DirectorIntensityRuleEngine
    {
        List<IDirectorIntensityRule> _rules = new List<IDirectorIntensityRule>();

        public DirectorIntensityRuleEngine(IEnumerable<IDirectorIntensityRule> rules)
        {
            _rules.AddRange(rules); // Appends items to the end of array
        }

        public float CalculatePerceivedIntensityPercentage(Director director) 
        {
            float intensity = 0; 
            foreach (var rule in _rules)
            {
                intensity = Mathf.Max(intensity, rule.CalculatePerceivedIntensity(director));
                // Applies the rule which outputs the greatest intensity value
            }
            return intensity;
        }
    }
}
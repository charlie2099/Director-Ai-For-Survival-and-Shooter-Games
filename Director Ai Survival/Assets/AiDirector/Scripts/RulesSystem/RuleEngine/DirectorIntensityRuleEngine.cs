using System.Collections.Generic;
using AiDirector.Scripts.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.Scripts.RulesSystem.RuleEngine
{
    /*
     * The rule engine for Director Intensity metric
     * Calculates all the rules passed into it by the DirectorIntensityRuleCalculator
     */
    public class DirectorIntensityRuleEngine 
    {
        private readonly List<IDirectorIntensityRule> _rules = new List<IDirectorIntensityRule>();

        public DirectorIntensityRuleEngine(IEnumerable<IDirectorIntensityRule> rules)
        {
            _rules.AddRange(rules); 
        }

        public float CalculatePerceivedIntensityPercentage(Director director) 
        {
            float intensity = 0; 
            foreach (var rule in _rules)
            {
                intensity = Mathf.Max(intensity, rule.CalculatePerceivedIntensity(director));
                // Applies the rule which outputs the greatest intensity weighting
            }
            return intensity;
        }
    }
}
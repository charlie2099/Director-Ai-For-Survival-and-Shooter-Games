using System.Collections.Generic;
using AiDirector.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.RulesSystem.RuleEngine
{
    public class DirectorGameEventRuleEngine
    {
        List<IDirectorGameEventRule> _rules = new List<IDirectorGameEventRule>();

        public DirectorGameEventRuleEngine(IEnumerable<IDirectorGameEventRule> rules)
        {
            _rules.AddRange(rules); // Appends items to the end of array
        }

        public float CalculateGameEventOutput(Director director) 
        {
            float chanceValue = 0; 
            foreach (var rule in _rules)
            {
                chanceValue = Mathf.Max(chanceValue, rule.CalculateGameEvent(director));
                // Applies the rule which outputs the greatest intensity value
            }
            return chanceValue;
            
            /*float intensity = 0; 
            foreach (var rule in _rules)
            {
                intensity = Mathf.Max(intensity, rule.CalculatePerceivedIntensity(director));
                // Applies the rule which outputs the greatest intensity value
            }
            return intensity;*/
        }
    }
}
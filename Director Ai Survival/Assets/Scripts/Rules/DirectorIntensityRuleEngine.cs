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
            _rules.AddRange(rules); // range??
        }

        public float CalcuatePercievedIntensityPercentage(Player player, Director director) // percentage??, Don't pass in director?
        {
            float intensity = 0; // or Director.GetIntensity()
            foreach (var rule in _rules)
            {
                intensity = Math.Max(intensity, rule.CalculatePerceivedIntensity(player, director /*Director.GetIntensity()*/));
            }
            return intensity;
            // ^ Applies the one which outputs the greatest intensity value
        }
    }
}
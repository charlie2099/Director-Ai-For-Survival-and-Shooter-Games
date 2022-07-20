﻿using System.Collections.Generic;
using AiDirector.Scripts.RulesSystem.Interfaces;
using AiDirector.Scripts.RulesSystem.RuleEngine;
using AiDirector.Scripts.RulesSystem.Rules.IntensityRules;

namespace AiDirector.Scripts.RulesSystem.RuleCalculators
{
    /*
     * [Info]
     * Provides an output (intensity) that the Director utilises to monitor the
     * player's performance and dictate the state the Director is in.
     *
     * [Note]
     * After creating a rule, make sure to add it to the rules list in the constructor below
     * so that it is utilised.
     */
    public class DirectorIntensityCalculator 
    {
        private List<IDirectorIntensityRule> _rules;

        public DirectorIntensityCalculator()
        {
            _rules = new List<IDirectorIntensityRule>
            {
                new DistanceFromEnemyRule(3f, 6f), 
                new DistanceFromEnemyRule(6f, 2f), 
                new PlayerIdleRule(5f,3f), 
                new HealthLowRule(50f, 2f), 
                new HealthLowRule(10f, 6f),
                new KillSpeedRule(2, 6f, 5f)
            };
        }
        
        public float CalculatePerceivedIntensityOutput(Director director) 
        {
            var engine = new DirectorIntensityRuleEngine(_rules);
            return engine.CalculatePerceivedIntensityPercentage(director);
            
            // Using the DirectorIntensityRuleEngine to evaluate the rules and produce an output
            // Outputs the greatest intensity value
        }
    }
}
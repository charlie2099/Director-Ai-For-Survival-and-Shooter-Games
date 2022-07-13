using System.Collections.Generic;
using System.Linq;
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
        
        public bool CalculateGameEventOutput(Director director)
        {
            bool firstMatch = false; // rename to allMatchesFound?
            foreach (var rule in _rules)
            {
                firstMatch = _rules.All(ctx => rule.CalculateGameEvent(director));
                // Finds all rules that return true?
            }

            return firstMatch;


            //_rules.All(ctx => true);
            // TODO: Apply the rule which matches first 

            /*float chanceValue = 0; 
            foreach (var rule in _rules)
            {
                chanceValue = Mathf.Max(chanceValue, rule.CalculateGameEvent(director));
            
            }*/
        }
    }
}
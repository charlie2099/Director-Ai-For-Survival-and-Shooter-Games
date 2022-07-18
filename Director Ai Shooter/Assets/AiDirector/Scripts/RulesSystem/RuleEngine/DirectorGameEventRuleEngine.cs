using System.Collections.Generic;
using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.RuleEngine
{
    /*
     * The rule engine for Director Game Events
     * Calculates all the rules passed into it by the DirectorGameEventRuleCalculator
     */
    public class DirectorGameEventRuleEngine
    {
        private readonly List<IDirectorGameEventRule> _rules = new List<IDirectorGameEventRule>();

        public DirectorGameEventRuleEngine(IEnumerable<IDirectorGameEventRule> rules)
        {
            _rules.AddRange(rules); 
        }
        
        public void CalculateGameEventOutput(Director director)
        {
            foreach (var rule in _rules)
            {
                rule.CalculateGameEvent(director);
                // Applies every rule 
            }
        }
    }
}
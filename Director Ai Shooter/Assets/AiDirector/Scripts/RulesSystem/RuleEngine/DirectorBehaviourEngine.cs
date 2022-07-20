using System.Collections.Generic;
using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.RuleEngine
{
    /*
     * The rule engine for Director Game Events
     * Calculates all the rules passed into it by the DirectorGameEventRuleCalculator
     */
    public class DirectorBehaviourEngine
    {
        private readonly List<IDirectorBehaviourRule> _rules = new List<IDirectorBehaviourRule>();

        public DirectorBehaviourEngine(IEnumerable<IDirectorBehaviourRule> rules)
        {
            _rules.AddRange(rules); 
        }
        
        public void CalculateBehaviourOutput(Director director)
        {
            foreach (var rule in _rules)
            {
                rule.CalculateBehaviour(director);
                // Applies every rule 
            }
        }
    }
}
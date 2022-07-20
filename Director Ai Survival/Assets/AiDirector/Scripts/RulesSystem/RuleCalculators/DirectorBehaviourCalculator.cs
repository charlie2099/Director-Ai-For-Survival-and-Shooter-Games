using System;
using System.Collections.Generic;
using System.Linq;
using AiDirector.Scripts.RulesSystem.Interfaces;
using AiDirector.Scripts.RulesSystem.RuleEngine;
using AiDirector.Scripts.RulesSystem.Rules.BehaviourRules;

namespace AiDirector.Scripts.RulesSystem.RuleCalculators
{
    /*
    * [Info]
    * Executes Game Event rules that the Director utilises to determine behaviour
    *
    * [Note]
    * After creating a rule, make sure to add it to the rules list in the constructor below
    * so that it is utilised.
    */
    public class DirectorBehaviourCalculator
    {
        private readonly List<IDirectorBehaviourRule> _rules;

        public DirectorBehaviourCalculator()
        {
            _rules = new List<IDirectorBehaviourRule>
            {
                new ExampleBehaviourRule()
            };
            
            // Using Reflection
            /*var ruleType = typeof(IDirectorBehaviourRule);
            IEnumerable<IDirectorBehaviourRule> rules = GetType().Assembly.GetTypes()
                .Where(p => ruleType.IsAssignableFrom(p) && !p.IsInterface)
                .Select(r => Activator.CreateInstance(r) as IDirectorBehaviourRule);
            _rules.AddRange(rules);*/
        }

        public void CalculateBehaviourOutput(Director director) 
        {
            var engine = new DirectorBehaviourEngine(_rules);
            engine.CalculateBehaviourOutput(director);
        }
    }
}
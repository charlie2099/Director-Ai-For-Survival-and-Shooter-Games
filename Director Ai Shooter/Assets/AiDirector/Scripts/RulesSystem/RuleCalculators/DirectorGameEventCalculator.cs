using System.Collections.Generic;
using AiDirector.Scripts.RulesSystem.Interfaces;
using AiDirector.Scripts.RulesSystem.RuleEngine;
using AiDirector.Scripts.RulesSystem.Rules.GameEventRules;

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
    public class DirectorGameEventCalculator
    {
        private readonly List<IDirectorGameEventRule> _rules;

        public DirectorGameEventCalculator()
        {
            _rules = new List<IDirectorGameEventRule>
            {
                new BossSpawningRule(),
                new MedkitSpawnOnPeakEnd(),
                new AmmoSpawnOnPeakEnd(),
                new KillStreakRule(2, 5),
                new ProgressionRule(2, 3)
            };
            
            // Using Reflection
            /*var ruleType = typeof(IDirectorGameEventRule);
            IEnumerable<IDirectorGameEventRule> rules = GetType().Assembly.GetTypes()
                .Where(p => ruleType.IsAssignableFrom(p) && !p.IsInterface)
                .Select(r => Activator.CreateInstance(r) as IDirectorGameEventRule);
            _rules.AddRange(rules);*/
        }

        public void CalculateGameEventOutput(Director director) 
        {
            var engine = new DirectorGameEventRuleEngine(_rules);
            engine.CalculateGameEventOutput(director);
        }
    }
}
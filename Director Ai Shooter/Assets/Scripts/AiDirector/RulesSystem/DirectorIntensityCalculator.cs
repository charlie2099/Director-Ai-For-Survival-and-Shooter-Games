using System.Collections.Generic;
using AiDirector.RulesSystem.Interfaces;
using AiDirector.RulesSystem.RuleEngine;
using AiDirector.RulesSystem.Rules.IntensityRules;

namespace AiDirector.RulesSystem
{
    /*
     * [Info]
     * Provides an output (intensity) that the Director utilises to determine behaviour
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
            _rules = new List<IDirectorIntensityRule>()
            {
                new DistanceFromEnemyRule(3f, 6f), 
                new DistanceFromEnemyRule(6f, 2f), 
                new PlayerIdleRule(5f,3f), 
                new HealthLowRule(50f, 2f), 
                new HealthLowRule(10f, 6f),
            };

            // [OPTION 2]
            // Using Reflection
            /*var ruleType = typeof(IDirectorIntensityRule);
            IEnumerable<IDirectorIntensityRule> rules = this.GetType().Assembly.GetTypes()
                .Where(p => ruleType.IsAssignableFrom(p) && !p.IsInterface)
                .Select(r => Activator.CreateInstance(r) as IDirectorIntensityRule);

            var engine = new DirectorIntensityRuleEngine(rules);
            return engine.CalculatePerceivedIntensityPercentage(player, director); */
            /*^ [Info]
            Look at all types in current assembly of current type i.e. DirectorIntensityCalculator
            Filter down to just the types that are assignable from ruleType i.e. IDirectorIntensityRule, but not the interface itself
            Uses projection through .Select, which creates an instance of each one of the rules. Though rules should be stateless to use this technique!*/

            /*[OPTION 3]
            A further alternative?
            Using a user interface that allows the designer to add rules to the system without touching this class*/
        }
        
        public float CalculatePerceivedIntensityOutput(Director director) 
        {
            var engine = new DirectorIntensityRuleEngine(_rules);
            return engine.CalculatePerceivedIntensityPercentage(director);
            // Outputs the greatest intensity value
        }
    }
}
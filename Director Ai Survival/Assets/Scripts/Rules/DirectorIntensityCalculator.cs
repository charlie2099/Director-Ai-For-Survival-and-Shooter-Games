using System;
using System.Collections.Generic;
using System.Linq;
using AiDirector;

namespace Rules
{
    public class DirectorIntensityCalculator
    {
        public float CalculatePercievedIntensityPercentage(Player player, Director director) // percentage??, Don't pass in director?
        {
            // [OPTION 1]
            
            /*var rules = new List<IDirectorIntensityRule>();
            //rules.Add(new KillsAtADistanceRule();

            var engine = new DirectorIntensityRuleEngine(rules);
            return engine.CalcuatePercievedIntensityPercentage(player, director);*/
            
            // [OPTION 2]
            
            // Using Reflection
            var ruleType = typeof(IDirectorIntensityRule);
            IEnumerable<IDirectorIntensityRule> rules = this.GetType().Assembly.GetTypes()
                .Where(p => ruleType.IsAssignableFrom(p) && !p.IsInterface)
                .Select(r => Activator.CreateInstance(r) as IDirectorIntensityRule);

            var engine = new DirectorIntensityRuleEngine(rules);
            return engine.CalcuatePercievedIntensityPercentage(player, director);
            // ^
            // Look at all types in current assembly of current type i.e. DirectorIntensityCalculator
            // Filter down to just the types that are assignable from ruleType i.e. IDirectorIntensityRule, but not the interface itself
            // Uses projection through .Select, which creates an instance of each one of the rules. Though rules should be stateless to use this technique!
            
            // [OPTION 3]
            
            // A further alternative?
            // Using a user interface that allows the designer to add rules to the system without touching this class
        }
    }
}
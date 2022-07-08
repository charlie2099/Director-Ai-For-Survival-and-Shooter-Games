using System;
using System.Collections.Generic;
using System.Linq;
using AiDirector;
using UnityEngine;

namespace Rules
{
    /*
     * Provides an output that the Director utilises
     * This goes in the director class? 
     */
    public class DirectorIntensityCalculator : MonoBehaviour
    {
        public static DirectorIntensityCalculator Instance;
        private List<IDirectorIntensityRule> _rules = new List<IDirectorIntensityRule>();
        
        private DirectorIntensityCalculator()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There are multiple instances of DirectorIntensityCalculator active!");
            }
            
            // distance, intensity
            //_rules.Add(new DistanceFromEnemyRule(LessThan,5f, 4f));
            //_rules.Add(new DistanceFromEnemyRule(MoreThan,20f, 6f));
            _rules.Add(new DistanceFromEnemyRule(2f, 10f));  // if enemy less than 5 metres from player, increase intensity by 4
            _rules.Add(new DistanceFromEnemyRule(5f, 5f)); // if enemy less than 20 metres from player, increase intensity by 6; // DOESN'T WORK, PASS IN MORE ARGUEMENTS?
            _rules.Add(new DistanceFromEnemyRule(10f, 2f));
        }
        
        public float CalculatePerceivedIntensityPercentage(PlayerTemplate player, Director director) 
        {
            var engine = new DirectorIntensityRuleEngine(_rules);
            return engine.CalculatePerceivedIntensityPercentage(player, director);
            // Outputs the greatest intensity value?
            
            
            // [OPTION 1]
            /*var rules = new List<IDirectorIntensityRule>();
            rules.Add(new DistanceFromEnemyRule(5f, 4f));
            rules.Add(new DistanceFromEnemyRule(10f, 2f));

            var engine = new DirectorIntensityRuleEngine(rules);
            return engine.CalculatePerceivedIntensityPercentage(player, director);*/
            // Outputs the greatest intensity value?

            // [OPTION 2]
            // Using Reflection
            /*var ruleType = typeof(IDirectorIntensityRule);
            IEnumerable<IDirectorIntensityRule> rules = this.GetType().Assembly.GetTypes()
                .Where(p => ruleType.IsAssignableFrom(p) && !p.IsInterface)
                .Select(r => Activator.CreateInstance(r) as IDirectorIntensityRule);

            var engine = new DirectorIntensityRuleEngine(rules);
            return engine.CalculatePerceivedIntensityPercentage(player, director); */
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
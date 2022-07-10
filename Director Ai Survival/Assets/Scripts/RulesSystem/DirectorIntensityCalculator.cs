using System.Collections.Generic;
using AiDirector;
using RulesSystem.Interfaces;
using RulesSystem.RuleEngine;
using RulesSystem.Rules;
using UnityEngine;

namespace RulesSystem
{
    /*
     * [Info]
     * Provides an output (intensity) that the Director utilises to determine behaviour
     *
     * [Note]
     * After creating a rule, make sure to add it to the rules list in the constructor below
     * so that it is utilised.
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
            
            //_rules.Add(new DistanceFromEnemyRule(LessThan,5f, 4f));
            //_rules.Add(new DistanceFromEnemyRule(MoreThan,20f, 6f));
            
            _rules.Add(new DistanceFromEnemyRule(2f, 10f));  // if enemy less than 2 metres from player,  increase intensity by 10
            _rules.Add(new DistanceFromEnemyRule(5f, 5f));   // if enemy less than 5 metres from player,  increase intensity by 5; 
            _rules.Add(new DistanceFromEnemyRule(10f, 2f));  // if enemy less than 10 metres from player, increase intensity by 2; 
            _rules.Add(new PlayerIdleRule(5,3));
            _rules.Add(new HealthLowRule(50, 2f));
            _rules.Add(new PlayerAggressionRule(5, 2f));
            _rules.Add(new ResourcesSpentRule(30, 4f));
            //_rules.Add(new ConsumableUseFrequencyRule(2, 5f, 2f, 3f));
            
            
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
        
        public float CalculatePerceivedIntensityPercentage(Director director) 
        {
            var engine = new DirectorIntensityRuleEngine(_rules);
            return engine.CalculatePerceivedIntensityPercentage(director);
            // Outputs the greatest intensity value?
        }
    }
}
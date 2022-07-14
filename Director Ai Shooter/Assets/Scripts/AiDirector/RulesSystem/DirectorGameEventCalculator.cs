using System;
using System.Collections.Generic;
using AiDirector.RulesSystem.Interfaces;
using AiDirector.RulesSystem.RuleEngine;
using AiDirector.RulesSystem.Rules.GameEventRules;
using UnityEngine;

namespace AiDirector.RulesSystem
{
    public class DirectorGameEventCalculator : MonoBehaviour
    {
        //public Action OnDirectorEvent;
        public static DirectorGameEventCalculator Instance;
        private List<IDirectorGameEventRule> _rules;

        private DirectorGameEventCalculator()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There are multiple instances of DirectorIntensityCalculator active!");
            }

            _rules = new List<IDirectorGameEventRule>()
            {
                new BossSpawningRule(),
                new MedkitSpawnOnPeakEnd()
                //new RespiteSkipRule()
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
        
        public void CalculateGameEventOutput(Director director) 
        {
            var engine = new DirectorGameEventRuleEngine(_rules);
            engine.CalculateGameEventOutput(director);
            // Outputs all rules that return true? i.e. All rules whose conditions are met
            
            // invoke an event? That is subscribed to by the director?
        }
    }
}
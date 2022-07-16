using System.Collections.Generic;
using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.RuleEngine
{
    public class DirectorGameEventRuleEngine
    {
        List<IDirectorGameEventRule> _rules = new List<IDirectorGameEventRule>();

        public DirectorGameEventRuleEngine(IEnumerable<IDirectorGameEventRule> rules)
        {
            _rules.AddRange(rules); // Appends items to the end of array
        }
        
        public void CalculateGameEventOutput(Director director)
        {
            foreach (var rule in _rules)
            {
                rule.CalculateGameEvent(director);
            }



            /*bool firstMatch = false; // rename to allMatchesFound?
            foreach (var rule in _rules)
            {
                firstMatch = _rules.All(ctx => rule.CalculateGameEvent(director));
                Debug.Log(rule + ": " + rule.CalculateGameEvent(director));

                // .Any() method returns true if at least one of the elements in the source sequence matches the provided predicate. Otherwise, it returns false.
                // .Any() – tell you if any object in the collection meets a criteria (returns true or false)
                
                // .All() method returns true if every element in the source sequence matches the provided predicate. Otherwise, it returns false
            }

            return firstMatch;
            // This method returns true if every rule returns true?*/
        }
    }
}
using System.Collections.Generic;
using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.RuleEngine
{
    public class DirectorStateRuleEngine
    {
        private List<IDirectorStateRule> _rules = new List<IDirectorStateRule>();

        public DirectorStateRuleEngine(IEnumerable<IDirectorStateRule> rules)
        {
            _rules.AddRange(rules); // Appends items to the end of array
        }

        public DirectorState2 CalculateDirectorStateResult(Director director)
        {
            // if perceived intensity > 70
            //     directorState = Peak;

            DirectorState2 directorState2 = 0; 
            foreach (var rule in _rules)
            {
                //_rules.All()
                //directorState2 = (DirectorState2) Mathf.Max((float)directorState2, (float)rule.CalculateDirectorState(director));
                // Applies the rule which outputs the greatest intensity value
            }
            return directorState2;
        }
    }
}
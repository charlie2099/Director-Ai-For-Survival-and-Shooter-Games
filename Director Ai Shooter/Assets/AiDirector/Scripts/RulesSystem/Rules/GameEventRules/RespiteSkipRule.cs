using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.GameEventRules
{
    public class RespiteSkipRule : IDirectorGameEventRule
    {
        public void CalculateGameEvent(Director director)
        {
            if (director.GetPlayer().GetCurrentHealth() <= 50)
            {
                //Debug.Log("Respite skip rule");
                //director.DecreasePerceivedIntensityMetric(50);
                
                //director.TestMethod();
                
                //director.directorState.CurrentTempo = DirectorState.Tempo.Peak;

                // returns true if condition is met, and then the director compares it between all other events 
                // that have also returned true and picks one to execute?

                // OR

                // returns a randomly generated value and whichever event rule outputs the highest generated value 
                // is the event that is executed? 

                // Events are executed every x seconds and/or at the beginning of the peak phase.
            }
        }
    }
}
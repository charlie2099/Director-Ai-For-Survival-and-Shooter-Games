using AiDirector.RulesSystem.Interfaces;

namespace AiDirector.RulesSystem.Rules.GameEventRules
{
    public class BossSpawningRule : IDirectorGameEventRule
    {
        public float CalculateGameEvent(Director director)
        {
            if (director.GetPlayer().GetCurrentHealth() == director.GetPlayer().GetMaxHealth() &&
                director.GetPlayer().GetWeapon().GetCurrentAmmo() == director.GetPlayer().GetWeapon().GetMagCapacity() &&
                director.GetPerceivedIntensity() > 50)
            {
                //director.SpawnBoss();

                return director.GetPerceivedIntensity();
                
                // returns true if condition is met, and then the director compares it between all other events 
                // that have also returned true and picks one to execute?
                
                // OR
                
                // returns a randomly generated value and whichever event rule outputs the highest generated value 
                // is the event that is executed? 
                
                // Events are executed every x seconds and/or at the beginning of the peak phase.
            }

            return 0;
        }
    }
}
using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.BehaviourRules
{
    public class BossSpawningRule : IDirectorBehaviourRule
    {
        public void CalculateBehaviour(Director director)
        {
            if(director.GetPlayer().GetCurrentHealth() >= director.GetPlayer().GetMaxHealth() && 
               director.GetDirectorState().CurrentTempo == DirectorState.Tempo.Peak)
            {
                director.maxPopulationCount += 4;
                director.SpawnBoss();
            }
        }
    }
}
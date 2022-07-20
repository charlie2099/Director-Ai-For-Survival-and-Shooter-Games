using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.BehaviourRules
{
    public class MedkitSpawnOnPeakEnd : IDirectorBehaviourRule
    {
        public void CalculateBehaviour(Director director)
        {
            if (director.GetPlayer().GetCurrentHealth() <= 50 &&
                director.GetDirectorState().CurrentTempo == DirectorState.Tempo.Respite)
            {
                director.SpawnItem("Medkit");
            }
        }
    }
}
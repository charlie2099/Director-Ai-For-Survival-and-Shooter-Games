using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.BehaviourRules
{
    public class AmmoSpawnOnPeakEnd : IDirectorBehaviourRule
    {
        public void CalculateBehaviour(Director director)
        {
            if (director.GetPlayer().GetWeapon().GetCurrentAmmo() <= 5 &&
                director.GetDirectorState().CurrentTempo == DirectorState.Tempo.Respite)
            {
                director.SpawnItem("AmmoCrate");
            }
        }
    }
}
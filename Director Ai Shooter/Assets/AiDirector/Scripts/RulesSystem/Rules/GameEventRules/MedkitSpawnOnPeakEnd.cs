using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.GameEventRules
{
    public class MedkitSpawnOnPeakEnd : IDirectorGameEventRule
    {
        public void CalculateGameEvent(Director director)
        {
            if (director.GetPlayer().GetCurrentHealth() <= 50 &&
                director.GetDirectorState().CurrentTempo == DirectorState.Tempo.Respite)
            {
                director.SpawnItem("Medkit");
            }
        }
    }
}
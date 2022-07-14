using AiDirector.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.RulesSystem.Rules.GameEventRules
{
    public class MedkitSpawnOnPeakEnd : IDirectorGameEventRule
    {
        public void CalculateGameEvent(Director director)
        {
            if (director.GetPlayer().GetCurrentHealth() <= 50 &&
                director.directorState.CurrentTempo == DirectorState.Tempo.Respite)
            {
                director.SpawnItem("Medkit", "Medkits");
            }
        }
    }
}
using AiDirector.RulesSystem.Interfaces;

namespace AiDirector.RulesSystem.Rules.GameEventRules
{
    public class KillStreakRule : IDirectorGameEventRule
    {
        public void CalculateGameEvent(Director director)
        {
            if(director.GetPlayer().GetKillCount() >= 5  && director.directorState.CurrentTempo == DirectorState.Tempo.Peak)
            {
                director.maxPopulationCount = 10;
                director.SpawnBoss();
            }
        }
    }
}
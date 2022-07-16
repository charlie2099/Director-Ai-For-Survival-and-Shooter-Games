using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.GameEventRules
{
    public class KillStreakRule : IDirectorGameEventRule
    {
        private readonly int _killsToGet;
        private readonly int _enemiesToSpawn;
        
        public KillStreakRule(int killsToGet, int enemiesToSpawn)
        {
            _killsToGet = killsToGet;
            _enemiesToSpawn = enemiesToSpawn;
        }

        public void CalculateGameEvent(Director director)
        {
            if(director.GetPlayer().GetKillCount() >= _killsToGet  && director.directorState.CurrentTempo == DirectorState.Tempo.Peak)
            {
                director.maxPopulationCount += _enemiesToSpawn;
                director.SpawnBoss();
            }
        }
    }
}
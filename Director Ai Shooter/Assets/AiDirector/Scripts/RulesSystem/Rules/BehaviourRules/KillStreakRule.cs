using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.BehaviourRules
{
    public class KillStreakRule : IDirectorBehaviourRule
    {
        private readonly int _killsToGet;
        private readonly int _enemiesToSpawn;
        
        public KillStreakRule(int killsToGet, int enemiesToSpawn)
        {
            _killsToGet = killsToGet;
            _enemiesToSpawn = enemiesToSpawn;
        }

        public void CalculateBehaviour(Director director)
        {
            if(director.GetPlayer().GetKillCount() >= _killsToGet  && director.GetDirectorState().CurrentTempo == DirectorState.Tempo.Peak)
            {
                director.maxPopulationCount += _enemiesToSpawn;
                director.SpawnBoss();
            }
        }
    }
}
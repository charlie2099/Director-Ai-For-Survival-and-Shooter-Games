using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.BehaviourRules
{
    public class ProgressionRule : IDirectorBehaviourRule
    {
        private readonly int _generatorsOnline;
        private readonly int _enemiesToSpawn;
        
        public ProgressionRule(int generatorsOnline, int enemiesToSpawn)
        {
            _generatorsOnline = generatorsOnline;
            _enemiesToSpawn = enemiesToSpawn;
        }
        
        public void CalculateBehaviour(Director director)
        {
            director.maxPopulationCount += Generator.GeneratorsOnline * _enemiesToSpawn;
            
            if (Generator.GeneratorsOnline >= _generatorsOnline)
            {
                director.SpawnBoss();
            }
        }
    }
}
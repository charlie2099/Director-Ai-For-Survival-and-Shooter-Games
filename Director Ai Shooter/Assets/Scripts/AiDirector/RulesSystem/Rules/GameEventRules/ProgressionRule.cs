using AiDirector.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.RulesSystem.Rules.GameEventRules
{
    public class ProgressionRule : IDirectorGameEventRule
    {
        private readonly int _generatorsOnline;
        private readonly int _enemiesToSpawn;
        
        public ProgressionRule(int generatorsOnline, int enemiesToSpawn)
        {
            _generatorsOnline = generatorsOnline;
            _enemiesToSpawn = enemiesToSpawn;
        }
        
        public void CalculateGameEvent(Director director)
        {
            director.maxPopulationCount += Generator.GeneratorsOnline * _enemiesToSpawn;
            
            if (Generator.GeneratorsOnline >= _generatorsOnline)
            {
                director.SpawnBoss();
            }
        }
    }
}
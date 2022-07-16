﻿using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.GameEventRules
{
    public class BossSpawningRule : IDirectorGameEventRule
    {
        public void CalculateGameEvent(Director director)
        {
            /*if (director.GetPlayer().GetCurrentHealth() == director.GetPlayer().GetMaxHealth() &&
                director.GetPlayer().GetWeapon().GetCurrentAmmo() == director.GetPlayer().GetWeapon().GetMagCapacity() &&
                director.GetPerceivedIntensity() > 50)*/

            if(director.GetPlayer().GetCurrentHealth() >= director.GetPlayer().GetMaxHealth() && director.GetDirectorState().CurrentTempo == DirectorState.Tempo.Peak)
            {
                director.maxPopulationCount += 4;
                director.SpawnBoss();
            }
        }
    }
}
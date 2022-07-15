﻿using AiDirector.RulesSystem.Interfaces;

namespace AiDirector.RulesSystem.Rules.GameEventRules
{
    public class AmmoSpawnOnPeakEnd : IDirectorGameEventRule
    {
        public void CalculateGameEvent(Director director)
        {
            if (director.GetPlayer().GetWeapon().GetCurrentAmmo() <= 5 &&
                director.directorState.CurrentTempo == DirectorState.Tempo.Respite)
            {
                director.SpawnItem("AmmoCrate");
            }
        }
    }
}
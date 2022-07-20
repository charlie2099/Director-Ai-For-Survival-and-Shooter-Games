using AiDirector.Scripts.RulesSystem.Interfaces;
using Items;
using UnityEngine;

namespace AiDirector.Scripts.RulesSystem.Rules.IntensityRules
{
    public class PlayerAggressionRule : IDirectorIntensityRule
    {
        private float _clock;
        private float _timePassed;
        private float _intensity;

        public PlayerAggressionRule(float timePassed, float intensity)
        {
            _timePassed = timePassed;
            _intensity = intensity;
        }

        private bool PlayerHoldingMusketForSomeTime(Director director)
        {
            _clock += 1 * director.GetIntensityCalculationRate();

            Debug.Log("ItemTypeInHand: " + director.GetPlayer().GetItemTypeInHand());

            if (director.GetPlayer().GetItemTypeInHand() == ItemType.Type.MUSKET && _clock >= _timePassed)
            {
                return true;
            }

            if (_clock >= _timePassed)
            {
                _clock = 0;
            }
            return false;
        }
        
        public float CalculatePerceivedIntensity(Director director)
        {
            if (PlayerHoldingMusketForSomeTime(director))
            {
                return _intensity;
            }
            return 0;
        }
    }
}
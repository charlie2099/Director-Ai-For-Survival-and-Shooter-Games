using AiDirector;
using RulesSystem.Interfaces;
using UnityEngine;

namespace RulesSystem.Rules
{
    public class ConsumableUseFrequencyRule : IDirectorIntensityRule
    {
        private float _clock;
        private float _clock2;
        private float _intensity;
        private float _timePassed;
        private float _intensityDuration;
        private int _consumablesUsed;

        public ConsumableUseFrequencyRule(int consumablesUsed, float timePassed, float intensityDuration, float intensity)
        {
            _consumablesUsed = consumablesUsed;
            _timePassed = timePassed;
            _intensityDuration = intensityDuration;
            _intensity = intensity;
        }

        private bool PlayerFrequentlyConsumingItems(Director director)
        {
            _clock += 1 * director.GetIntensityCalculationRate();

            if (director.GetPlayer().GetConsumablesUsed() >= _consumablesUsed && _clock >= _timePassed)
            {
                _clock2 += 1 * director.GetIntensityCalculationRate();

                if (_clock2 >= _intensityDuration)
                {
                    //_consumablesUsed += _consumablesUsed;
                    return false;
                }
                return true;
            }

            /*if (_clock >= _timePassed)
            {
                _clock = 0;
            }*/
            return false;
        }

        public float CalculatePerceivedIntensity(Director director)
        {
            if (PlayerFrequentlyConsumingItems(director))
            {
                return _intensity;
            }
            return 0;
        }
    }
}
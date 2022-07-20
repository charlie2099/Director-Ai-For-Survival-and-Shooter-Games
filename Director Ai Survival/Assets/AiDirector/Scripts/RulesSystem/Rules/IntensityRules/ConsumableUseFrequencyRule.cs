using AiDirector.Scripts.RulesSystem.Interfaces;

namespace AiDirector.Scripts.RulesSystem.Rules.IntensityRules
{
    public class ConsumableUseFrequencyRule : IDirectorIntensityRule
    {
        private float _clock;
        private float _intensity;
        private float _timePassed;
        private float _intensityDuration;
        private int _consumablesUsed;
        private bool _active;

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

            if (director.GetPlayer().GetConsumablesUsed() >= _consumablesUsed && _clock >= _timePassed && !_active)
            {
                //Debug.Log("<color=green>Intensity duration started</color>");
                _timePassed = _clock + _intensityDuration;
                _active = true;
            }

            if (_active)
            {
                if (_clock >= _timePassed)
                {
                    //Debug.Log("<color=orange>Intensity duration ended</color>");
                    _consumablesUsed += _consumablesUsed;
                    _active = false;
                }
                return true;
            }
            
            if (_clock >= _timePassed && !_active)
            {
                //Debug.Log("<color=red>Rule was not satisfied</color>");
                _clock = 0;
            }
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
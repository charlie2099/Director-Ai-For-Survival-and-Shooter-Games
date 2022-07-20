using AiDirector.Scripts.RulesSystem.Interfaces;
using UnityEngine;

namespace AiDirector.Scripts.RulesSystem.Rules.IntensityRules
{
    /*
     * Define your Intensity Rules like this
     * Each Intensity rule returns an intensity value
     */
    public class ExampleIntensityRule : IDirectorIntensityRule
    {
        public float CalculatePerceivedIntensity(Director director)
        {
            return 0;
        }
    }
}

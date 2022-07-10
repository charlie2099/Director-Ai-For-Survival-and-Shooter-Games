using AiDirector;

namespace RulesSystem.Interfaces
{
    /*
     * Interface that all rules inherit from
     */
    public interface IDirectorIntensityRule
    {
        public float CalculatePerceivedIntensity(Director director);
    }
}
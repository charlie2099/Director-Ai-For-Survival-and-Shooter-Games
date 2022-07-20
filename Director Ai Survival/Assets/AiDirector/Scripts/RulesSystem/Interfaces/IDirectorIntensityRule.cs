namespace AiDirector.Scripts.RulesSystem.Interfaces
{
    /*
     * The interface that all Intensity Rules should
     * inherit from
     */
    public interface IDirectorIntensityRule
    {
        public float CalculatePerceivedIntensity(Director director);
    }
}
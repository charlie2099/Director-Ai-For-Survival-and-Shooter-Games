using AiDirector;

namespace Rules
{
    /*
     * Interface that all rules inherit from
     */
    public interface IDirectorIntensityRule
    {
        public float CalculatePerceivedIntensity(PlayerTemplate player, Director director);
    }
}
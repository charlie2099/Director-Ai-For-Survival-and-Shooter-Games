using AiDirector;

namespace Rules
{
    /*
     * Interface that rules follow
     */
    public interface IDirectorIntensityRule
    {
        public float CalculatePerceivedIntensity(Player player, Director director);
    }
}
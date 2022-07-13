namespace AiDirector.RulesSystem.Interfaces
{
    public interface IDirectorIntensityRule
    {
        public float CalculatePerceivedIntensity(Director director);
    }
}
namespace AiDirector.Scripts.RulesSystem.Interfaces
{
    public enum DirectorState2 { BuildUp, Peak, PeakFade, Respite }

    public interface IDirectorStateRule
    {
        public DirectorState2 CalculateDirectorState(Director director);
    }
}
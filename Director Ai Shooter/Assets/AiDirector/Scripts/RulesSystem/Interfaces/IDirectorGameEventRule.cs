namespace AiDirector.Scripts.RulesSystem.Interfaces
{
    /*
     * The interface that all Game Event Rules should
     * inherit from
     */
    public interface IDirectorGameEventRule
    {
        public void CalculateGameEvent(Director director);
    }
}
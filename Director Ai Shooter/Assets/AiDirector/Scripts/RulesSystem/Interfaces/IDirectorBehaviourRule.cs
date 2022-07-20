namespace AiDirector.Scripts.RulesSystem.Interfaces
{
    /*
     * The interface that all Behaviour Rules should
     * inherit from
     */
    public interface IDirectorBehaviourRule
    {
        public void CalculateBehaviour(Director director);
    }
}
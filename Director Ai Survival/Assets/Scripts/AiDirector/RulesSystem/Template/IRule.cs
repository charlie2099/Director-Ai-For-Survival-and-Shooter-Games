namespace AiDirector.RulesSystem.Template
{
    /*
     * Interface that rules follow
     */
    public interface IRule
    {
        bool IsApplicable();
        void Execute();
    }
}
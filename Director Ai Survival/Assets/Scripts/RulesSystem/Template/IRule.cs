namespace Rules
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
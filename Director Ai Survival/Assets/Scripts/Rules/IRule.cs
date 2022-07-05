namespace Rules
{
    public interface IRule
    {
        bool IsApplicable();
        void Execute();
    }
}
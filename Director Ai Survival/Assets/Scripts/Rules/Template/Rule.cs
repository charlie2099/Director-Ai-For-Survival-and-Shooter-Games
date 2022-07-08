namespace Rules
{
    /*
     * Here is how a designer will come and define their own rules for the director system
     * By creating a new rule class that inherits the IRule interface, new rules can be made
     * and processed by the rule evaluator.
     */
    
    public class Rule : IRule
    {
        public bool IsApplicable()
        {
            return true;
        }

        public void Execute()
        {
        
        }
    }
}

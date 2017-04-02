using System;
using System.Activities;

namespace CasFiniteStateSample
{

    public class CasRule : CodeActivity
    {
        // Define an activity input argument of type string
        public int RuleNumber { get; set; }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (RuleNumber < 0)
                metadata.AddValidationError("Rule number must not be negative");
            if (RuleNumber == 0)
                metadata.AddValidationError("Pease enter a rule number");
            base.CacheMetadata(metadata);
        }

        protected override void Execute(CodeActivityContext context)
        {
            Console.WriteLine("Using rule R{0:n0}", RuleNumber);
        }

        public override string ToString()
        {
            return string.Format("On rule R{0}", RuleNumber);
        }
    }
}
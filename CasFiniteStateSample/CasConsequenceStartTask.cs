using System;
using System.Activities;

namespace CasFiniteStateSample
{
    public class CasConsequenceStartTask : CodeActivity
    {
        public InArgument<string> TaskId { get; set; }
        public int RuleID { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string tid = context.GetValue(TaskId);
            Console.WriteLine("{2} : {0}, if R{1} is true", tid, RuleID, DisplayName);
        }
    }

        
}

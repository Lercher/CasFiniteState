using System;
using System.Activities;

namespace CasFiniteStateSample
{
    public class CasStartTask : CodeActivity
    {
        public InArgument<string> TaskId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string tid = context.GetValue(TaskId);
            Console.WriteLine("Activate Task {0}", tid);
        }
    }

        
}

using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace CasFiniteStateSample
{

    class Program
    {
        static void Main(string[] args)
        {
            Activity wf = new SampleWorkflow();
            WorkflowInvoker.Invoke(wf);
            Console.WriteLine();
            Console.Write("Press return to exit ... ");
            Console.ReadLine();
        }
    }
}

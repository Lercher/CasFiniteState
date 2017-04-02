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
            var app = new WorkflowApplication(wf);
            app.Run();
            while (true)
            {
                System.Threading.Thread.Sleep(250);
                try
                {
                    var bms = app.GetBookmarks();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("[");
                    foreach (var bm in bms)
                    {
                        Console.Write(" {0} ", bm.BookmarkName);
                    }
                    Console.Write("] ");
                }
                catch (Exception ex)
                {
                    Console.Write("{0}. Press return to quit ... ", ex.Message);
                    Console.ReadLine();
                    return;
                }
                Console.Write("Enter command q-quit, e-trigger event: ");
                var l = Console.ReadLine();
                switch(l.Trim().ToLowerInvariant())
                {
                    case "q":
                        app.Abort();
                        return;
                    case "e":
                        app.ResumeBookmark("TriggerEvent", null);
                        break;
                }

            }
        }
    }
}

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

            // http://blogs.msmvps.com/theproblemsolver/2011/01/07/doing-synchronous-workflow-execution-using-the-workflowapplication/
            // app.SynchronizationContext = new SynchronousSynchronizationContext();

            app.Run();
            while (true)
            {
                System.Threading.Thread.Sleep(100);
                try
                {
                    var bms = app.GetBookmarks();
                    Console.WriteLine();
                    Console.WriteLine();
                    var n = 0;
                    foreach (var bm in bms)
                    {
                        n++;
                        Console.Write("[{1} {0}] ", bm.BookmarkName, n);
                    }
                    Console.Write("q-quit, 1-5. Enter command: ");
                    var l = Console.ReadLine();
                    switch (l.Trim().ToLowerInvariant())
                    {
                        case "q":
                            app.Abort();
                            return;
                        case "1":
                            app.ResumeBookmark(bms[0].BookmarkName, null);
                            break;
                        case "2":
                            app.ResumeBookmark(bms[1].BookmarkName, null);
                            break;
                        case "3":
                            app.ResumeBookmark(bms[2].BookmarkName, null);
                            break;
                        case "4":
                            app.ResumeBookmark(bms[3].BookmarkName, null);
                            break;
                        case "5":
                            app.ResumeBookmark(bms[4].BookmarkName, null);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    app.Abort();
                    Console.Write("{0}. Press return to quit ... ", ex.Message);
                    Console.ReadLine();
                    return;
                }
            }
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Activities.XamlIntegration; // ActivityXamlServices

namespace CasFiniteStateSample
{
    class Program
    {

        public static Activity LoadActivityFrom(FileInfo xaml)
        {
            // see http://stackoverflow.com/questions/6098077/rehosted-workflow-designer-default-imported-namespaces
            using (var rs = xaml.OpenRead())
            using (var xr = new System.Xaml.XamlXmlReader(rs))
            using (var br = ActivityXamlServices.CreateBuilderReader(xr))
            {
                var ab = System.Xaml.XamlServices.Load(br) as ActivityBuilder;
                return ab.Implementation;
            }
            /*
            * don't just return ActivityXamlServices.Load(rs) because, if you do and you .Load() this Activity to a WorkflowDesigner instance wd
            * you will get a dialog that states a null reference exception in System.Activities.Presentation.View.ImportDesigner.OnContextChanged()
            * and this is because wd.Context.Services.GetService<System.Activities.Presentation.Services.ModelService>().Root.Properties["Imports"] is null in 
            * https://referencesource.microsoft.com/#System.Activities.Presentation/System.Activities.Presentation/System/Activities/Presentation/View/ImportDesigner.xaml.cs,472d4082845d64b6,references
            * void OnContextChanged() at
            * this.importsModelItem = modelService.Root.Properties[NamespaceListPropertyDescriptor.ImportCollectionPropertyName].Collection
            * N.B.: NamespaceListPropertyDescriptor.ImportCollectionPropertyName is "Imports"
            */
        }

        static void Main(string[] args)
        {
            var pos = (StateMachine) LoadActivityFrom(new FileInfo(@"\daten\github\casfinitestate\casfinitestatesample\poslikeworkflow.xaml"));
            using (var fs = File.OpenWrite("saved.xaml"))
                System.Xaml.XamlServices.Save(fs, pos);

            var wf = new SampleWorkflow();
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
                        Console.WriteLine("[{1} {0}] ", bm.BookmarkName, n);
                    }
                    Console.Write("Enter 1..{0} or q-quit: ", bms.Count);
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

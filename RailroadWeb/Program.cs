using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RailroadWeb.Computation;
using RailroadWeb.Entities;
using RailroadWeb.Validation;

namespace RailroadWeb
{
    class Program
    {
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();

        static void Main(string[] args)
        {
            
            var pathToWeb = ConfigurationManager.AppSettings["PathToWeb"];
            var pathForRoutes = ConfigurationManager.AppSettings["DirectoryForRoutes"];
            var filesExtension = ConfigurationManager.AppSettings["FileExtension"];
          
            if (!ValidationService.ValidateConfiguration(pathForRoutes, pathToWeb, filesExtension, out var  validationMessage))
            {
                Console.WriteLine(validationMessage);
                Console.ReadLine();
                return;
            }

            var routeFiles = Directory.GetFiles(pathForRoutes,$"*{filesExtension}");

            var web =EntityReader.GetEntity<Web>(pathToWeb).Result;
            
            var solver = new CrashComputationService(web);
            
            var response = Enum.ComputationResponse.InvalidInputData;

            var tasks = new List<Task<Enum.ComputationResponse>>();
            var parent = Task.Run(() =>
              {
                  TaskFactory tf = new TaskFactory(s_cts.Token,
                                                   TaskCreationOptions.AttachedToParent,
                                                   TaskContinuationOptions.ExecuteSynchronously,
                                                   TaskScheduler.Default);

                  foreach (var routeFile in routeFiles)
                  {
                      tasks.Add(tf.StartNew(() => EntityReader.GetEntity<Route>(routeFile).Result)
                                  .ContinueWith((r) => solver.ComputeForRoute(r.Result)));
                      
                  }                
              });

            try
            {
                parent.Wait(s_cts.Token);
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            while (tasks.Any())
            {
                var finishedTask = Task.WhenAny(tasks).Result;

                response = finishedTask.Result;

                if (response != Enum.ComputationResponse.Success)
                {
                    tasks.Clear();
                    //s_cts.Cancel();
                }
                else
                {
                    tasks.Remove(finishedTask);
                }                
            }
            
            Console.WriteLine(response.ToString());
            Console.ReadLine();
        }
    }
}

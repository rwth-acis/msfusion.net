using System;
using System.Collections.Generic;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using numl.Model;
using numl.Supervised.DecisionTree;
using ContestManager.Modules;
using ContestManager.Modules.HAR;
using ContestManager.Modules.HGR;
using ARLEMDecipher;
using System.Linq;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Classifiers.Trees;

namespace ContestManager
{
    

    class Program
    {

        // var randomForestClassifier;
        // var logisticRegressionClassifier;
        // var adaBoostClassifier;

        static void TrainModules()
        {
            //var WISDMO = new WISDM("/i5/mobileMotion/accelerometer");
            //WISDMO.Train();

            var MyoGymO = new MyoGym("/i5/myo/accelerometer", "/i5/myo/gyroscope", "/i5/myo/emg");
            MyoGymO.TrainFromFeatures();

        }


        static void Main(string[] args)
        {
            // Step One Parse ARLEM document into object
            Console.WriteLine("Choose your workplace");
            int WorkplaceId = int.Parse(Console.ReadLine());

            ARLEMDecipher.ARLEMDecipher aRLEMDecipher = new ARLEMDecipher.ARLEMDecipher("http://127.0.0.1:8080");
            if (!aRLEMDecipher.LoadWorkplaceJSON(WorkplaceId))
            {
                Console.ReadKey();
                return;
            }
            int[] availableActivities = aRLEMDecipher.AvailableActivites();
            if (availableActivities.Length < 0)
            {
                Console.ReadKey();
                return;
            }
 
            Console.WriteLine("Please Select an activity");
            aRLEMDecipher.Workplace.Activities.ForEach(activity => {
                Console.WriteLine(aRLEMDecipher.Workplace.Activities.IndexOf(activity) + ": " + activity.Name);
            });
            int ActivityId = int.Parse(Console.ReadLine());
            aRLEMDecipher.LoadActivityJSON(availableActivities[ActivityId]);
            List<string> Sensors = aRLEMDecipher.AvailableSensors();

            List<String> Modules = aRLEMDecipher.RequiredModules();
            List<IModule> ModuleToExecute = new List<IModule>();

            Dictionary<string, IModule> ModuleDictionary = new Dictionary<string, IModule>();

            Modules.ForEach(moduleName =>
            {
                IModule BestFitModule = null;
                IModule.ModuleMap[moduleName].ForEach(module =>
                {
                    if(module.IsCalculatable(Sensors))
                    {
                        BestFitModule = module;
                    }
                });
                if(BestFitModule == null)
                {
                    Console.WriteLine(moduleName + " can't be calcualted with available sensors");
                }
                else
                {
                    ModuleDictionary[moduleName] = BestFitModule;
                    ModuleToExecute.Add(BestFitModule);
                }
            });


            Console.WriteLine("Activity requirs following sensors. Please connect your sensors and press any key to continue.");
            ModuleToExecute.ForEach(module =>
            {
                module.RequiredSensors.ForEach(urn => Console.WriteLine(urn));
            });

            List<ExportedAction> ExportedActions = aRLEMDecipher.GetActivityActions();
            if(ExportedActions.Count <= 0)
            {
                Console.WriteLine("Activity does not have any actions that can be used to demonstrate this framework. So terminating the program.");
                Console.ReadKey();
                return;
            }
            ExportedAction CurrentAction = ExportedActions[0];
            Console.ReadKey();

            foreach(var d in ModuleDictionary)
             {
                d.Value.Config((dynamic output) =>
                {
                    // ModuleDictionary[CurrentAction.Module].DecisionToMessage((int)output);
                    // Console.WriteLine(Array.Find(CurrentAction.ComparedValue, (x => x == output)));
                    if (Array.Find(CurrentAction.ComparedValue, (x => x == output)) > 0)
                    {
                        bool activityFinished = true;
                        ModuleDictionary[CurrentAction.Module].Stop();
                        foreach (var exportedAction in ExportedActions)
                        {
                            if (exportedAction.Id == CurrentAction.NextAction)
                            {
                                ModuleDictionary[CurrentAction.Module].Stop();
                                CurrentAction = exportedAction;
                                Console.WriteLine(CurrentAction.Instruction);
                                ModuleDictionary[CurrentAction.Module].Start();
                                activityFinished = false;
                                break;
                            }
                        }
                        if (activityFinished)
                        {
                            Console.WriteLine("Activity Finished");
                        }
                    }
                });
            }

            Console.WriteLine(CurrentAction.Instruction);
            ModuleDictionary[CurrentAction.Module].Start();

        }
   
    }
}

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
using FusionFramework.Features;
using FusionFramework.Features.Complex;
using FusionFramework.Features.TimeDomain;
using FusionFramework.Data.Segmentators;
using FusionFramework.Classifiers.VectorMachines;
using FusionFramework.Fusion.Strategies;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using System.Text;

namespace ContextManager
{
    class ResultantAcceleration : FusionFramework.Features.IFeature
    {
        public ResultantAcceleration()
        {
            Flavour = FeatureFlavour.MatrixInValueOut;
        }

        public override dynamic Calculate(dynamic data)
        {
            double[][] Array = data;
            return new Mean().Calculate(Array.Select(x => (double)new Magnitude().Calculate(x)).ToArray<double>());
        }
    }

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
        static void MyoGym_no_acc()
        {
            FeatureManager featureManager = new FeatureManager();
            var Classifier = new MulticlassSupportVectorMachineClassifier();
            Classifier.Load("C:\\Users\\riz\\Desktop\\MyoGYm\\MyoGym_NOEMG_SVM");
            var dataReader = new MQTTReader<double[]>("/i5/myo/full", (dynamic output) =>
            {
                var FeaturSpace = FeatureManager.Generate(output, new List<IFeature>()
                    {
                        new HjorthParameters(0,1,2),
                        new StandardDeviation(0,1,2),
                        new Mean(0,1,2),
                        new Max(0,1,2),
                        new Min(0,1,2),
                        new Percentile(5,  0,1,2),
                        new Percentile(10, 0,1,2),
                        new Percentile(25, 0,1,2),
                        new Percentile(50, 0,1,2),
                        new Percentile(75, 0,1,2),
                        new Percentile(90, 0,1,2),
                        new Percentile(95, 0,1,2),
                        new ZeroCrossing(0,1,2),
                        new MeanCrossing(0,1,2),
                        new Entropy(0,1,2),
                        new Correlation(0, 1),
                        new Correlation(0, 2),
                        new Correlation(1, 2),

                        new HjorthParameters(3,4,5),
                        new StandardDeviation(3,4,5),
                        new Mean(3,4,5),
                        new Max(3,4,5),
                        new Min(3,4,5),
                        new Percentile(5,  3,4,5),
                        new Percentile(10, 3,4,5),
                        new Percentile(25, 3,4,5),
                        new Percentile(50, 3,4,5),
                        new Percentile(75, 3,4,5),
                        new Percentile(90, 3,4,5),
                        new Percentile(95, 3,4,5),
                        new ZeroCrossing(3,4,5),
                        new MeanCrossing(3,4,5),
                        new Entropy(3,4,5)

                    });

                Console.WriteLine(Classifier.Classify(FeaturSpace));

            });
            dataReader.Add(new SlidingWindow<double[]>(200, 0));
            dataReader.Start();


        }

        static void MyoGym()
        {
            FeatureManager featureManager = new FeatureManager();
            var Classifier = new MulticlassSupportVectorMachineClassifier();
            Classifier.Load("C:\\Users\\riz\\Desktop\\MyoGYm\\MyoGym_SVM");
            var dataReader = new MQTTReader<double[]>("/i5/myo/full", (dynamic output) =>
            {
                var FeaturSpace = FeatureManager.Generate(output, new List<IFeature>()
                    {
                        new HjorthParameters(8,9,10),
                        new StandardDeviation(8,9,10),
                        new Mean(8,9,10),
                        new Max(8,9,10),
                        new Min(8,9,10),
                        new Percentile(5,  8,9,10),
                        new Percentile(10, 8,9,10),
                        new Percentile(25, 8,9,10),
                        new Percentile(50, 8,9,10),
                        new Percentile(75, 8,9,10),
                        new Percentile(90, 8,9,10),
                        new Percentile(95, 8,9,10),
                        new ZeroCrossing(8,9,10),
                        new MeanCrossing(8,9,10),
                        new Entropy(8, 9, 10),
                        new Correlation(9, 10),
                        new Correlation(9, 11),
                        new Correlation(10, 11),

                        new HjorthParameters(11,12,13),
                        new StandardDeviation(11,12,13),
                        new Mean(11,12,13),
                        new Max(11,12,13),
                        new Min(11,12,13),
                        new Percentile(5,  11,12,13),
                        new Percentile(10, 11,12,13),
                        new Percentile(25, 11,12,13),
                        new Percentile(50, 11,12,13),
                        new Percentile(75, 11,12,13),
                        new Percentile(90, 11,12,13),
                        new Percentile(95, 11,12,13),
                        new ZeroCrossing(11,12,13),
                        new MeanCrossing(11,12,13),
                        new Entropy(11,12,13),

                        new StandardDeviation(0,1,2,3,4,5,6,7),
                        new Mean(0,1,2,3,4,5,6,7 ),
                        new Max(0,1,2,3,4,5,6,7 ),
                        new Min(0,1,2,3,4,5,6,7 ),
                        new Percentile(5, 0,1,2,3,4,5,6,7 ),
                        new Percentile(10, 0,1,2,3,4,5,6,7 ),
                        new Percentile(25,  0,1,2,3,4,5,6,7 ),
                        new Percentile(50, 0,1,2,3,4,5,6,7 ),
                        new Percentile(75, 0,1,2,3,4,5,6,7 ),
                        new Percentile(90, 0,1,2,3,4,5,6,7 ),
                        new Percentile(95, 0,1,2,3,4,5,6,7 ),

                        new SumLargerThan(25, 0,1,2,3,4,5,6,7 ),
                        new SumLargerThan(50, 0,1,2,3,4,5,6,7 ),
                        new SumLargerThan(100, 0,1,2,3,4,5,6,7 )

                    });

                Console.WriteLine("HGR(" + Classifier.Classify(FeaturSpace) + ")");

            });
            dataReader.Add(new SlidingWindow<double[]>(200, 0));
            dataReader.Start();


        }

        static void MyoGymFusion()
        {
            var Classifier = new MulticlassSupportVectorMachineClassifier();
            Classifier.Load("C:\\Users\\riz\\Desktop\\MyoGYm\\MyoGym_SVM1");
            var Features = new DataInFeatureOut(new MQTTReader<double[]>("/i5/myo/full", new SlidingWindow<double[]>(200, 0)), new IFeature[] {
                new HjorthParameters(8,9,10),
                        new StandardDeviation(8,9,10),
                        new Mean(8,9,10),
                        new Max(8,9,10),
                        new Min(8,9,10),
                        new Percentile(5,  8,9,10),
                        new Percentile(10, 8,9,10),
                        new Percentile(25, 8,9,10),
                        new Percentile(50, 8,9,10),
                        new Percentile(75, 8,9,10),
                        new Percentile(90, 8,9,10),
                        new Percentile(95, 8,9,10),
                        new ZeroCrossing(8,9,10),
                        new MeanCrossing(8,9,10),
                        new Entropy(8, 9, 10),
                        new Correlation(9, 10),
                        new Correlation(9, 11),
                        new Correlation(10, 11),

                        new HjorthParameters(11,12,13),
                        new StandardDeviation(11,12,13),
                        new Mean(11,12,13),
                        new Max(11,12,13),
                        new Min(11,12,13),
                        new Percentile(5,  11,12,13),
                        new Percentile(10, 11,12,13),
                        new Percentile(25, 11,12,13),
                        new Percentile(50, 11,12,13),
                        new Percentile(75, 11,12,13),
                        new Percentile(90, 11,12,13),
                        new Percentile(95, 11,12,13),
                        new ZeroCrossing(11,12,13),
                        new MeanCrossing(11,12,13),
                        new Entropy(11,12,13),

                        new StandardDeviation(0,1,2,3,4,5,6,7),
                        new Mean(0,1,2,3,4,5,6,7 ),
                        new Max(0,1,2,3,4,5,6,7 ),
                        new Min(0,1,2,3,4,5,6,7 ),
                        new Percentile(5, 0,1,2,3,4,5,6,7 ),
                        new Percentile(10, 0,1,2,3,4,5,6,7 ),
                        new Percentile(25,  0,1,2,3,4,5,6,7 ),
                        new Percentile(50, 0,1,2,3,4,5,6,7 ),
                        new Percentile(75, 0,1,2,3,4,5,6,7 ),
                        new Percentile(90, 0,1,2,3,4,5,6,7 ),
                        new Percentile(95, 0,1,2,3,4,5,6,7 ),

                        new SumLargerThan(25, 0,1,2,3,4,5,6,7 ),
                        new SumLargerThan(50, 0,1,2,3,4,5,6,7 ),
                        new SumLargerThan(100, 0,1,2,3,4,5,6,7 )


            });
            var Decision = new FeaturesInDecisionOut(new List<IFusionStrategy>() { Features }, Classifier);
            Decision.OnFusionFinished = (decision) =>
            {
                Console.WriteLine(decision);
            };

            Features.Start();

        }

        MulticlassSupportVectorMachineClassifier svmClassifier;

        static void Cmacc_mqtt()
        {
            FeatureManager featureManager = new FeatureManager();
            var Classifier = new DecisionTreeClassifier(FusionFramework.Classifiers.DecisionTreeLearningAlgorithms.C45Learning);
            Classifier.Load("C:\\Users\\riz\\Desktop\\WISDM\\WisdnDT");
            var dataReader = new MQTTReader<double[]>("/i5/mobileMotion/accelerometer", (dynamic output) =>
            {
                var FeaturSpace = FeatureManager.Generate(output, new List<IFeature>()
                    {
                        new Mean(),
                        new StandardDeviation(),
                        new MeanAbsoluteDeviation(),
                        new ResultantAcceleration(),
                        new BinDistribution(10),

                        new Variance(),
                        new Median(),
                        new Range(),
                        new Min(),
                        new Max(),
                        new RootMeanSquare()

                    });

                Console.WriteLine("HAR(" + Classifier.Classify(FeaturSpace) + ")");

            });
            dataReader.Add(new SlidingWindow<double[]>(200, 0));
            dataReader.Start();


        }

        static void CMacc()
        {
            FeatureManager featureManager = new FeatureManager();
            var Classifier = new DecisionTreeClassifier(FusionFramework.Classifiers.DecisionTreeLearningAlgorithms.C45Learning);
            Classifier.Load("C:\\Users\\riz\\Desktop\\MyoGymDT");
            var dataReader = new MQTTReader<double[]>("/i5/myo/full", (dynamic output) =>
            {
                var FeaturSpace = FeatureManager.Generate(output, new List<IFeature>()
                    {
                        new HjorthParameters(8,9,10),
                        new StandardDeviation(8,9,10),
                        new Mean(8,9,10),
                        new Max(8,9,10),
                        new Min(8,9,10),
                        new Percentile(5,  8,9,10),
                        new Percentile(10, 8,9,10),
                        new Percentile(25, 8,9,10),
                        new Percentile(50, 8,9,10),
                        new Percentile(75, 8,9,10),
                        new Percentile(90, 8,9,10),
                        new Percentile(95, 8,9,10),
                        new ZeroCrossing(8,9,10),
                        new MeanCrossing(8,9,10),
                        new Entropy(9, 10, 11),
                        new Correlation(9, 10),
                        new Correlation(9, 11),
                        new Correlation(10, 11),

                        new HjorthParameters(11,12,13),
                        new StandardDeviation(11,12,13),
                        new Mean(11,12,13),
                        new Max(11,12,13),
                        new Min(11,12,13),
                        new Percentile(5,  11,12,13),
                        new Percentile(10, 11,12,13),
                        new Percentile(25, 11,12,13),
                        new Percentile(50, 11,12,13),
                        new Percentile(75, 11,12,13),
                        new Percentile(90, 11,12,13),
                        new Percentile(95, 11,12,13),
                        new ZeroCrossing(11,12,13),
                        new MeanCrossing(11,12,13),
                        new Entropy(11,12,13),

                        new StandardDeviation(0,1,2,3,4,5,6,7),
                        new Mean(0,1,2,3,4,5,6,7 ),
                        new Max(0,1,2,3,4,5,6,7 ),
                        new Min(0,1,2,3,4,5,6,7 ),
                        new Percentile(5, 0,1,2,3,4,5,6,7 ),
                        new Percentile(10, 0,1,2,3,4,5,6,7 ),
                        new Percentile(25,  0,1,2,3,4,5,6,7 ),
                        new Percentile(50, 0,1,2,3,4,5,6,7 ),
                        new Percentile(75, 0,1,2,3,4,5,6,7 ),
                        new Percentile(90, 0,1,2,3,4,5,6,7 ),
                        new Percentile(95, 0,1,2,3,4,5,6,7 ),

                        new SumLargerThan(25, 0,1,2,3,4,5,6,7 ),
                        new SumLargerThan(50, 0,1,2,3,4,5,6,7 ),
                        new SumLargerThan(100, 0,1,2,3,4,5,6,7 )

                    });

                Console.WriteLine(Classifier.Classify(FeaturSpace));

            });
            dataReader.Add(new SlidingWindow<double[]>(200, 0));
            dataReader.Start();
        }

        static void Main(string[] args)
        {
            //Task taskA = Task.Factory.StartNew(MyoGymFusion);
            //Task taskB = Task.Factory.StartNew(MyoGym);

            //taskA.Wait();
            //taskB.Wait();
            //MyoGym();


            //Cmacc_mqtt();
            //MyoGym();
            // Step One Parse ARLEM document into object
            Console.WriteLine("Choose your workplace");
            int WorkplaceId = int.Parse(Console.ReadLine());

            ARLEMDecipher.ARLEMDecipher aRLEMDecipher = new ARLEMDecipher.ARLEMDecipher("http://127.0.0.1:8080/api");
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
                                switch (exportedAction.Id)
                                {
                                    case 36:
                                        sendMQTT("/i5/hololens/car/tyre", "spin");
                                    break;
                                    case 40:
                                        sendMQTT("/i5/car", "open");
                                    break;
                                    case 42:
                                        sendMQTT("/i5/hololens/car/steering", "spin");
                                        break;
                                }
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
                    } else
                    {
                        Console.WriteLine(output);
                    }
                });
            }

            Console.WriteLine(CurrentAction.Instruction);
            ModuleDictionary[CurrentAction.Module].Start();
            Console.ReadKey();

        }

        public static void sendMQTT(string path, string msg)
        {
            var Client = new MqttClient("iot.eclipse.org");
            Client.Connect(Guid.NewGuid().ToString());
            Client.Publish(path, Encoding.UTF8.GetBytes(msg));
        }

    }
}

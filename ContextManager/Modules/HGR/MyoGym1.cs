using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FusionFramework.Fusion.Strategies;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Data.Segmentators;
using FusionFramework.Data.Transformers.Common;
using FusionFramework.Features;
using FusionFramework.Features.TimeDomain;
using Accord.Math;
using FusionFramework.Features.Complex;
using FusionFramework.Classifiers.Trees;
using FusionFramework.VirtualSensor;
using System.IO;
using FusionFramework.Utilities;
using FusionFramework.Classifiers.VectorMachines;

namespace ContestManager.Modules.HGR
{

    class MyoGym1 : IModule
    {
        MulticlassSupportVectorMachineClassifier Classifier;

        DataInFeatureOut AllFeatures;
        FeaturesInDecisionOut Decision;

        Myo MyoVirtualSensor;

        public MyoGym1(string mqttURL)
        {
            RequiredSensors = new List<string>()
            {
                "/i5/myo/full"
            };
            MyoVirtualSensor = new Myo(mqttURL);

        }

        public void PreConfig()
        {
            Classifier = new MulticlassSupportVectorMachineClassifier();
            Classifier.Load("Modules/HGR/MyoGym_SVM");
            AllFeatures = new DataInFeatureOut(MyoVirtualSensor.GetConfiguration().Reader, MyoVirtualSensor.GetConfiguration().Features);
            Decision = new FeaturesInDecisionOut(new List<IFusionStrategy>() { AllFeatures }, Classifier);

        }

        public override void Config(FusionFinished fusionFinished)
        {
            PreConfig();
            Decision.OnFusionFinished = fusionFinished;
        }

        public override IFusionStrategy Config()
        {
            PreConfig();
            return Decision;
        }

        

        public override void Start()
        {
            AllFeatures.Start();
        }

        public override void Stop()
        {
            AllFeatures.Stop();
        }


        public void TrainFromFeatures()
        {
            List<double[]> FeaturSpace = new List<double[]>();
            List<int> FeaturLabel = new List<int>();
            List<List<double[]>> DataWindows = new List<List<double[]>>();
            List<List<int>> LabelWindows = new List<List<int>>();
            var dataReader = new CSVReader<double[]>("MyoGymFeatures.csv", false, (dynamic output) =>
            {
                FeaturSpace = output;
                var labelReader = new CSVReader<int>("MyoGymFeaturesLabels.csv", false, (dynamic outputLabels) =>
                {
                    FeaturLabel = outputLabels;

                    DecisionTreeClassifier dt = new DecisionTreeClassifier(FusionFramework.Classifiers.DecisionTreeLearningAlgorithms.C45Learning);
                    dt.Train(FeaturSpace, FeaturLabel, true);
                    dt.Save("LDAMyoGYM");
                });

                labelReader.Start();
            });
            dataReader.Start();
        }

        public override void Train()
        {
            List<double[]> FeaturSpace = new List<double[]>();
            List<int> FeaturLabel = new List<int>();
            List<List<double[]>> DataWindows = new List<List<double[]>>();
            List<List<int>> LabelWindows = new List<List<int>>();
            var dataReader = new CSVReader<double[]>("MyoGym.csv", false, (dynamic output) =>
            {
                DataWindows = output;
                DataWindows.ForEach(window =>
                {
                    FeaturSpace.Add(FeatureManager.Generate(window, new List<IFeature>()
                    {
                        new HjorthParameters(10, 11, 12),
                        new StandardDeviation(10, 11, 12),
                        new Mean(10, 11, 12),
                        new Min(10, 11, 12),
                        new Max(10, 11, 12),
                        new Percentile(5,  10, 11, 12),
                        new Percentile(10, 10, 11, 12),
                        new Percentile(25, 10, 11, 12),
                        new Percentile(50, 10, 11, 12),
                        new Percentile(75, 10, 11, 12),
                        new Percentile(90, 10, 11, 12),
                        new Percentile(95, 10, 11, 12),
                        new ZeroCrossing(10, 11, 12),
                        new MeanCrossing(10, 11, 12),
                        new Entropy(11, 12, 13),
                        new Correlation(10, 11),
                        new Correlation(10, 12),
                        new Correlation(11, 12),

                        new HjorthParameters(14, 15, 16),
                        new StandardDeviation(14, 15, 16),
                        new Mean(14, 15, 16),
                        new Min(14, 15, 16),
                        new Max(14, 15, 16),
                        new Percentile(5,  14, 15, 16),
                        new Percentile(10, 14, 15, 16),
                        new Percentile(25, 14, 15, 16),
                        new Percentile(50, 14, 15, 16),
                        new Percentile(75, 14, 15, 16),
                        new Percentile(90, 14, 15, 16),
                        new Percentile(95, 14, 15, 16),
                        new ZeroCrossing(14, 15, 16),
                        new MeanCrossing(14, 15, 16),
                        new Entropy(14, 15, 16),

                        new StandardDeviation(1,2,3,4,5,6,7,8),
                        new Mean(1,2,3,4,5,6,7,8 ),
                        new Min(1,2,3,4,5,6,7,8 ),
                        new Max(1,2,3,4,5,6,7,8 ),
                        new Median(1,2,3,4,5,6,7,8 ),
                        new Percentile(5, 1,2,3,4,5,6,7,8 ),
                        new Percentile(10, 1,2,3,4,5,6,7,8 ),
                        new Percentile(25,  1,2,3,4,5,6,7,8 ),
                        new Percentile(50, 1,2,3,4,5,6,7,8 ),
                        new Percentile(75, 1,2,3,4,5,6,7,8 ),
                        new Percentile(90, 1,2,3,4,5,6,7,8 ),
                        new Percentile(95, 1,2,3,4,5,6,7,8 ),

                        new SumLargerThan(25, 1,2,3,4,5,6,7,8 ),
                        new SumLargerThan(50, 1,2,3,4,5,6,7,8 ),
                        new SumLargerThan(100, 1,2,3,4,5,6,7,8 ),

                    }).ToArray());
                });

                var labelReader = new CSVReader<int>("MyoGymLabel.csv", false, (dynamic outputLabels) =>
                {
                    LabelWindows = outputLabels;
                    LabelWindows.ForEach(row =>
                    {
                        FeaturLabel.Add(row.GroupBy(x => x).OrderByDescending(g => g.Count()).Take(1).Select(i => i.Key).First());
                    });
                    SaveArrayAsCSV(FeaturSpace.ToArray(), "MyoGymFeatures.csv");
                    SaveArrayAsCSV(FeaturLabel.ToArray(), "MyoGymFeaturesLabels.csv");
                    DecisionTreeClassifier dt = new DecisionTreeClassifier(FusionFramework.Classifiers.DecisionTreeLearningAlgorithms.C45Learning);
                    dt.Train(FeaturSpace, FeaturLabel, true);
                    dt.Save("LDAMyoGYM");


                }, new SlidingWindow<int>(200, 25));
                labelReader.Start();


            }, new SlidingWindow<double[]>(200, 25));
            dataReader.Start();            
        }

        List<int> RemoveIndex = new List<int>();

        public void SaveArrayAsCSV(Array arrayToSave, string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                WriteMatrixToFile(arrayToSave, file);
            }
        }

        private void WriteMatrixToFile(Array items, TextWriter file)
        {
            foreach (object item in items)
            {
                if (item is Array)
                {
                    file.WriteLine(String.Join(",", ((double[])item).Select(x => x.ToString()).ToArray()));
                }
                else
                {
                    file.WriteLine(item);
                }
            }
        }

        public override void DecisionToMessage(int decision)
        {
            Console.WriteLine(decision);
        }
    }
}

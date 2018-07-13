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

namespace ContestManager.Modules.HGR
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

    class FrequenceyFeature : FusionFramework.Features.IFeature
    {
        public FrequenceyFeature()
        {
            Flavour = FeatureFlavour.VectorInVectorOut;
        }

        public override dynamic Calculate(dynamic data)
        {
            var Mean = Accord.Statistics.Measures.Mean(data);
            var STD = Accord.Statistics.Measures.StandardDeviation(data);
            double[] newData = ((double[])data);
            newData.Apply<double, double>((double value) => {
                return (value - Mean) / STD;
            });

            return new double[] { newData[1], TypeCasters.SubArray<double>(newData, 1, 5).Sum(), TypeCasters.SubArray<double>(newData, 6, 10).Sum() };
        }
    }

    class MyoGym : IModule
    {
        Accelerometer AccelerometerSensor;
        Gyroscope GyroscopeSensor;
        Emg EmgSensor;

        DecisionTreeClassifier Classifier;

        DataInFeatureOut AccelerometerFeatures, GryoFeatures, EMGFeatures;
        FeaturesInDecisionOut Decision;

        public MyoGym(string accelerometerMQTTURL, string gyroscopeMQTTURL, string emgMQTTURL)
        {
            AccelerometerSensor = new Accelerometer(accelerometerMQTTURL, 200, 25, new IFeature[] {
                new HjorthParameters(1,2,3),
                new StandardDeviation(1,2,3),
                new Mean(1,2,3),
                new Min(1,2,3),
                new Max(1,2,3),
                new Percentile(5,  1,2,3),
                new Percentile(10, 1,2,3),
                new Percentile(25, 1,2,3),
                new Percentile(50, 1,2,3),
                new Percentile(75, 1,2,3),
                new Percentile(90, 1,2,3),
                new Percentile(95, 1,2,3),
                new ZeroCrossing(1,2,3),
                new MeanCrossing(1,2,3),
                new Entropy(1,2,3),
                new Correlation(1, 2),
                new Correlation(1, 3),
                new Correlation(2, 3),
            });
            GyroscopeSensor = new Gyroscope(gyroscopeMQTTURL, 200, 25, new IFeature[] {
                new HjorthParameters(1, 2, 3),
                new StandardDeviation(1, 2, 3),
                new Mean(1, 2, 3),
                new Min(1, 2, 3),
                new Max(1, 2, 3),
                new Percentile(5,  1, 2, 3),
                new Percentile(10, 1, 2, 3),
                new Percentile(25, 1, 2, 3),
                new Percentile(50, 1, 2, 3),
                new Percentile(75, 1, 2, 3),
                new Percentile(90, 1, 2, 3),
                new Percentile(95, 1, 2, 3),
                new ZeroCrossing(1, 2, 3),
                new MeanCrossing(1, 2, 3),
                new Entropy(1, 2, 3),
            });
            EmgSensor = new Emg(emgMQTTURL, 200, 25, new IFeature[] {
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
            });
            RequiredSensors = new List<string>()
            {
                "/i5/myo/accelerometer",
                "/i5/myo/gyroscope",
                "/i5/myo/emg",
            };

        }

        public void PreConfig()
        {
            Classifier = new DecisionTreeClassifier(FusionFramework.Classifiers.DecisionTreeLearningAlgorithms.C45Learning);
            Classifier.Load("LDAMyoGYM");
            AccelerometerFeatures = new DataInFeatureOut(AccelerometerSensor.GetConfiguration().Reader, AccelerometerSensor.GetConfiguration().Features);
            GryoFeatures = new DataInFeatureOut(GyroscopeSensor.GetConfiguration().Reader, GyroscopeSensor.GetConfiguration().Features);
            EMGFeatures = new DataInFeatureOut(EmgSensor.GetConfiguration().Reader, EmgSensor.GetConfiguration().Features);
            var CombinedFeatures = new FeaturesInFeatureOut(new List<IFusionStrategy>() { AccelerometerFeatures, GryoFeatures, EMGFeatures });
            Decision = new FeaturesInDecisionOut(new List<IFusionStrategy>() { CombinedFeatures }, Classifier);

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
            AccelerometerFeatures.Start();
            GryoFeatures.Start();
            EMGFeatures.Start();
        }

        public override void Stop()
        {
            AccelerometerFeatures.Stop();
            GryoFeatures.Stop();
            EMGFeatures.Stop();
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

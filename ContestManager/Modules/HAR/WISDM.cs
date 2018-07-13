using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Data.Segmentators;
using FusionFramework.Data.Transformers.Common;
using FusionFramework.Features;
using FusionFramework.Features.TimeDomain;
using FusionFramework.Fusion.Strategies;
using Accord.Math;
using FusionFramework.Features.Complex;
using FusionFramework.Classifiers.Trees;
using FusionFramework.VirtualSensor;
using FusionFramework.Data.Transformers;

namespace ContestManager.Modules.HAR
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
            Array = Array.RemoveColumn<double>(0);
            return new Mean().Calculate(Array.Select(x => (double)new Magnitude().Calculate(x)).ToArray<double>());
        }
    }

    class PreProcessData : IDataTransformer
    {
        public override void Transform(ref List<double[]> data)
        {
            var TimeStamps = data.ToArray().GetColumn<double>(0);
            data = new List<double[]>(data.ToArray().RemoveColumn<double>(0));
            new Normalize().Transform(ref data);
            data =  new List<double[]>(Matrix.InsertColumn<double, double>(data.ToArray(), TimeStamps, 0));
        }

        public override void Transform(ref List<double> data)
        {
            
        }

        public override void Transform(ref List<int> data)
        {
            
        }
    }

    class WISDM : IModule
    {
        DecisionTreeClassifier Classifier;
        Accelerometer AccelerometerSensor;

        DataInFeatureOut Feature1;
        FeaturesInDecisionOut Decision;

        Dictionary<int, string> Decisions = new Dictionary<int, string>()
        {
            {1, "Walking" },
            {2, "Jogging" },
            {3, "Upstairs" },
            {4, "Downstairs" },
            {5, "Sitting" },
            {6, "Standing" },
        };

        public WISDM(string mqttURL)
        {
            AccelerometerSensor = new Accelerometer(mqttURL, 200, 25, new IFeature[] {
                new Mean(1, 2, 3),
                new StandardDeviation(1, 2, 3),
                new MeanAbsoluteDeviation(1, 2, 3),
                new AverageTimeBetweenPeaks(0, 1, 2, 3),
                new ResultantAcceleration(),
                new BinDistribution(10, 1, 2, 3)
            });
            RequiredSensors = new List<string>()
            {
                mqttURL
            };

        }        

        public void PreConfig()
        {
            Classifier = new DecisionTreeClassifier(FusionFramework.Classifiers.DecisionTreeLearningAlgorithms.C45Learning);
            Classifier.Load("DecisionTreeWSDM");
            Feature1 = new DataInFeatureOut(AccelerometerSensor.GetConfiguration().Reader, AccelerometerSensor.GetConfiguration().Features);
            Feature1.Add(new PreProcessData());
            Decision = new FeaturesInDecisionOut(new List<IFusionStrategy>() { Feature1 }, Classifier);

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
            Feature1.Start();
        }

        public override void Stop()
        {
            Feature1.Stop();
        }

        public override void Train()
        {
            List<double[]> FeaturSpace = new List<double[]>();
            List<int> FeaturLabel = new List<int>();
            List<List<double[]>> DataWindows = new List<List<double[]>>();
            List<List<int>> LabelWindows = new List<List<int>>();
            FeatureManager featureManager = new FeatureManager();
            featureManager.Add(new Mean(1, 2, 3));
            featureManager.Add(new StandardDeviation(1, 2, 3));
            featureManager.Add(new MeanAbsoluteDeviation(1, 2, 3));
            featureManager.Add(new AverageTimeBetweenPeaks(0, 1, 2, 3));
            featureManager.Add(new ResultantAcceleration());
            featureManager.Add(new BinDistribution(10, 1, 2, 3));
            featureManager.Add(new PreProcessData());

            var dataReader = new CSVReader<double[]>("data.txt", false, (dynamic output) =>
            {
                DataWindows = output;
                DataWindows.ForEach(window =>
                {
                    FeaturSpace.Add(featureManager.Generate(window).ToArray());
                });

                var labelReader = new CSVReader<int>("label.txt", false, (dynamic outputLabels) =>
                {
                    LabelWindows = outputLabels;
                    LabelWindows.ForEach(row =>
                    {
                        FeaturLabel.Add(row.GroupBy(x => x).OrderByDescending(g => g.Count()).Take(1).Select(i => i.Key).First());
                    });

                    DecisionTreeClassifier dt = new DecisionTreeClassifier(FusionFramework.Classifiers.DecisionTreeLearningAlgorithms.C45Learning);
                    dt.Train(FeaturSpace, FeaturLabel, true);
                    dt.Save("DecisionTreeWSDM");


                }, new SlidingWindow<int>(200, 0));
                labelReader.Start();

            }, new SlidingWindow<double[]>(200, 0));
            dataReader.Add(new Normalize());
            dataReader.Start();
        }

        public override void DecisionToMessage(int decision)
        {
            Console.WriteLine("User is " + Decisions[decision]);
        }
    }
}

using FusionFramework.Classifiers.VectorMachines;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Features;
using FusionFramework.Fusion.Strategies;
using FusionFramework.VirtualSensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestManager.Modules.XOR
{
    class XORModule1 : IModule
    {
        MulticlassSupportVectorMachineClassifier Classifier;
        Binary BinaryOne, BinaryTwo;

        DataInFeatureOut Feature1, Feature2;
        FeaturesInFeatureOut FilteredFeatures;
        FeaturesInDecisionOut Decision;

        public XORModule1(Binary binaryOne, Binary binaryTwo)
        {
            BinaryOne = binaryOne;
            BinaryTwo = binaryTwo;
            RequiredSensors = new List<string>()
            {
                "i5/binary/0",
                "i5/binary/1"
            };

        }

        public override bool IsCalculatable(List<string> availableSensors)
        {
            if(RequiredSensors.Intersect<string>(availableSensors).Count() > 0)
            {
                return false;
            }
            return true;
        }
      
        public override void Config(FusionFinished fusionFinished)
        {
            Classifier = new MulticlassSupportVectorMachineClassifier();
            Classifier.Load("XORModule1");
            Feature1 = new DataInFeatureOut(BinaryOne.GetConfiguration().Reader, BinaryOne.GetConfiguration().Features);
            Feature2 = new DataInFeatureOut(BinaryTwo.GetConfiguration().Reader, BinaryTwo.GetConfiguration().Features);
            FilteredFeatures = new FeaturesInFeatureOut(new List<IFusionStrategy>() { Feature1, Feature2 });
            Decision = new FeaturesInDecisionOut(new List<IFusionStrategy>() { FilteredFeatures }, Classifier);
            Decision.OnFusionFinished = fusionFinished;
        }

        public override void Train()
        {
            double[][] inputs =
                {
                    new double[] { 0, 1, 1, 0 }, //  0 
                    new double[] { 0, 1, 0, 0 }, //  0
                    new double[] { 0, 0, 1, 0 }, //  0
                    new double[] { 0, 1, 1, 0 }, //  0
                    new double[] { 0, 1, 0, 0 }, //  0
                    new double[] { 1, 0, 0, 0 }, //  1
                    new double[] { 1, 0, 0, 0 }, //  1
                    new double[] { 1, 0, 0, 1 }, //  1
                    new double[] { 0, 0, 0, 1 }, //  1
                    new double[] { 0, 0, 0, 1 }, //  1
                    new double[] { 1, 1, 1, 1 }, //  2
                    new double[] { 1, 0, 1, 1 }, //  2
                    new double[] { 1, 1, 0, 1 }, //  2
                    new double[] { 0, 1, 1, 1 }, //  2
                    new double[] { 1, 1, 1, 1 }, //  2
                };
            int[] outputs = // those are the class labels
            {
                0, 0, 0, 0, 0,
                1, 1, 1, 1, 1,
                2, 2, 2, 2, 2,
            };

            Classifier = new MulticlassSupportVectorMachineClassifier();
            Classifier.Train(new List<double[]>(inputs), new List<int>(outputs));
            Classifier.Save("XORModule1");
        }

        public override void Start()
        {
            Feature1.Start();
            Feature2.Start();
        }

        public override void Stop()
        {
            Feature1.Stop();
            Feature2.Stop();
        }
    }
}

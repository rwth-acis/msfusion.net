using Accord.MachineLearning.DecisionTrees;
using Accord.Math.Optimization.Losses;
using FusionFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Classifiers
{
    class RandomForestClassifier : IClassifier
    {
        RandomForestLearning LearningAlgorithm;
        RandomForest Model;
        int NumTrees;
        double SamplePropotion;

        RandomForestClassifier(int numTrees)
        {
            NumTrees = numTrees;
        }
        RandomForestClassifier(int numTrees, double samplePropotion)
        {
            SamplePropotion = samplePropotion;
        }

        public override double Classify(List<double> featureVector)
        {
            return Model.Decide(featureVector.ToArray());
        }

        public override void Load(string path)
        {
            Model = Accord.IO.Serializer.Load<RandomForest>(path);
        }

        public override void Save(string path)
        {
           Accord.IO.Serializer.Save<RandomForest>(Model, path);
        }
               
        public override void Train(List<double[]> trainingData, List<double> trainingLabels, bool calculateError = true)
        {
            LearningAlgorithm = new RandomForestLearning();
            if (NumTrees > 0)
            {
                LearningAlgorithm.NumberOfTrees = NumTrees;
            }

            if (SamplePropotion > 0)
            {
                LearningAlgorithm.SampleRatio = SamplePropotion;
            }
            int[][] TrainingData = TypeCasters.DoubleMultiArrayToInt(trainingData).ToArray();
            int[] TrainingLabels = TypeCasters.DoubleArrayToInt(trainingLabels).ToArray();

            Model = LearningAlgorithm.Learn(TrainingData, TrainingLabels);
            if(calculateError == true)
            {
                TrainingError = new ZeroOneLoss(TrainingLabels).Loss(Model.Decide(TrainingData));
            }
        }

        public override void Train(List<double[]> trainingData, List<double> trainingLabels, List<double[]> testingData, List<double> testingLabels)
        {
            Train(trainingData, trainingLabels);
            CalculateTrainingError(testingData, testingLabels);
        }

        public override void CalculateTrainingError(List<double[]> testData, List<double> testOutput)
        {
            TrainingError = new ZeroOneLoss(testData.ToArray()).Loss(Model.Decide(testData.ToArray()));
        }
    }
}

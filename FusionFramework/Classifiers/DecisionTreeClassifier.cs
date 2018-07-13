using Accord.MachineLearning.DecisionTrees.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using FusionFramework.Utilities;
using Accord.Math.Optimization.Losses;
using Accord.MachineLearning.DecisionTrees;

namespace FusionFramework.Classifiers
{
    class DecisionTreeClassifier : IClassifier
    {

        dynamic LearningAlgorithm;
        DecisionTree Model;
        DecisionTreeLearningAlgorithms LearningAlgorithmName;

        DecisionTreeClassifier(DecisionTreeLearningAlgorithms algorithm)
        {
            LearningAlgorithmName = algorithm;
        }

        public override double Classify(List<double> featureVector)
        {
            return Model.Decide(featureVector.ToArray());
        }

        public override void Load(string path)
        {
            Model = Accord.IO.Serializer.Load<DecisionTree>(path);
        }

        public override void Save(string path)
        {
            Accord.IO.Serializer.Save<DecisionTree>(Model, path);
        }

        public override void Train(List<double[]> trainingData, List<double> trainingLabels, bool calculateError = true)
        {
            int[][] TrainingData = TypeCasters.DoubleMultiArrayToInt(trainingData).ToArray();
            int[] TrainingLabels = TypeCasters.DoubleArrayToInt(trainingLabels).ToArray();
            if (LearningAlgorithmName == DecisionTreeLearningAlgorithms.ID3Learning)
            {
                LearningAlgorithm = new ID3Learning();
            }
            else
            {
                LearningAlgorithm = new C45Learning();
            }

            Model = LearningAlgorithm.Learn(TrainingData, TrainingLabels);
            if(calculateError == true)
            {
                CalculateTrainingError(trainingData, trainingLabels);
            }
        }

        public override void Train(List<double[]> trainingData, List<double> trainingLabels, List<double[]> testingData, List<double> testingLabels)
        {
            Train(trainingData, trainingLabels, false);
            CalculateTrainingError(testingData, testingLabels);
        }

        public override void CalculateTrainingError(List<double[]> testData, List<double> testOutput)
        {
            TrainingError = new ZeroOneLoss(testData.ToArray()).Loss(Model.Decide(testData.ToArray()));
        }

    }
}

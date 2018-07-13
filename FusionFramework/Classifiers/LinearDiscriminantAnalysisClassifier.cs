using Accord.Math.Optimization.Losses;
using Accord.Statistics.Analysis;
using FusionFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Classifiers
{
    class LinearDiscriminantAnalysisClassifier : IClassifier
    {

        LinearDiscriminantAnalysis LearningAlgorithm;
        LinearDiscriminantAnalysis.Pipeline Model;

        public override double Classify(List<double> featureVector)
        {
            return Model.Decide(featureVector.ToArray());
        }

        public override void Load(string path)
        {
            Model = Accord.IO.Serializer.Load<LinearDiscriminantAnalysis.Pipeline>(path);
        }

        public override void Save(string path)
        {
            Accord.IO.Serializer.Save<LinearDiscriminantAnalysis.Pipeline>(Model, path);
        }

        public override void Train(List<double[]> trainingData, List<double> trainingLabels, bool calculateError = true)
        {
            int[] TrainingLabels = TypeCasters.DoubleArrayToInt(trainingLabels).ToArray();
            double[][] TrainingData = trainingData.ToArray();
            Model = LearningAlgorithm.Learn(TrainingData, TrainingLabels);
            if(calculateError == true)
            {
                CalculateTrainingError(trainingData, trainingLabels);
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

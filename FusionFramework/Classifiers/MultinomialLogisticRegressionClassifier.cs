using Accord.Math.Optimization;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Classifiers
{
    class MultinomialLogisticRegressionClassifier : IClassifier
    {
        dynamic LearningAlgorithm;
        MultinomialLogisticRegression Model;
        LogisticRegressionOptimizationAlgorithm LearningAlgorithmName;

        double[][] Probabilities;

        public MultinomialLogisticRegressionClassifier(LogisticRegressionOptimizationAlgorithm learningAlgorithm)
        {
            LearningAlgorithmName = learningAlgorithm;
        }
        
        public override double Classify(List<double> featureVector)
        {
            return Model.Decide(featureVector.ToArray());
        }

        public override void Load(string path)
        {
            Model = Accord.IO.Serializer.Load<MultinomialLogisticRegression>(path);
        }

        public override void Save(string path)
        {
            Accord.IO.Serializer.Save<MultinomialLogisticRegression>(Model, path);
        }
        

        public override void Train(List<double[]> trainingData, List<double> trainingLabels, bool calculateError = true)
        {
            if (LearningAlgorithmName == LogisticRegressionOptimizationAlgorithm.ConjugateGradient)
            {
                LearningAlgorithm = new MultinomialLogisticLearning<ConjugateGradient>();
            }
            else if (LearningAlgorithmName == LogisticRegressionOptimizationAlgorithm.GradientDescent)
            {
                LearningAlgorithm = new MultinomialLogisticLearning<GradientDescent>();
            }
            else if (LearningAlgorithmName == LogisticRegressionOptimizationAlgorithm.BroydenFletcherGoldfarbShanno)
            {
                LearningAlgorithm = new MultinomialLogisticLearning<BroydenFletcherGoldfarbShanno>();
            }
            else
            {
                LearningAlgorithm = new LowerBoundNewtonRaphson()
                {
                    MaxIterations = 100,
                    Tolerance = 1e-6
                };
            }

            Model = LearningAlgorithm.Learn(trainingData.ToArray(), trainingLabels.ToArray());
            Probabilities = Model.Probabilities(trainingData.ToArray());
            if(calculateError == true)
            {
                CalculateTrainingError(trainingData, trainingLabels);
            }
        }

        public override void Train(List<double[]> trainingData, List<double> trainingLabels, List<double[]> testingData, List<double> testingLabels)
        {
            Train(trainingData, trainingLabels);
        }

        public override void CalculateTrainingError(List<double[]> testData, List<double> testOutput)
        {
            TrainingError = new ZeroOneLoss(testData.ToArray()).Loss(Model.Decide(testData.ToArray())); throw new NotImplementedException();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Classifiers
{
    enum DecisionTreeLearningAlgorithms
    {
        ID3Learning,
        C45Learning
    };

    enum LogisticRegressionOptimizationAlgorithm
    {
        LowerBoundNewtonRaphson,
        ConjugateGradient,
        GradientDescent,
        BroydenFletcherGoldfarbShanno
    };

    enum SVMLearningAlgorithm
    {
        LinearDualCoordinateDescentLinear,
        SequentialMinimalOptimizationGaussian
    };
    enum SVMFlavours
    {
        Linear,
        Gaussian
    };

    abstract class IClassifier
    {
        public double TrainingError;

        public abstract double Classify(List<double> decisionVector);
        public abstract void Train(List<double[]> trainingData, List<double> trainingLabels, bool calculateError = true);
        public abstract void Train(List<double[]> trainingData, List<double> trainingLabels, List<double[]> testingData, List<double> testingLabels);

        public abstract void Save(string path);
        public abstract void Load(string path);

        public abstract void CalculateTrainingError(List<double[]> testData, List<double> testOutput);
    }
}

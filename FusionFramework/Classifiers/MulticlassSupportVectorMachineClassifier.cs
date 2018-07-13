using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Classifiers
{
    class MulticlassSupportVectorMachineClassifier : IClassifier
    {
        dynamic LearningAlgorithm;
        SupportVectorMachine Model;
        SVMLearningAlgorithm LearningAlgorithmName;
        int NumTrees;
        double SamplePropotion;

        MulticlassSupportVectorMachineClassifier(SVMLearningAlgorithm learningAlgorithm)
        {
            LearningAlgorithmName = learningAlgorithm;
        }

        public override void CalculateTrainingError(List<double[]> testData, List<double> testOutput)
        {
            throw new NotImplementedException();
        }

        public override double Classify(List<double> decisionVector)
        {
            throw new NotImplementedException();
        }

        public override void Load(string path)
        {
            throw new NotImplementedException();
        }

        public override void Save(string path)
        {
            throw new NotImplementedException();
        }

        public override void Train(List<double[]> trainingData, List<double> trainingLabels, bool calculateError = true)
        {
            if(LearningAlgorithmName == SVMLearningAlgorithm.LinearDualCoordinateDescentLinear)
            {
                LearningAlgorithm = new MulticlassSupportVectorLearning<Linear>()
                {
                    Learner = (p) => new LinearDualCoordinateDescent()
                    {
                        Loss = Loss.L2
                    }
                };
            } else if (LearningAlgorithmName == SVMLearningAlgorithm.SequentialMinimalOptimizationGaussian)
            {
                LearningAlgorithm = new MulticlassSupportVectorLearning<Gaussian>()
                {
                    Learner = (param) => new SequentialMinimalOptimization<Gaussian>()
                    {
                        // Estimate a suitable guess for the Gaussian kernel's parameters.
                        // This estimate can serve as a starting point for a grid search.
                        UseKernelEstimation = true
                    }
                };
            }

            Model = LearningAlgorithm.Learn(trainingData, trainingLabels);
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
    }
}

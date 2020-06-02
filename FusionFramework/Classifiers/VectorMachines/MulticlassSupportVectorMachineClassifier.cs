using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Analysis;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Classifiers.VectorMachines
{
    public class MulticlassSupportVectorMachineClassifier : IClassifier
    {
        /// <summary>
        /// The Learning algorithm used to train the model.
        /// </summary>
        MulticlassSupportVectorLearning<Linear> LearningAlgorithm;

        /// <summary>
        /// Model that will be used for classification and training
        /// </summary>
        MulticlassSupportVectorMachine<Linear> Model;

        /// <summary>
        /// Perform classification from loaded model and returns the infered class.
        /// </summary>
        /// <param name="vector">The input vector that should be list of double.</param>
        /// <returns>Resultant class as an integer</returns>
        public override int Classify(List<double> vector)
        {
            return Model.Decide(vector.ToArray());
        }

        /// <summary>
        /// Trains the classifier and computes the training error if option provided.
        /// </summary>
        /// <param name="trainingData">The training data that will be used to train classifier.</param>
        /// <param name="trainingLabels">The training labels related to provided training data.</param>
        /// <param name="calculateError">The boolean check to tell if the training error should be calculated.</param>
        public override void Train(List<double[]> trainingData, List<int> trainingLabels, bool calculateError = true)
        {
            LearningAlgorithm = new MulticlassSupportVectorLearning<Linear>()
            {
                Learner = (p) => new LinearDualCoordinateDescent()
                {
                    Loss = Loss.L2
                }
            };

            Model = LearningAlgorithm.Learn(trainingData.ToArray(), trainingLabels.ToArray());
            if(calculateError == true)
            {
                CalculateTrainingError(trainingData, trainingLabels);
            }
        }

        /// <summary>
        /// Trains the classifier and computes the training error if option provided.
        /// </summary>
        /// <param name="trainingData">The training data that will be used to train the classifier.</param>
        /// <param name="trainingLabels">The training labels related to provided training data.</param>
        /// <param name="testingData">The testing data that will be used to test the classifier.</param>
        /// <param name="testingLabels">The testing labels related to provided test the output of the classifier.</param>
        public override void Train(List<double[]> trainingData, List<int> trainingLabels, List<double[]> testingData, List<int> testingLabels)
        {
            Train(trainingData, trainingLabels);
            CalculateTrainingError(testingData, testingLabels);
        }

        /// <summary>
        /// Loads the trained model.
        /// </summary>
        /// <param name="path">The location from where to load the trained model.</param>
        public override void Load(string path)
        {
            Model = Accord.IO.Serializer.Load<MulticlassSupportVectorMachine<Linear>>(path);
        }

        /// <summary>
        /// Saves the trained model.
        /// </summary>
        /// <param name="path">The location where to save the trained model.</param>
        public override void Save(string path)
        {
            Accord.IO.Serializer.Save<MulticlassSupportVectorMachine<Linear>>(Model, path);
        }

        /// <summary>
        /// Calculates error after training the model.
        /// </summary>
        /// <param name="testData">The test data that would be used to calculate error.</param>
        /// <param name="testOutput">The test labels that would be used to calculate error.</param>
        public override void CalculateTrainingError(List<double[]> testData, List<int> testOutput)
        {
            TrainingError = new ZeroOneLoss(testOutput.ToArray()).Loss(Model.Decide(testData.ToArray()));
            GeneralConfusionMatrix cm = GeneralConfusionMatrix.Estimate(Model, testData.ToArray(), testOutput.ToArray());
            double error = cm.Error;         // should be 0.066666666666666652
            double accuracy = cm.Accuracy;   // should be 0.93333333333333335
            double kappa = cm.Kappa;         // should be 0.9
            double chiSquare = cm.ChiSquare; // should be 248.52216748768473
        }

    }
}

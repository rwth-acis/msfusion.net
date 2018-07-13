using Accord.Math.Optimization.Losses;
using Accord.Statistics.Analysis;
using FusionFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Classifiers.Analysis
{
    /// <summary>
    /// Linear Discriminant Analysis (LDA) is a method of finding such a linear combination of variables which best separates two or more classes.
    /// <i>Fisher, R. A. (1936). "The Use of Multiple Measurements in Taxonomic Problems". Annals of Eugenics. 7 (2): 179–188.</i>
    /// </summary>
    public class LinearDiscriminantAnalysisClassifier : IClassifier
    {
        /// <summary>
        /// The Learning algorithm used to train the model.
        /// </summary>
        LinearDiscriminantAnalysis LearningAlgorithm;

        public LinearDiscriminantAnalysisClassifier()
        {
            LearningAlgorithm = new LinearDiscriminantAnalysis();
        }

        public LinearDiscriminantAnalysisClassifier(string path)
        {
            LearningAlgorithm = new LinearDiscriminantAnalysis();
            Load(path);
        }

        /// <summary>
        /// Model that will be used for classification and training
        /// </summary>
        LinearDiscriminantAnalysis.Pipeline Model;

        /// <summary>
        /// Perform  classification from loaded model and returns the infered class.
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
            Model = LearningAlgorithm.Learn(trainingData.ToArray(), trainingLabels.ToArray());
            if (calculateError == true)
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
            Model = Accord.IO.Serializer.Load<LinearDiscriminantAnalysis.Pipeline>(path);
        }

        /// <summary>
        /// Saves the trained model.
        /// </summary>
        /// <param name="path">The location where to save the trained model.</param>
        public override void Save(string path)
        {
            Accord.IO.Serializer.Save<LinearDiscriminantAnalysis.Pipeline>(Model, path);
        }

        /// <summary>
        /// Calculates error after training the model.
        /// </summary>
        /// <param name="testData">The test data that would be used to calculate error.</param>
        /// <param name="testOutput">The test labels that would be used to calculate error.</param>
        public override void CalculateTrainingError(List<double[]> testData, List<int> testOutput)
        {
            int[] res = Model.Decide(testData.ToArray());
            TrainingError = new ZeroOneLoss(testOutput.ToArray()).Loss(res);
        }
    }
}

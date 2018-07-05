using Accord.Math.Optimization;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using System.Collections.Generic;

namespace FusionFramework.Classifiers.Regression
{
    /// <summary>
    /// A classification method for multiclass logistic regression.
    /// </summary>
    public class MultinomialLogisticRegressionClassifier : IClassifier
    {
        /// <summary>
        /// The Learning algorithm used to train the model.
        /// </summary>
        dynamic LearningAlgorithm;

        /// <summary>
        /// Model that will be used for classification and training.
        /// </summary>
        MultinomialLogisticRegression Model;

        /// <summary>
        /// The name choosen by client for learning algorithm.
        /// </summary>
        LogisticRegressionOptimizationAlgorithm LearningAlgorithmName;

        /// <summary>
        /// Porbability distribution for classified classes
        /// </summary>
        double[][] Probabilities;

        /// <summary>
        /// Instantiate Classifier with optimization algorithm
        /// </summary>
        /// <param name="learningAlgorithm">The optimization algorithm for Logestic regression</param>
        public MultinomialLogisticRegressionClassifier(LogisticRegressionOptimizationAlgorithm learningAlgorithm)
        {
            LearningAlgorithmName = learningAlgorithm;
        }

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
        }

        /// <summary>
        /// Loads the trained model.
        /// </summary>
        /// <param name="path">The location from where to load the trained model.</param>
        public override void Load(string path)
        {
            Model = Accord.IO.Serializer.Load<MultinomialLogisticRegression>(path);
        }

        /// <summary>
        /// Saves the trained model.
        /// </summary>
        /// <param name="path">The location where to save the trained model.</param>
        public override void Save(string path)
        {
            Accord.IO.Serializer.Save<MultinomialLogisticRegression>(Model, path);
        }

        /// <summary>
        /// Calculates error after training the model.
        /// </summary>
        /// <param name="testData">The test data that would be used to calculate error.</param>
        /// <param name="testOutput">The test labels that would be used to calculate error.</param>
        public override void CalculateTrainingError(List<double[]> testData, List<int> testOutput)
        {
            TrainingError = new ZeroOneLoss(testData.ToArray()).Loss(Model.Decide(testData.ToArray()));
        }

    }
}

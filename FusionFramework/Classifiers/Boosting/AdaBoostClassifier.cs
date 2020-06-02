using Accord.MachineLearning.Boosting;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math.Optimization.Losses;
using System.Collections.Generic;

namespace FusionFramework.Classifiers.Boosting
{
    /// <summary>
    /// AdaBoost is a machine learning algorithm presented by Yoav Freund and Robert Schapire.
    /// This adaboost algorithm uses decision tree as weak classifier which is considered as the best fit combination for Adaboost.
    /// <i>Freund, Yoav; Schapire, Robert E (1997). "A decision-theoretic generalization of on-line learning and an application to boosting". Journal of Computer and System Sciences. 55: 119. CiteSeerX 10.1.1.32.8918 Freely accessible. doi:10.1006/jcss.1997.1504: original paper of Yoav Freund and Robert E.Schapire where AdaBoost is first introduced</i>.
    /// </summary>
    public class AdaBoostingClassifier : IClassifier
    {
        /// <summary>
        /// The Learning algorithm used to train the model.
        /// </summary>
        AdaBoost<DecisionTree> LearningAlgorithm;

        /// <summary>
        /// Model that will be used for classification and training.
        /// </summary>
        Boost<DecisionTree> Model;

        /// <summary>
        /// Creates an instance of AdaBoost Classifier.
        /// </summary>
        /// <param name="maxIterations">The maximum iterations used by adaboost</param>
        /// <param name="tolerance">The maximum toerlance</param>
        public AdaBoostingClassifier(int maxIterations, double tolerance)
        {
            LearningAlgorithm = new AdaBoost<DecisionTree>();
            LearningAlgorithm.Learner = (param) => new C45Learning();
            LearningAlgorithm.MaxIterations = maxIterations;
            LearningAlgorithm.Tolerance = tolerance;
        }

        /// <summary>
        /// Creates an instance of AdaBoost Classifier.
        /// </summary>
        /// <param name="maxIterations">The maximum iterations used by adaboost</param>
        /// <param name="tolerance">The maximum toerlance</param>
        /// <param name="maxHeight">The maximum tree height of the decision tree.</param>
        /// <param name="maxVariables">The maximum variables of the decision tree.</param>
        public AdaBoostingClassifier(int maxIterations, double tolerance, int maxHeight, int maxVariables)
        {
            LearningAlgorithm = new AdaBoost<DecisionTree>();
            LearningAlgorithm.Learner = (param) => new C45Learning()
            {
                MaxHeight = maxHeight,
                MaxVariables = maxVariables
            };
            LearningAlgorithm.MaxIterations = maxIterations;
            LearningAlgorithm.Tolerance = tolerance;
        }

        /// <summary>
        /// Perform classification from loaded model and returns the infered class.
        /// </summary>
        /// <param name="vector">The input vector that should be list of double.</param>
        /// <returns>Resultant class as an integer</returns>
        public override int Classify(List<double> vector)
        {
            return Model.ToMulticlass().Decide(vector.ToArray());
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
        }

        /// <summary>
        /// Loads the trained model.
        /// </summary>
        /// <param name="path">The location from where to load the trained model.</param>
        public override void Load(string path)
        {
            Model = Accord.IO.Serializer.Load<Boost<DecisionTree>>(path);
        }

        /// <summary>
        /// Saves the trained model.
        /// </summary>
        /// <param name="path">The location where to save the trained model.</param>
        public override void Save(string path)
        {
            Accord.IO.Serializer.Save<Boost<DecisionTree>>(Model, path);
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

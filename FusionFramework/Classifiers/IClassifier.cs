using System.Collections.Generic;

namespace FusionFramework.Classifiers
{
    /// <summary>
    /// Selection for Decision Tree learning algorithm.
    /// </summary>
    public enum DecisionTreeLearningAlgorithms
    {
        ID3Learning,
        C45Learning
    };

    /// <summary>
    /// Selection for Logistic Regression optimization algorithm.
    /// </summary>
    public enum LogisticRegressionOptimizationAlgorithm
    {
        LowerBoundNewtonRaphson,
        ConjugateGradient,
        GradientDescent,
        BroydenFletcherGoldfarbShanno
    };

    /// <summary>
    /// Selection for Support Vector Machine's learning algorithm.
    /// </summary>
    public enum SVMLearningAlgorithm
    {
        LinearDualCoordinateDescentLinear,
        SequentialMinimalOptimizationGaussian
    };

    /// <summary>
    /// Selection for Support Vector Machine's kernels.
    /// </summary>
    public enum SVMFlavours
    {
        Linear,
        Gaussian
    };

    /// <summary>
    /// Abstract class for Classifiers allowing the client to integrate more classification algorithms if needed.
    /// </summary>
    public abstract class IClassifier
    {
        /// <summary>
        /// Get only property for training error after training step execution.
        /// </summary>
        public double TrainingError;

        /// <summary>
        /// Perform classification from loaded model and returns the infered class.
        /// </summary>
        /// <param name="vector">The input vector that should be list of double.</param>
        /// <returns>Resultant class as an integer</returns>
        public abstract int Classify(List<double> vector);

        /// <summary>
        /// Trains the classifier and computes the training error if option provided.
        /// </summary>
        /// <param name="trainingData">The training data that will be used to train classifier.</param>
        /// <param name="trainingLabels">The training labels related to provided training data.</param>
        /// <param name="calculateError">The boolean check to tell if the training error should be calculated.</param>
        public abstract void Train(List<double[]> trainingData, List<int> trainingLabels, bool calculateError = true);

        /// <summary>
        /// Trains the classifier and computes the training error if option provided.
        /// </summary>
        /// <param name="trainingData">The training data that will be used to train the classifier.</param>
        /// <param name="trainingLabels">The training labels related to provided training data.</param>
        /// <param name="testingData">The testing data that will be used to test the classifier.</param>
        /// <param name="testingLabels">The testing labels related to provided test the output of the classifier.</param>
        public abstract void Train(List<double[]> trainingData, List<int> trainingLabels, List<double[]> testingData, List<int> testingLabels);

        /// <summary>
        /// Saves the trained model.
        /// </summary>
        /// <param name="path">The location where to save the trained model.</param>
        public abstract void Save(string path);

        /// <summary>
        /// Loads the trained model.
        /// </summary>
        /// <param name="path">The location from where to load the trained model.</param>
        public abstract void Load(string path);

        /// <summary>
        /// Calculates error after training the model.
        /// </summary>
        /// <param name="testData">The test data that would be used to calculate error.</param>
        /// <param name="testOutput">The test labels that would be used to calculate error.</param>
        public abstract void CalculateTrainingError(List<double[]> testData, List<int> testOutput);
    }
}

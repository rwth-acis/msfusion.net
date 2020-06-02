using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionFramework.Classifiers
{
    class CustomClassifier : IClassifier
    {
        public override void CalculateTrainingError(List<double[]> testData, List<int> testOutput)
        {
            throw new NotImplementedException();
        }

        public override int Classify(List<double> vector)
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

        public override void Train(List<double[]> trainingData, List<int> trainingLabels, bool calculateError = true)
        {
            throw new NotImplementedException();
        }

        public override void Train(List<double[]> trainingData, List<int> trainingLabels, List<double[]> testingData, List<int> testingLabels)
        {
            throw new NotImplementedException();
        }
    }
}

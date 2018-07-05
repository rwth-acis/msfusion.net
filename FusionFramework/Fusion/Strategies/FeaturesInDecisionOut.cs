using FusionFramework.Classifiers;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Data.Transformers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Fusion.Strategies
{
    /// <summary>
    /// The feature in decision out is whenthe inputs are the features from different sensors and the output of the fusion process is a decision such as target class recognition.
    /// B. V. Dasarathy, “Sensor fusion potential exploitation-innovative architectures and illustrative applications,” Proc.IEEE, vol. 85, pp. 24–38, Jan. 1997.
    /// </summary>
    public class FeaturesInDecisionOut : IFusionStrategy
    {
        /// <summary>
        /// Reader to get data from either file or stream.
        /// </summary>
        private IReader DataReader;

        /// <summary>
        /// Instantiate FeaturesInDecisionOut fusion strategy by setting important configurations.
        /// </summary>
        /// <param name="data">Data to be fuse.</param>
        public FeaturesInDecisionOut(List<double> data)
        {
            FeatureVector = data;
            StartReading = Fuse;
            StopReading = Empty;
        }

        /// <summary>
        /// Instantiate FeaturesInDecisionOut fusion strategy by setting the reader.
        /// </summary>
        /// <param name="reader">Reader to be used.</param>
        public FeaturesInDecisionOut(IReader reader)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            StartReading = DataReader.Start;
            StopReading = DataReader.Stop;
        }

        /// <summary>
        /// Instantiate FeaturesInDecisionOut fusion strategy by setting the reader.
        /// </summary>
        /// <param name="reader">Reader to be used.</param>
        /// <param name="classifier">Classifier to be use.</param>
        public FeaturesInDecisionOut(IReader reader, IClassifier classifier)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            StartReading = DataReader.Start;
            StopReading = DataReader.Stop;
            Classifier = classifier;
        }

        /// <summary>
        /// Instantiate FeaturesInDecisionOut fusion strategy by providing other fusion strategies whose output would be used as an input for this fusion.
        /// </summary>
        /// <param name="fusionStrategies"></param>
        /// <param name="classifier">Classifier to be use.</param>
        public FeaturesInDecisionOut(List<IFusionStrategy> fusionStrategies, IClassifier classifier)
        {
            Classifier = classifier;
            FusionStrategies = fusionStrategies;
            FusionStrategies.ForEach((IFusionStrategy fusionStrategy) =>
            {
                fusionStrategy.OnFusionFinished = ((dynamic output) =>
                {
                    if (IFusionStrategyFinished >= FusionStrategies.Count)
                    {
                        if (FusionStrategies.Count == 1)
                        {
                            FeatureVector = output;
                        }
                        Fuse();
                    }
                    else
                    {
                        FeatureVector.AddRange(new List<double>(output));
                        IFusionStrategyFinished++;
                    }
                });
            });
        }

        /// <summary>
        /// Start fusion process by calling configured delegate
        /// </summary>
        public override void Start() => StartReading();

        /// <summary>
        /// Stop fusion process by calling configured delegate
        /// </summary>
        public override void Stop() => StopReading();

        /// <summary>
        /// Fuse the data in to features
        /// </summary>
        public void Fuse()
        {
            PreProcess(ref Data);
            double Decision = Classifier.Classify(FeatureVector);
            OnFusionFinished(Decision);
        }

        /// <summary>
        /// When the reader completes reading.
        /// </summary>
        /// <param name="data">Data that has been read.</param>
        private void OnReadFinished(dynamic data)
        {
            Data = data;
            Fuse();
        }
    }
}

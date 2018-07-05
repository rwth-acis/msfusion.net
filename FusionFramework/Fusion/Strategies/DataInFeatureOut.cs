using FusionFramework.Core.Data.Reader;
using FusionFramework.Data.Transformers;
using FusionFramework.Features;
using System.Collections.Generic;

namespace FusionFramework.Fusion.Strategies
{
    /// <summary>
    /// The data in feature out is when data from different sensors (or different bands of the same sensor) are combined to derive some form ofa feature ofthe object in the environment or a descriptor ofthe phenomenon under observation.
    /// B. V. Dasarathy, “Sensor fusion potential exploitation-innovative architectures and illustrative applications,” Proc.IEEE, vol. 85, pp. 24–38, Jan. 1997.
    /// </summary>
    public class DataInFeatureOut : IFusionStrategy
    {
        /// <summary>
        /// Reader to get data from either file or stream.
        /// </summary>
        private IReader DataReader;

        /// <summary>
        /// Features to be calculated.
        /// </summary>
        private List<IFeature> Features;

        private FeatureManager FeatureManager;

        /// <summary>
        /// Instantiate DataInFeaturOut fusion strategy by setting important configurations.
        /// </summary>
        /// <param name="data">Data to be fuse.</param>
        public DataInFeatureOut(List<double[]> data)
        {
            Data = data;
            StartReading = Fuse;
            StopReading = Empty;
        }

        /// <summary>
        /// Instantiate DataInFeaturOut fusion strategy by setting the reader.
        /// </summary>
        /// <param name="reader">Reader to be used.</param>
        public DataInFeatureOut(IReader reader)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            StartReading = DataReader.Start;
            StopReading = DataReader.Stop;
        }

        /// <summary>
        /// Instantiate DataInFeaturOut fusion strategy by setting the reader.
        /// </summary>
        /// <param name="reader">Reader to be used.</param>
        /// <param name="features">Features to be calculated.</param>
        public DataInFeatureOut(IReader reader, IFeature[] features)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            StartReading = DataReader.Start;
            StopReading = DataReader.Stop;
            Features = new List<IFeature>(features);
        }

        /// <summary>
        /// Instantiate DataInFeaturOut fusion strategy by providing other fusion strategies whose output would be used as an input for this fusion.
        /// </summary>
        /// <param name="fusionStrategies"></param>
        public DataInFeatureOut(List<IFusionStrategy> fusionStrategies)
        {
            FusionStrategies = fusionStrategies;
            FusionStrategies.ForEach((IFusionStrategy fusionStrategy) =>
            {
                fusionStrategy.OnFusionFinished = ((dynamic output) =>
                {
                    if (IFusionStrategyFinished == FusionStrategies.Count)
                    {
                        Fuse();
                    }
                    else
                    {
                        Data.AddRange(new List<double[]>(output));
                        IFusionStrategyFinished++;
                    }
                });
            });                      
        }

        /// <summary>
        /// Add features to the list
        /// </summary>
        /// <param name="feature">Feature to be added</param>
        public void Add(IFeature feature)
        {
            Features.Add(feature);
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

            FeatureManager = new FeatureManager();
            List<double> NewFeatures = FeatureManager.Generate(Data, Features);
            PostProcess(ref Data);

            OnFusionFinished(NewFeatures);
        }

        /// <summary>
        /// When the reader completes reading.
        /// </summary>
        /// <param name="data">Data that has been read.</param>
        private void OnReadFinished(dynamic data)
        {
            Data = new List<double[]>(data);
            Fuse();
        }
    }
}

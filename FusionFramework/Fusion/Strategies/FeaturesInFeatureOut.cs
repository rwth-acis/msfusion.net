using FusionFramework.Core;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Data.Transformers;
using FusionFramework.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Fusion.Strategies
{
    /// <summary>
    /// The feature in feature out is when both the input and output of the fusion process are features. Accordingly, this has been commonly referred to as feature fusion.
    /// B. V. Dasarathy, “Sensor fusion potential exploitation-innovative architectures and illustrative applications,” Proc.IEEE, vol. 85, pp. 24–38, Jan. 1997.
    /// </summary>
    public class FeaturesInFeatureOut : IFusionStrategy
    {
        /// <summary>
        /// Reader to get data from either file or stream.
        /// </summary>
        private IReader DataReader;

        private CoreBuffer<double[]> FeatureBuffer;

        /// <summary>
        /// Instantiate FeaturesInFeatureOut fusion strategy by setting important configurations.
        /// </summary>
        /// <param name="data">Data to be fuse.</param>
        public FeaturesInFeatureOut(List<double[]> data)
        {
            Data = data;
            StartReading = Fuse;
            StopReading = Empty;
            FeatureBuffer = new CoreBuffer<double[]>(OnBufferFinished, 1);
        }

        /// <summary>
        /// Instantiate FeaturesInFeatureOut fusion strategy by setting the reader.
        /// </summary>
        /// <param name="reader">Reader to be used.</param>
        public FeaturesInFeatureOut(IReader reader)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            StartReading = DataReader.Start;
            StopReading = DataReader.Stop;
            FeatureBuffer = new CoreBuffer<double[]>(OnBufferFinished, 1);
        }

        /// <summary>
        /// Instantiate FeaturesInFeatureOut fusion strategy by providing other fusion strategies whose output would be used as an input for this fusion.
        /// </summary>
        /// <param name="fusionStrategies"></param>
        public FeaturesInFeatureOut(List<IFusionStrategy> fusionStrategies)
        {
            FeatureBuffer = new CoreBuffer<double[]>(OnBufferFinished, fusionStrategies.Count);
            FusionStrategies = fusionStrategies;
            int TmpIndex = 0;
            FusionStrategies.ForEach((IFusionStrategy fusionStrategy) =>
            {
                fusionStrategy.Id = TmpIndex;
                TmpIndex++;
                fusionStrategy.OnFusionFinished = ((dynamic output) =>
                {
                    FeatureBuffer.Push(((List<double>)output).ToArray(), fusionStrategy.Id);

                    /*if (IFusionStrategyFinished == FusionStrategies.Count)
                    {
                        if(FusionStrategies.Count == 1)
                        {
                            FeatureVector = output;
                        }
                        Fuse();
                    }
                    else
                    {
                        FeatureVector.AddRange(output);
                        IFusionStrategyFinished++;
                    }*/
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
            PostProcess(ref FeatureVector);
            OnFusionFinished?.Invoke(FeatureVector);
            FeatureVector = new List<double>();
            IFusionStrategyFinished = 0;
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

        private void OnBufferFinished(dynamic data)
        {
            Data = (List<double[]>) data;
            Data.ForEach(vector =>
            {
                FeatureVector.AddRange(vector);
            });
            Fuse();
        }
    }
}

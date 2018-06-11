using FusionFramework.Core.Data.Reader;
using FusionFramework.Data.Transformers;
using FusionFramework.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Fusion.Strategies
{
    class FeaturesInFeatureOut : IFusionStrategy
    {
        private IReader DataReader;

        public FeaturesInFeatureOut(List<double[]> data)
        {
            Data = data;
            ExecutionMethod = Fuse;
        }

        public FeaturesInFeatureOut(IReader reader)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            ExecutionMethod = DataReader.Start;
        }

        public FeaturesInFeatureOut(List<IFusionStrategy> fusionStrategies)
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
                        FeatureVector.AddRange(new List<double>(output));
                        IFusionStrategyFinished++;
                    }
                });
            });
        }

        public override void Start() => ExecutionMethod();

        public void Fuse()
        {
            PreProcess(ref Data);

            List<double> NewFeatures = new List<double>();

            foreach(double[] TmpData in Data)
            {
                NewFeatures.AddRange(new List<double>(TmpData));
            }

            PostProcess(ref Data);

            OnFusionFinished(NewFeatures);
        }

        private void OnReadFinished(dynamic data)
        {
            Data = data;
            Fuse();
        }
    }
}

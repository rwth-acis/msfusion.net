using FusionFramework.Core.Data.Reader;
using FusionFramework.Data.Transformers;
using FusionFramework.Features;
using System.Collections.Generic;

namespace FusionFramework.Fusion.Strategies
{
    class DataInFeatureOut : IFusionStrategy
    {

        private IReader DataReader;
        private List<IFeature> Features;
        private FeatureManager FeatureManager;

        public DataInFeatureOut(List<double[]> data)
        {
            Data = data;
            ExecutionMethod = Fuse;
        }

        public DataInFeatureOut(IReader reader)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            ExecutionMethod = DataReader.Start;
        }

        public DataInFeatureOut(IReader reader, IFeature[] features)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            ExecutionMethod = DataReader.Start;
        }

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

        public void Add(IFeature feature)
        {
            Features.Add(feature);
        }

        public override void Start() => ExecutionMethod();

        public void Fuse()
        {
            PreProcess(ref Data);

            FeatureManager = new FeatureManager();
            List<double> NewFeatures = FeatureManager.Generate(Data, Features);

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

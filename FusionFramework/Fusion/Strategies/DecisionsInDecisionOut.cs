using FusionFramework.Classifiers;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Data.Transformers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Fusion.Strategies
{
    class DecisionsInDecisionOut : IFusionStrategy
    {
        private IReader DataReader;

        public DecisionsInDecisionOut(double[] decisions)
        {
            FeatureVector = new List<double>(decisions);
            ExecutionMethod = Fuse;
        }

        public DecisionsInDecisionOut(IReader reader)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            ExecutionMethod = DataReader.Start;
        }

        public DecisionsInDecisionOut(IReader reader, IClassifier classifier)
        {
            DataReader = reader;
            DataReader.OnReadFinished = OnReadFinished;
            ExecutionMethod = DataReader.Start;
            Classifier = classifier;
        }

        public DecisionsInDecisionOut(List<IFusionStrategy> fusionStrategies)
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


        public override void Start()
        {
            ExecutionMethod();
        }

        public void Fuse()
        {
            PreProcess(ref Data);
            double Decision = Classifier.Classify(FeatureVector);
            OnFusionFinished(Decision);
        }

        private void OnReadFinished(dynamic data)
        {
            Data = data;
            Fuse();
        }
    }
}

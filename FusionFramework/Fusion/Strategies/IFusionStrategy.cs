using FusionFramework.Classifiers;
using FusionFramework.Core;
using FusionFramework.Data.Transformers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Fusion.Strategies
{
    delegate void ExecutionMethodDelegate();
    delegate void FusionFinished(dynamic Output);
    abstract class IFusionStrategy : Transformable
    {
        protected List<double[]> Data;
        protected List<double> FeatureVector;

       
        protected ExecutionMethodDelegate ExecutionMethod;
        protected IClassifier Classifier;

        protected List<IFusionStrategy> FusionStrategies;
        protected int IFusionStrategyFinished;

        public FusionFinished OnFusionFinished;
        
        abstract public void Start();

    }
}

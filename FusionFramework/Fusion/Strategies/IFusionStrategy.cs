using FusionFramework.Classifiers;
using FusionFramework.Core;
using FusionFramework.Data.Transformers;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Classes for fusion strategies to compensate data, feature and decision level fusion.
/// </summary>
namespace FusionFramework.Fusion.Strategies
{
    /// <summary>
    /// Delegate to methods that would start and stop fusion process.
    /// </summary>
    public delegate void ExecutionMethodDelegate();

    /// <summary>
    /// Delegate for fusion finished event.
    /// </summary>
    /// <param name="Output"></param>
    public delegate void FusionFinished(dynamic Output);

    /// <summary>
    /// Abstract class that every fusion strategy must inherit.
    /// </summary>
    public abstract class IFusionStrategy : Transformable
    {
        /// <summary>
        /// Data to be fused
        /// </summary>
        protected List<double[]> Data;

        /// <summary>
        /// Features to be fused
        /// </summary>
        protected List<double> FeatureVector = new List<double>();

        /// <summary>
        /// Start the fusion process by reading the data.
        /// </summary>
        protected ExecutionMethodDelegate StartReading;

        /// <summary>
        /// Stop the fusion process.
        /// </summary>
        protected ExecutionMethodDelegate StopReading;

        /// <summary>
        /// Classifier that would be used for decision level fusion and feature level fusion.
        /// </summary>
        protected IClassifier Classifier;

        /// <summary>
        /// Client application can provide other fusion strategies as an input which then will be executed in tree like structure.
        /// </summary>
        protected List<IFusionStrategy> FusionStrategies;

        /// <summary>
        /// Number of fusion strategies that are finished already.
        /// </summary>
        protected int IFusionStrategyFinished = 0;

        /// <summary>
        /// Event handler for fusion process completion.
        /// </summary>
        public FusionFinished OnFusionFinished;
        
        /// <summary>
        /// Start Fusion Processs
        /// </summary>
        abstract public void Start();

        /// <summary>
        /// Stop Fusion Processs
        /// </summary>
        abstract public void Stop();

        /// <summary>
        /// Empty method that is used logically in different fusion strategies.
        /// </summary>
        public void Empty()
        {

        }

    }
}

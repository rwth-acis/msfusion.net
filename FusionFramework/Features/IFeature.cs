using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Package for all the time domain and the frequency domain features.
/// </summary>
namespace FusionFramework.Features
{
    public enum FeatureFlavour
    {
        MatrixInValueOut,
        MatrixInVectorOut,
        VectorInValueOut,
        VectorInVectorOut,

    };

    public enum CorrelationAlgorithm
    {
        Pearson,
        WeightedPearson,
        Spearman

    };

    /// <summary>
    /// Abstract class that every feature must override.
    /// </summary>
    public abstract class IFeature
    {
        public FeatureFlavour Flavour = FeatureFlavour.VectorInValueOut;

        public bool ReturnsArray;

        public int[] UseColumns;

        /// <summary>
        /// Calculate feature.
        /// </summary>
        /// <param name="data">Data to be calculated.</param>
        /// <returns></returns>
        public abstract dynamic Calculate(dynamic data);

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Analysis;
using Accord.Statistics.Kernels;

/// <summary>
/// Reducers are used for dimensionality reduction
/// </summary>
namespace FusionFramework.Data.Transformers.Reducers
{
    /// <summary>
    /// Kernel Principle Component Ananlysis is a dimensionality reduction algorithm.
    /// </summary>
    public class KernelPCAReducer : IDataTransformer
    {
        /// <summary>
        /// KernelPCA classifier
        /// </summary>
        KernelPrincipalComponentAnalysis KernePCAClassifier;

        /// <summary>
        /// Instantiate KernalPCA classifier.
        /// </summary>
        public KernelPCAReducer()
        {
            KernePCAClassifier = new KernelPrincipalComponentAnalysis(new Linear(), PrincipalComponentMethod.Center);
        }

        /// <summary>
        /// Apply dimensionality reduction to provided matrix.
        /// </summary>
        /// <param name="data">Matrix to be reduced.</param>
        public override void Transform(ref List<double[]> data)
        {
            data = new List<double[]>(KernePCAClassifier.Transform(data.ToArray()));
        }

        /// <summary>
        /// Apply dimensionality reduction to provided vector.
        /// </summary>
        /// <param name="data">Vector to be reduced.</param>
        public override void Transform(ref List<double> data)
        {
            data = new List<double>(KernePCAClassifier.Transform(data.ToArray()));
        }

        /// <summary>
        /// Apply dimensionality reduction to provided int vector.
        /// </summary>
        /// <param name="data">Vector to be reduced.</param>
        public override void Transform(ref List<int> data)
        {
            
        }
    }
}

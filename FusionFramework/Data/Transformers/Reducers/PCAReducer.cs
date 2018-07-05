using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Analysis;

namespace FusionFramework.Data.Transformers.Reducers
{
    /// <summary>
    /// Principle Component Ananlysis is a dimensionality reduction algorithm.
    /// </summary>
    public class PCAReducer : IDataTransformer
    {
        /// <summary>
        /// PCA classifier
        /// </summary>
        PrincipalComponentAnalysis PCAClassifier;

        /// <summary>
        /// Instantiate PCA classifier.
        /// </summary>
        public PCAReducer()
        {
            PCAClassifier = new PrincipalComponentAnalysis()
            {
                Method = PrincipalComponentMethod.Center,
                Whiten = true
            };

        }

        /// <summary>
        /// Apply dimensionality reduction to provided matrix.
        /// </summary>
        /// <param name="data">Matrix to be reduced.</param>
        public override void Transform(ref List<double[]> data)
        {
            data = new List<double[]>(PCAClassifier.Transform(data.ToArray()));
        }

        /// <summary>
        /// Apply dimensionality reduction to provided vector.
        /// </summary>
        /// <param name="data">Vector to be reduced.</param>
        public override void Transform(ref List<double> data)
        {
            data = new List<double>(PCAClassifier.Transform(data.ToArray()));
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

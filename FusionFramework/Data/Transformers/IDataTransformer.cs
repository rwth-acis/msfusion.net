using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Filters;

/// <summary>
/// Transformers are the operation that transform data from one state to other.
/// </summary>
namespace FusionFramework.Data.Transformers
{
    /// <summary>
    /// Abstract class that every transformer should extend.
    /// </summary>
    public abstract class IDataTransformer
    {
        /// <summary>
        /// Transform a matrix.
        /// </summary>
        /// <param name="data">Matrix to be transformed.</param>
        abstract public void Transform(ref List<double[]> data);

        /// <summary>
        /// Transform a vector.
        /// </summary>
        /// <param name="data">Vector to be transformed.</param>
        abstract public void Transform(ref List<double> data);

        /// <summary>
        /// Transform a vector of integrer
        /// </summary>
        /// <param name="data">Vector to be transformed.</param>
        abstract public void Transform(ref List<int> data);

    }
}

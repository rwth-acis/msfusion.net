using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Filters;
using Accord.Math;
using System.Linq;

namespace FusionFramework.Data.Transformers.Common
{
    /// <summary>
    /// Normalizes the data.
    /// </summary>
    public class Normalize : IDataTransformer
    {
        /// <summary>
        /// Normalizes a matrix.
        /// </summary>
        /// <param name="data">Matrix to be normalize.</param>
        public override void Transform(ref List<double[]> data)
        {
            data = data.Select(x => x = Accord.Math.Matrix.Normalize(x)).ToList<double[]>();
        }

        /// <summary>
        /// Normalizes a vector.
        /// </summary>
        /// <param name="data">Matrix to be normalize.</param>
        public override void Transform(ref List<double> data)
        {
            data = Accord.Math.Matrix.Normalize(data.ToArray()).ToList<double>();
        }

        /// <summary>
        /// Normalizes an int vector.
        /// </summary>
        /// <param name="data">Vector to be normalize.</param>
        public override void Transform(ref List<int> data)
        {

        }
    }
}

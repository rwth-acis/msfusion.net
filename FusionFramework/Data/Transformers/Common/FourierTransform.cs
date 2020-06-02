using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

/// <summary>
/// Common Transformers
/// </summary>
namespace FusionFramework.Data.Transformers.Common
{
    // TODO: Improve FFT implementation

    /// <summary>
    /// Fourier Transform
    /// </summary>
    public class FourierTransform : IDataTransformer
    {

        /// <summary>
        /// Transform time domain signal to frequency domain signal.
        /// </summary>
        /// <param name="data">Signal to be transformed.</param>
        /// <returns></returns>
        public Complex[,] Transform(List<double[]> data)
        {
            Complex[][] ComplexArray = data.ConvertAll<Complex[]>(new Converter<double[], Complex[]>(DoubleToComplex)).ToArray();
            Complex[,] ComplexMatrix = new Complex[ComplexArray.GetLength(0), ComplexArray.GetLength(1)];
            for (int i = 0; i < ComplexArray.GetLength(0); i++)
            {
                for (int j = 0; j < ComplexArray.GetLength(0); i++)
                {
                    ComplexMatrix[i, j] = ComplexArray[i][j];
                }
            }
            Accord.Math.FourierTransform.FFT2(ComplexMatrix, Accord.Math.FourierTransform.Direction.Forward);
            return ComplexMatrix;
        }

        public override void Transform(ref List<double[]> data)
        {
            
        }

        public override void Transform(ref List<double> data)
        {

        }

        public override void Transform(ref List<int> data)
        {

        }

        /// <summary>
        /// Type casting from double vector to complex vector.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Complex[] DoubleToComplex(double[] input)
        {
            return Accord.Math.ComplexMatrix.ToComplex(input);
        }
    }
}

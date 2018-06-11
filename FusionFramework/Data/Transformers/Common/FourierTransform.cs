using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FusionFramework.Data.Transformers.Common
{
    // TODO: Improve FFT implementation

    class FourierTransform
    {
        public Complex[,] Transform(ref List<double[]> data)
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

        private Complex[] DoubleToComplex(double[] input)
        {
            return Accord.Math.ComplexMatrix.ToComplex(input);
        }
    }
}

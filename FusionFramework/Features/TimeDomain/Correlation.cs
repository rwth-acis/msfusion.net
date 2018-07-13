using System;
using System.Collections.Generic;
using System.Text;
using Accord.Math;

namespace FusionFramework.Features.TimeDomain
{
    delegate void CorrelationMethod(double[] x);

    public class Correlation : IFeature
    {
        int X, Y;

        public Correlation(int x, int y)
        {
            X = x;
            Y = y;
            Flavour = FeatureFlavour.MatrixInValueOut;            
        }

        public override dynamic Calculate(dynamic window)
        {
            return Pearson(((double[][])window).GetColumn<double>(0), ((double[][])window).GetColumn<double>(0));
        }

        public double Pearson(double[] x, double[] y)
        {
            return MathNet.Numerics.Statistics.Correlation.Pearson(x, y);
        }

        public double WeightedPearson(double[] x, double[] y, double[] weights)
        {
            return MathNet.Numerics.Statistics.Correlation.WeightedPearson(x, y, weights);
        }

        public double CalculatePearson(double[] x, double[] y)
        {
            return MathNet.Numerics.Statistics.Correlation.Pearson(x, y);
        }

        public double CalculateSpearman(double[] x, double[] y)
        {
            return MathNet.Numerics.Statistics.Correlation.Spearman(x, y);
        }
    }
}

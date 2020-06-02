using System;
using System.Collections.Generic;
using System.Text;
using Accord.Math;

namespace FusionFramework.Features.Complex
{
    public class AverageTimeBetweenPeaks : IFeature
    {
        double[] TimeStamps;
        int TimeIndex;

        public AverageTimeBetweenPeaks(int timeIndex)
        {
            Flavour = FeatureFlavour.MatrixInVectorOut;
            TimeIndex = timeIndex;
        }

        public AverageTimeBetweenPeaks(int timeIndex, params int[] useColumns)
        {
            Flavour = FeatureFlavour.MatrixInVectorOut;
            UseColumns = useColumns;
            TimeIndex = timeIndex;
        }

        public override dynamic Calculate(dynamic data)
        {
            double[][] Array = data;
            TimeStamps = Array.GetColumn<double>(TimeIndex);
            List<double> Output = new List<double>();

            foreach (var col in UseColumns)
            {
                Output.Add(Calculate(Array.GetColumn<double>(col)));
            }
            return Output;
        }

        private double Calculate(double[] data)
        {
            int[] Peaks = Accord.Audio.Tools.FindPeaks(data);
            List<double> differece = new List<double>();
            foreach (var col in Peaks)
            {
                if (col != 0)
                    differece.Add(TimeStamps[col] - TimeStamps[col - 1]);
            }
            return Accord.Statistics.Measures.Mean(differece.ToArray());
        }
    }
}

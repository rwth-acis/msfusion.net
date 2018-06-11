using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.Complex
{
    class AverageTimeBetweenPeaks : IFeature
    {
        int PeakIndex;
        public AverageTimeBetweenPeaks(int index)
        {
            PeakIndex = index;
        }

        public override dynamic Calculate(dynamic data)
        {
            int[] Peaks = Accord.Audio.Tools.FindPeaks(data);
            double[] differece = new double[Peaks.Length];
            for (int p = 1; p < Peaks.Length; p++)
            {
                differece[p] = data[p] - data[p - 1];
            }
            return Accord.Statistics.Measures.Mean(differece);
        }
    }
}

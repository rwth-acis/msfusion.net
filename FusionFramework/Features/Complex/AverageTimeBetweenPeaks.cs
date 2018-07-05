using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.Complex
{
    public class AverageTimeBetweenPeaks : IFeature
    {
        double[] TimeStamps;
        public AverageTimeBetweenPeaks(double[] timeIndex)
        {
            TimeStamps = timeIndex;
        }

        public AverageTimeBetweenPeaks(double[] timeIndex, int[] useColumns)
        {
            UseColumns = useColumns;
            TimeStamps = timeIndex;
        }

        public override dynamic Calculate(dynamic data)
        {
            int[] Peaks = Accord.Audio.Tools.FindPeaks(data);
            List<double> differece = new List<double>();
            foreach(var col in Peaks)
            {
                if(col != 0)
                differece.Add(TimeStamps[col] - TimeStamps[col - 1]);
            }
            return Accord.Statistics.Measures.Mean(differece.ToArray());
        }
    }
}

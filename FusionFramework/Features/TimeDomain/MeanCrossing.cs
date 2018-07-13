using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math;

namespace FusionFramework.Features.TimeDomain
{
    public class MeanCrossing : FusionFramework.Features.IFeature
    {
        public MeanCrossing()
        {

        }

        public MeanCrossing(params int[] columns)
        {
            UseColumns = columns;
        }

        public override dynamic Calculate(dynamic data)
        {
            double[] Array = data;
            double Mean = Accord.Statistics.Measures.Mean(Array);
            List<double> x1 = Array.Get(2, Array.Count()).Where(x => x > Mean).ToList<double>();
            var x2 = Array.Get(1, Array.Count() - 1).Where(x => x <= Mean).ToArray<double>();
            x1.AddRange(x2);
            return x1.Sum();
        }
    }
}

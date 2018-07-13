using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math;

namespace FusionFramework.Features.TimeDomain
{
    public class ZeroCrossing : IFeature
    {
        public ZeroCrossing()
        {

        }

        public ZeroCrossing(params int[] columns)
        {
            UseColumns = columns;
        }

        public override dynamic Calculate(dynamic data)
        {
            double[] Array = data;
            List<double> x1 = Array.Get(2, Array.Count()).Where(x => x > 0).ToList<double>();
            var x2 = Array.Get(1, Array.Count() - 1).Where(x => x <= 0).ToArray<double>();
            x1.AddRange(x2);
            return x1.Sum();
        }
    }
}

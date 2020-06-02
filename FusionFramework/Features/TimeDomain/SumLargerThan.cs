using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math;
using System.Text;
using System.Threading.Tasks;

namespace FusionFramework.Features.TimeDomain
{
    public class SumLargerThan : IFeature
    {
        double Bound;

        public SumLargerThan(double value)
        {
            Bound = value;
        }

        public SumLargerThan(double value, params int[] columns)
        {
            Bound = value;
            UseColumns = columns;
        }

        public override dynamic Calculate(dynamic data)
        {
            double[] Array = data;
            List<double> x1 = Array.Where(x => x > Bound).ToList<double>();
            return x1.Sum();
        }
    }
}

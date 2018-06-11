using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FusionFramework.Features.Complex
{
    class HjorthParameters : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Execute(data);
        }

        public double[] Execute(double[] data)
        {

            double[] datap = data.Select(x => Math.Pow(x, 2)).ToArray();
            double[] datax = data.Zip(data.Skip(1), (a, b) => Math.Pow(b - a, 2)).ToArray();
            double[] dataxx = datax.Zip(datax.Skip(1), (a, b) => Math.Pow(b - a, 2)).ToArray();

            double mx2 = Accord.Statistics.Measures.Mean(datap);
            double mx3 = Accord.Statistics.Measures.Mean(datax);
            double mx4 = Accord.Statistics.Measures.Mean(dataxx);

            double mob = mx3 / mx2;
            return new double[] { Math.Sqrt(mob), Math.Sqrt(mx4 / mx3 - mob) };
        }
    }
}

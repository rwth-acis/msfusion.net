using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Visualizations;
using System.Linq;

namespace FusionFramework.Features.Complex
{
    class BinDistribution : IFeature
    {
        int BinCount;
        public BinDistribution(int binCount)
        {
            BinCount = binCount;
        }

        public override dynamic Calculate(dynamic data)
        {
            var Hist = new Histogram();
            Hist.Compute(data, BinCount);
            return Hist.ToArray().Select(x => (double)x).ToArray();
        }
    }
}

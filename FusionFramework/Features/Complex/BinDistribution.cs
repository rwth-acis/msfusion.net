using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Visualizations;
using System.Linq;

namespace FusionFramework.Features.Complex
{
    public class BinDistribution : IFeature
    {
        int BinCount;
        public BinDistribution(int binCount)
        {
            Flavour = FeatureFlavour.VectorInVectorOut;
            ReturnsArray = true;
            BinCount = binCount;
        }
        public BinDistribution(int binCount, int[] useColumns)
        {
            Flavour = FeatureFlavour.VectorInVectorOut;
            ReturnsArray = true;
            UseColumns = useColumns;
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

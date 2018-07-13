using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FusionFramework.Features.Complex
{
    public class HjorthParameters : IFeature
    {
        public HjorthParameters()
        {
            Flavour = FeatureFlavour.VectorInVectorOut;
        }

        public HjorthParameters(params int[] useColumns)
        {
            Flavour = FeatureFlavour.VectorInVectorOut;
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Execute(data);
        }

        public double[] Execute(double[] data)
        {
            var dxV = data.Zip(data.Skip(1), (x, y) => y - x).ToList();
            dxV.Insert(0, data[0]);
            var ddxV = dxV.Zip(dxV.Skip(1), (x, y) => y - x).ToList();
            ddxV.Insert(0, dxV[0]);

            double mx2 = Accord.Statistics.Measures.Mean(data.Select(x => Math.Pow(x, 2)).ToArray());
            double mdx2 = Accord.Statistics.Measures.Mean(dxV.Select(x => Math.Pow(x, 2)).ToArray());
            double mddx2 = Accord.Statistics.Measures.Mean(ddxV.Select(x => Math.Pow(x, 2)).ToArray());

            double mob = mdx2 / mx2;
            return new double[] { Math.Sqrt(mob), Math.Sqrt(mddx2 / mdx2 - mob) };
        }
    }
}

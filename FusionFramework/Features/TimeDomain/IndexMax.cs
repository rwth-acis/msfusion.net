using System;
using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class IndexMax : IFeature
    {
        public IndexMax()
        {

        }
        public IndexMax(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            var DoubleData = ((double[])data);
            return Array.IndexOf(DoubleData, DoubleData.Max());
        }
    }
}

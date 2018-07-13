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
            return Array.IndexOf(data, data.Max());
        }
    }
}

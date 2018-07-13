using System;
using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class IndexMin : IFeature
    {
        public IndexMin()
        {

        }
        public IndexMin(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Array.IndexOf(data, data.Min());
        }
    }
}

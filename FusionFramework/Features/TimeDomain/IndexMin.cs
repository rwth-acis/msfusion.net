using System;
using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class IndexMin : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Array.IndexOf(data, data.Min());
        }
    }
}

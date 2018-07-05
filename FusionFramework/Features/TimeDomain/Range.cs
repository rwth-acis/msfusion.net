using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class Range : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return data.Max() - data.Min();
        }
    }
}

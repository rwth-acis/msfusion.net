using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class Sum : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return data.Sum();
        }
    }
}

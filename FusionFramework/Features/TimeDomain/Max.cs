using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class Max : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return data.Max();
        }
    }
}

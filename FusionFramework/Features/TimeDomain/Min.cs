using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class Min : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return ((double[])data).Min();

        }
    }
}

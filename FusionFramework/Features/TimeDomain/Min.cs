using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class Min : IFeature
    {
        public Min()
        {
        }

        public Min(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return ((double[])data).Min();

        }
    }
}

using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class Max : IFeature
    {
        public Max()
        {
        }

        public Max(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return ((double[]) data).Max();
        }
    }
}

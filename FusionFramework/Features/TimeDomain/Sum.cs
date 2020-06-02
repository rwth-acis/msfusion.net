using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class Sum : IFeature
    {
        public Sum()
        {

        }
        public Sum(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return ((double[])data).Sum();
        }
    }
}

using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class Range : IFeature
    {

        public Range()
        {

        }
        public Range(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            var DoubleData = ((double[])data);
            return DoubleData.Max() - DoubleData.Min();
        }
    }
}

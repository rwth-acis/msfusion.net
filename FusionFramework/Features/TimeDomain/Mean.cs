using System.Linq;
namespace FusionFramework.Features.TimeDomain
{
    public class Mean : IFeature
    {
        public Mean()
        {
            Flavour = FeatureFlavour.VectorInValueOut;
        }
        public Mean(int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Mean(data);
        }
    }
}

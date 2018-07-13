namespace FusionFramework.Features.TimeDomain
{
    public class Variance : IFeature
    {
        public Variance()
        {

        }
        public Variance(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Variance(data);
        }
    }
}

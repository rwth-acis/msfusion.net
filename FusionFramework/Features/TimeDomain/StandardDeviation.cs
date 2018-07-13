namespace FusionFramework.Features.TimeDomain
{
    public class StandardDeviation : IFeature
    {
        public StandardDeviation()
        {
        }
        public StandardDeviation(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Circular.StandardDeviation(data);
        }
    }
}

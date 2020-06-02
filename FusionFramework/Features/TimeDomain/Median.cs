namespace FusionFramework.Features.TimeDomain
{
    public class Median : IFeature
    {
        public Median()
        {
            
        }

        public Median(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Circular.Median(data);
        }
    }
}

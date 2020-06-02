namespace FusionFramework.Features.TimeDomain
{
    public class Skewness : IFeature
    {
        public Skewness()
        {

        }

        public Skewness(params int[] columns)
        {
            UseColumns = columns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Skewness(data);
        }
    }
}

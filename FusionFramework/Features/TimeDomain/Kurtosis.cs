namespace FusionFramework.Features.TimeDomain
{
    public class Kurtosis : IFeature
    {
        public Kurtosis()
        {

        }

        public Kurtosis(params int[] columns)
        {
            UseColumns = columns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Kurtosis(data);
        }
    }
}

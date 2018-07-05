namespace FusionFramework.Features.TimeDomain
{
    public class Skewness : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Skewness(data);
        }
    }
}

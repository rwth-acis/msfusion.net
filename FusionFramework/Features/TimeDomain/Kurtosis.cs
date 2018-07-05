namespace FusionFramework.Features.TimeDomain
{
    public class Kurtosis : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Kurtosis(data);
        }
    }
}

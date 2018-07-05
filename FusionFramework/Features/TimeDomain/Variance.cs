namespace FusionFramework.Features.TimeDomain
{
    public class Variance : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Variance(data);
        }
    }
}

namespace FusionFramework.Features.FrequencyDomain
{
    public class Median : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Circular.Median(data);
        }
    }
}

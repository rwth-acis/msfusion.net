namespace FusionFramework.Features.TimeDomain
{
    public class InterquartileRange : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.UpperQuartile(data) - Accord.Statistics.Measures.LowerQuartile(data);
        }
    }
}

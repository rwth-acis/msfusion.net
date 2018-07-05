namespace FusionFramework.Features.TimeDomain
{
    public class Mode : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Mode(data);
        }
    }
}

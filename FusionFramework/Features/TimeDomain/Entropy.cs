namespace FusionFramework.Features.TimeDomain
{
    public class Entropy : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Entropy(data);
        }        
    }
}

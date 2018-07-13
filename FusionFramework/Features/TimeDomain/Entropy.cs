namespace FusionFramework.Features.TimeDomain
{
    public class Entropy : IFeature
    {
        public Entropy()
        {

        }

        public Entropy(params int[] columns)
        {
            UseColumns = columns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return MathNet.Numerics.Statistics.StreamingStatistics.Entropy(data);
        }        
    }
}

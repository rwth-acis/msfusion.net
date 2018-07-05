namespace FusionFramework.Features.TimeDomain
{
    public class Percentile : IFeature
    {
        int Percentage;

        public Percentile(int percentile)
        {
            Percentage = percentile;
        }

        public override dynamic Calculate(dynamic data)
        {
            return MathNet.Numerics.Statistics.Statistics.Percentile(data, Percentage);
        }
    }
}

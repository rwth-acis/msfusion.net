namespace FusionFramework.Features.TimeDomain
{
    public class Magnitude : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Math.Norm.Euclidean(data);
        }
    }
}

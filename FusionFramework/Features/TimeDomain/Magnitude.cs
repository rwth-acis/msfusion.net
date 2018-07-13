namespace FusionFramework.Features.TimeDomain
{
    public class Magnitude : IFeature
    {
        public Magnitude()
        {

        }

        public Magnitude(params int[] columns)
        {
            UseColumns = columns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Accord.Math.Norm.Euclidean(data);
        }
    }
}

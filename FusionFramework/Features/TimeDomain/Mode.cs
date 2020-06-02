namespace FusionFramework.Features.TimeDomain
{
    public class Mode : IFeature
    {
        public Mode()
        {

        }

        public Mode(params int[] columns)
        {
            UseColumns = columns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Mode(data);
        }
    }
}

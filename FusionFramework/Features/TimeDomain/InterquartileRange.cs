namespace FusionFramework.Features.TimeDomain
{
    public class InterquartileRange : IFeature
    {
        public InterquartileRange()
        {

        }

        public InterquartileRange(params int[] columns)
        {
            UseColumns = columns;
        }

        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.UpperQuartile(data) - Accord.Statistics.Measures.LowerQuartile(data);
        }
    }
}

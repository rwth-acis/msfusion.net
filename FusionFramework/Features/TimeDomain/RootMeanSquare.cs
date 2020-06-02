using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class RootMeanSquare : IFeature
    {
        public RootMeanSquare()
        {

        }
        public RootMeanSquare(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            double[] TempData = data;
            return Accord.Audio.Tools.RootMeanSquare(TempData.Select(x => (float)x).ToArray());
        }
    }
}

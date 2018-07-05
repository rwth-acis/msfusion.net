using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    public class RootMeanSquare : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            double[] TempData = data;
            return Accord.Audio.Tools.RootMeanSquare(TempData.Select(x => (float)x).ToArray());
        }
    }
}

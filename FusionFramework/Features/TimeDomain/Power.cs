using System.Linq;
using System;

namespace FusionFramework.Features.TimeDomain
{
    public class Power : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            double[] TempData = data;
            return Math.Pow(Accord.Audio.Tools.RootMeanSquare(TempData.Select(x => (float)x).ToArray()), 2);
        }
    }
}

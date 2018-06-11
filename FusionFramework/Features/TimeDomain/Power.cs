using System;
using System.Collections.Generic;
using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    class Power : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            double[] tmp = (double[])data;
            return Math.Pow(Accord.Audio.Tools.RootMeanSquare(tmp.Select(x => (float)x).ToArray()), 2);
        }
    }
}

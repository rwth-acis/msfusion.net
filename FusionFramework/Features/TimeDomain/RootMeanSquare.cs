using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    class RootMeanSquare : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            double[] tmp = (double[])data;
            return Accord.Audio.Tools.RootMeanSquare(tmp.Select(x => (float)x).ToArray());
        }
    }
}

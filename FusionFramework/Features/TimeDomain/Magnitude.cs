using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.TimeDomain
{
    class Magnitude : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Math.Norm.Euclidean(data);
        }
    }
}

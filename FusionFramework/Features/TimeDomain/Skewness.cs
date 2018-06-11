using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.TimeDomain
{
    class Skewness : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.Skewness(data);
        }
    }
}

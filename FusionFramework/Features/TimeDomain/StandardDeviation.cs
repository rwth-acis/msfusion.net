using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.TimeDomain
{
    class StandardDeviation : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Circular.StandardDeviation(data);
        }
    }
}

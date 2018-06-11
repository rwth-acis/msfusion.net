using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.FrequencyDomain
{
    class Median : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Circular.Median(data);
        }
    }
}

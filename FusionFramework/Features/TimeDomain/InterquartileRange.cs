using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.TimeDomain
{
    class InterquartileRange : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Accord.Statistics.Measures.UpperQuartile(data) - Accord.Statistics.Measures.LowerQuartile(data);
        }
    }
}

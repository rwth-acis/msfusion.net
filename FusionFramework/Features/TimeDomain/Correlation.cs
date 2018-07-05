using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.TimeDomain
{
    public class Correlation : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return 0;//Accord.Statistics.Measures.Correlation(data);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.Complex
{
    class MeanAbsoluteDeviation : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            double MAD = 0.0;
            double Mean = Accord.Statistics.Measures.Mean(data);
            foreach(double Item in data)
            {
                MAD += Item - Mean;
            }
            return MAD;
        }
    }
}

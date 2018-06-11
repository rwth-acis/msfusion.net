using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.TimeDomain
{
    class Max : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return data.Max();
        }
    }
}

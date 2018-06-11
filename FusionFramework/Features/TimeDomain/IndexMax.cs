using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FusionFramework.Features.TimeDomain
{
    class IndexMax : IFeature
    {
        public override dynamic Calculate(dynamic data)
        {
            return Array.IndexOf(data, data.Max());
        }
    }
}

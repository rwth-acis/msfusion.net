using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features
{
    abstract class IFeature
    {
        public abstract dynamic Calculate(dynamic data);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionFramework.Features.TimeDomain
{
    class MyCustomFeature : IFeature
    {
        public MyCustomFeature()
        {

        }

        public MyCustomFeature(params int[] useColumns)
        {
            UseColumns = useColumns;
        }

        public override dynamic Calculate(dynamic data)
        {
            throw new NotImplementedException();
        }
    }
}

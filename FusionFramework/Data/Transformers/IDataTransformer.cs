using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Filters;

namespace FusionFramework.Data.Transformers
{
    abstract class IDataTransformer
    {
        abstract public void Transform(ref List<double[]> data);
        

       

        public void Transform(ref List<double> data)
        {

        }

        public void Transform(ref List<int> data)
        {

        }

    }
}

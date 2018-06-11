using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Filters;

namespace FusionFramework.Data.Transformers.Common
{
    class Normalize : IDataTransformer
    {
        public override void Transform(ref List<double[]> data)
        {        
            Normalization Normalizer = new Normalization();
            data = new List<double[]>(Normalizer.Apply(data.ToArray()));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Analysis;
using Accord.Statistics.Kernels;

namespace FusionFramework.Data.Transformers.Reducers
{
    class KernelPCAReducer : IDataTransformer
    {
        KernelPrincipalComponentAnalysis KernePCAClassifier;

        public KernelPCAReducer()
        {
            KernePCAClassifier = new KernelPrincipalComponentAnalysis(new Linear(), PrincipalComponentMethod.Center);
        }

        public override void Transform(ref List<double[]> data)
        {
            data = new List<double[]>(KernePCAClassifier.Transform(data.ToArray()));
        }
    }
}

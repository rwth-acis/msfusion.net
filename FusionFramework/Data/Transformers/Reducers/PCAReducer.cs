using System;
using System.Collections.Generic;
using System.Text;
using Accord.Statistics.Analysis;

namespace FusionFramework.Data.Transformers.Reducers
{
    class PCAReducer : IDataTransformer
    {
        PrincipalComponentAnalysis PCAClassifier;

        public PCAReducer()
        {
            PCAClassifier = new PrincipalComponentAnalysis()
            {
                Method = PrincipalComponentMethod.Center,
                Whiten = true
            };

        }

        public override void Transform(ref List<double[]> data)
        {
            data = new List<double[]>(PCAClassifier.Transform(data.ToArray()));
        }
    }
}

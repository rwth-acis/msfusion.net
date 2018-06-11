using Accord.Statistics.Models.Fields.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features
{
    class FeatureManager
    {
        public List<double> Generate(List<double[]> data, List<IFeature> features)
        {
            List<double> FeatureVector = new List<double>();
            data.ForEach((double[] row) =>
            {
                features.ForEach((IFeature Feature) =>
                {
                    FeatureVector.Add(Feature.Calculate(row));
                });
            });
            return FeatureVector;
        }

        public List<double> Generate(double[] data, List<IFeature> features)
        {
            List<double> FeatureVector = new List<double>();
            features.ForEach((IFeature Feature) =>
            {
                FeatureVector.Add(Feature.Calculate(data));
            });
            return FeatureVector;
        }
    }
}

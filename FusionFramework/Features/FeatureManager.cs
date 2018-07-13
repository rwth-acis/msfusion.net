using Accord.Math;
using Accord.Statistics.Models.Fields.Features;
using FusionFramework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features
{

    public class FeatureManager : Transformable
    {
        List<IFeature> Features = new List<IFeature>();

        public static List<double> Generate(List<double[]> data, List<IFeature> features)
        {
            
            List<double> FeatureVector = new List<double>();
            double[][] Array = data.ToArray();
            features.ForEach((IFeature Feature) =>
            {
                if (Feature.UseColumns == null)
                {
                    Feature.UseColumns = FeatureManager.GetIndexFromRange(0, Array[0].Length, 1);
                }

                switch (Feature.Flavour)
                {
                    case FeatureFlavour.MatrixInValueOut:
                        FeatureVector.Add(Feature.Calculate(Array));
                        break;
                    case FeatureFlavour.MatrixInVectorOut:
                        FeatureVector.AddRange(Feature.Calculate(Array));
                        break;
                    case FeatureFlavour.VectorInVectorOut:
                        foreach (var col in Feature.UseColumns)
                        {
                            FeatureVector.AddRange(Feature.Calculate(Array.GetColumn<double>(col)));
                        }
                        break;
                    default:
                        foreach (var col in Feature.UseColumns)
                        {
                            FeatureVector.Add(Feature.Calculate(Array.GetColumn<double>(col)));
                        }
                        break;
                }
            });
            return FeatureVector;
        }

        public List<double> Generate(List<double[]> data)
        {
            PreProcess(ref data);
            var Tmp = Generate(data, Features);
            PostProcess(ref Tmp);
            return Tmp;
        }

        public static List<double> Generate(double[] data, List<IFeature> features)
        {
            List<double> FeatureVector = new List<double>();
            features.ForEach((IFeature Feature) =>
            {
                FeatureVector.Add(Feature.Calculate(data));
            });
            return FeatureVector;
        }

        public List<double> Generate(double[] data)
        {
            return Generate(data, Features);
        }

        public static int[] GetIndexFromRange(int start, int end, int increment)
        {
            List<int> Indices = new List<int>();
            for (int i = start; i < end; i += increment)
            {
                Indices.Add(i);
            }
            return Indices.ToArray();
        }

        public void Add(IFeature feature)
        {
            Features.Add(feature);
        }
        
    }
}

using FusionFramework.Data.Transformers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Core
{
    abstract class Transformable
    {
        protected List<IDataTransformer> PreProcessors;
        protected List<IDataTransformer> PostProcessors;

        public void Add(IDataTransformer dataTransformer, bool isPreProcessor)
        {
            if (isPreProcessor)
            {
                PreProcessors.Add(dataTransformer);
            }
            else
            {
                PostProcessors.Add(dataTransformer);
            }
        }

        public void PreProcess(ref List<double[]> Data)
        {
            List<double[]> tmp = Data;
            PreProcessors.ForEach(delegate (IDataTransformer transformer)
            {
                transformer.Transform(ref tmp);
            });
            Data = tmp;
        }

        public void PostProcess(ref List<double[]> Data)
        {
            List<double[]> tmp = Data;
            PostProcessors.ForEach(delegate (IDataTransformer transformer)
            {
                transformer.Transform(ref tmp);
            });
            Data = tmp;
        }
    }
}

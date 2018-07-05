using FusionFramework.Data.Transformers;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Core operations of the framework.
/// </summary>
namespace FusionFramework.Core
{
    /// <summary>
    /// All the objects that can perform data transformation should extend this class.
    /// </summary>
    public abstract class Transformable
    {
        /// <summary>
        /// Gets or sets list of data Pre-processing operations.
        /// </summary>
        protected List<IDataTransformer> PreProcessors;

        /// <summary>
        /// Gets or sets list of data Post-processing operations.
        /// </summary>
        protected List<IDataTransformer> PostProcessors;

        /// <summary>
        /// Instantiate transformable class.
        /// </summary>
        public Transformable()
        {
            PreProcessors = new List<IDataTransformer>();
            PostProcessors = new List<IDataTransformer>();
        }

        /// <summary>
        /// Adds pre or post processing operations to the queue. 
        /// </summary>
        /// <param name="dataTransformer">Transformation operation.</param>
        /// <param name="isPreProcessor">Is a pre-processor or post-processor</param>
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

        /// <summary>
        /// Pre-processing a matrix.
        /// </summary>
        /// <param name="Data">Matrix to be pre-processed.</param>
        public void PreProcess(ref List<double[]> Data)
        {
            if(PreProcessors.Count > 0)
            {
                List<double[]> tmp = Data;
                PreProcessors.ForEach(delegate (IDataTransformer transformer)
                {
                    transformer.Transform(ref tmp);
                });
                Data = tmp;
            }
        }

        /// <summary>
        /// Pre-processing a vector
        /// </summary>
        /// <param name="Data">Vector to be pre-processed.</param>
        public void PreProcess(ref List<double> Data)
        {
            if (PreProcessors.Count > 0)
            {
                List<double> tmp = Data;
                PreProcessors.ForEach(delegate (IDataTransformer transformer)
                {
                    transformer.Transform(ref tmp);
                });
                Data = tmp;
            }
        }

        /// <summary>
        /// Post-processing a matrix.
        /// </summary>
        /// <param name="Data">Matrix to be post-processed.</param>
        public void PostProcess(ref List<double[]> Data)
        {
            if (PostProcessors.Count > 0)
            {
                List<double[]> tmp = Data;
                PostProcessors.ForEach(delegate (IDataTransformer transformer)
                {
                    transformer.Transform(ref tmp);
                });
                Data = tmp;
            }
        }

        /// <summary>
        /// Post-processing a vector
        /// </summary>
        /// <param name="Data">Vector to be post-processed.</param>
        public void PostProcess(ref List<double> Data)
        {
            if (PostProcessors.Count > 0)
            {
                List<double> tmp = Data;
                PostProcessors.ForEach(delegate (IDataTransformer transformer)
                {
                    transformer.Transform(ref tmp);
                });
                Data = tmp;
            }
        }
    }
}

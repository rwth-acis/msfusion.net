using Accord.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Data.Transformers.Filters
{
    /// <summary>
    /// HighPass Filter
    /// </summary>
    public class HighPassFilter : IDataTransformer
    {
        float Alpha;
        int SampleRate;

        /// <summary>
        /// Instantiate filter with parameters
        /// </summary>
        /// <param name="alpha">Alpha</param>
        /// <param name="sampleRate">Sample rate for the fitler</param>
        public HighPassFilter(float alpha, int sampleRate)
        {
            Alpha = alpha;
            SampleRate = sampleRate;
        }

        /// <summary>
        /// Apply envelope filter to provided matrix.
        /// </summary>
        /// <param name="data">Matrix to be filtered.</param>
        public override void Transform(ref List<double[]> data)
        {
            List<Signal> Signals = new List<Signal>();
            data.ForEach((Item) =>
            {
                Signals.Add(Accord.Audio.Signal.FromArray(Item, SampleRate));
            });
            var HPFilter = new Accord.Audio.Filters.HighPassFilter(Alpha);
            Signals = new List<Signal>(HPFilter.Apply(Signals.ToArray()));
            List<double[]> Output = new List<double[]>();
            Signals.ForEach((Item) =>
            {
                Output.Add(Item.ToDouble());
            });
            data = Output;
        }

        /// <summary>
        /// Apply envelope filter to provided vector.
        /// </summary>
        /// <param name="data">Vector to be filtered.</param>
        public override void Transform(ref List<double> data)
        {
            
        }

        /// <summary>
        /// Apply envelope filter to provided int vector.
        /// </summary>
        /// <param name="data">Vector to be filtered.</param>
        public override void Transform(ref List<int> data)
        {

        }
    }
}

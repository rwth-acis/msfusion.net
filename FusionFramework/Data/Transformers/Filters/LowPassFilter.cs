using Accord.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Data.Transformers.Filters
{
    /// <summary>
    /// LowPass Filter
    /// </summary>
    public class LowPassFilter : IDataTransformer
    {
        float Alpha;
        int SampleRate;

        /// <summary>
        /// Instantiate filter with parameters
        /// </summary>
        /// <param name="alpha">Alpha</param>
        /// <param name="sampleRate">Sample rate for the fitler</param>
        public LowPassFilter(float alpha, int sampleRate)
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
                Signals.Add(Signal.FromArray(Item, SampleRate));
            });
            var LPFilter = new Accord.Audio.Filters.LowPassFilter(Alpha);
            Signals = new List<Signal>(LPFilter.Apply(Signals.ToArray()));
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

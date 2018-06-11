using Accord.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Data.Transformers.Filters
{
    class EnvelopeFilter : IDataTransformer
    {
        float Alpha;
        int SampleRate;

        public EnvelopeFilter(float alpha, int sampleRate)
        {
            Alpha = alpha;
            SampleRate = sampleRate;
        }
        public override void Transform(ref List<double[]> data)
        {
            List<Signal> Signals = new List<Signal>();
            data.ForEach((Item) =>
            {
                Signals.Add(Signal.FromArray(Item, SampleRate));
            });
            var Filter = new Accord.Audio.Filters.EnvelopeFilter(Alpha);
            Signals = new List<Signal>(Filter.Apply(Signals.ToArray()));
            List<double[]> Output = new List<double[]>();
            Signals.ForEach((Item) =>
            {
                Output.Add(Item.ToDouble());
            });
            data = Output;
        }

    }
}

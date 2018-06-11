using Accord.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Data.Transformers.Filters
{
    class LowPassFilter : IDataTransformer
    {
        float Alpha;
        int SampleRate;

        public LowPassFilter(float alpha, int sampleRate)
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
            var LPFilter = new Accord.Audio.Filters.LowPassFilter(Alpha);
            Signals = new List<Signal>(LPFilter.Apply(Signals.ToArray()));
            List<double[]> Output = new List<double[]>();
            Signals.ForEach((Item) =>
            {
                Output.Add(Item.ToDouble());
            });
            data = Output;
        }

    }
}

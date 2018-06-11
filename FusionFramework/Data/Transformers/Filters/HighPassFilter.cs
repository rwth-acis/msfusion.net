using Accord.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Data.Transformers.Filters
{
    class HighPassFilter : IDataTransformer
    {
        float Alpha;
        int SampleRate;

        public HighPassFilter(float alpha, int sampleRate)
        {
            Alpha = alpha;
            SampleRate = sampleRate;
        }
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
    }
}

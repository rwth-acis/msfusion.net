using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Features.TimeDomain
{
    public class Energy : IFeature
    {
        int SampleRate;

        public Energy(int sampleRate)
        {
            SampleRate = sampleRate;
        }
        public Energy(int sampleRate, params int[] useColumns)
        {
            UseColumns = useColumns;
            SampleRate = sampleRate;
        }

        public override dynamic Calculate(dynamic data)
        {
            Accord.Audio.Signal Signal = Accord.Audio.Signal.FromArray(data, SampleRate);
            return Signal.GetEnergy();
        }
    }
}

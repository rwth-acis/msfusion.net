using System;
using System.Collections.Generic;
using System.Text;
using FusionFramework.Features;
using FusionFramework.Data.Segmentators;

namespace FusionFramework.VirtualSensor
{
    public class Magetometer : IVirtualSensor
    {
        public Magetometer(string mqttURL, int windowSize, int overlap)
        {
            MqttUrl = mqttURL;
            Features = new IFeature[]
            {
                new FusionFramework.Features.TimeDomain.Mean()
            };

            Window = new SlidingWindow<double[]>(windowSize, overlap);
        }
    }
}

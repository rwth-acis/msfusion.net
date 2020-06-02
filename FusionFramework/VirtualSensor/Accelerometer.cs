using System;
using System.Collections.Generic;
using System.Text;
using FusionFramework.Features;
using FusionFramework.Data.Segmentators;

namespace FusionFramework.VirtualSensor
{
    public class Accelerometer : IVirtualSensor
    {
        public Accelerometer(string mqttURL, int windowSize, int overlap)
        {
            MqttUrl = mqttURL;
            Features = new IFeature[]
            {
                new FusionFramework.Features.TimeDomain.Mean()
            };

            Window = new SlidingWindow<double[]>(windowSize, overlap);
        }

        public Accelerometer(string mqttURL, int windowSize, int overlap, IFeature[] features)
        {
            MqttUrl = mqttURL;
            Features = features;
            Window = new SlidingWindow<double[]>(windowSize, overlap);
        }
    }
}

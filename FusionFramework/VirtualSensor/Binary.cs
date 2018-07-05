using Accord.Statistics.Models.Fields.Features;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Features;
using FusionFramework.Data.Segmentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionFramework.VirtualSensor
{
    public class Binary : IVirtualSensor
    {
        public Binary(string mqttURL, int windowSize, int overlap)
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

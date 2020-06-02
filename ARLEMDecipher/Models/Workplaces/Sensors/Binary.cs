using Accord.Statistics.Models.Fields.Features;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Features;
using FusionFramework.Transformer.Data;
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

            Window = new SlidingWindow(windowSize, overlap);
        }

        public VirtualSnesorConfiguration GetConfiguration()
        {
            var vSensor = new VirtualSnesorConfiguration();
            vSensor.Reader = new MQTTReader(MqttUrl, Window);
            vSensor.Features = Features;
            return vSensor;
        }
    }
}

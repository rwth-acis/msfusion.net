using Accord.Statistics.Models.Fields.Features;
using FusionFramework.Core.Data.Reader;
using FusionFramework.Features;
using FusionFramework.Data.Segmentators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.VirtualSensor
{
    public struct VirtualSnesorConfiguration {
        public MQTTReader<double[]> Reader;
        public IFeature[] Features;
    };


    public abstract class IVirtualSensor
    {
        protected string MqttUrl;
        protected IFeature[] Features;
        protected SlidingWindow<double[]> Window;

        

        public VirtualSnesorConfiguration GetConfiguration()
        {
            var vSensor = new VirtualSnesorConfiguration();
            vSensor.Reader = new MQTTReader<double[]>(MqttUrl, Window);
            vSensor.Features = Features;
            return vSensor;
        }
    }


}

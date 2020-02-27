using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARLEMDecipher.Models.Workplaces.Configurables
{
    [Serializable()]
    public class Configurable
    {
        [XmlArray("apps")]
        [XmlArrayItem("app", typeof(App))]
        [JsonProperty("app")]
        public List<App> Apps { get; set; }

        [XmlArray("devices")]
        [XmlArrayItem("device", typeof(Device))]
        [JsonProperty("device")]
        public List<Device> Devices { get; set; }
    }
}

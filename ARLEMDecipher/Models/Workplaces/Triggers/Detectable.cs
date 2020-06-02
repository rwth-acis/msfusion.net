using ARLEMDecipher.Models.Common;
using ARLEMDecipher.Models.Workplaces.Sensors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARLEMDecipher.Models.Workplaces.Triggers
{
    [Serializable()]
    public class Detectable : IWorkplaceItem
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int ID { get; set; }

        [XmlElement("sensor")]
        [JsonProperty("sensor")]
        public int Sensor { get; set; }

        [XmlAttribute("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [XmlAttribute("url")]
        [JsonProperty("url")]
        public string Url { get; set; }

        public Asset Asset { get; set; }

        [XmlElement("author")]
        [JsonProperty("author")]
        public Author Author { get; set; }
    }
}

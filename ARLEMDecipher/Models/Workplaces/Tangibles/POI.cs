using ARLEMDecipher.Models.Common;
using ARLEMDecipher.Models.Workplaces.Tangibles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARLEMDecipher.Models.Workplaces
{
    [Serializable()]
    public class POI : IWorkplaceItem
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int ID { get; set; }

        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlAttribute("x")]
        [JsonProperty("x")]
        public string X { get; set; }

        [XmlAttribute("y")]
        [JsonProperty("y")]
        public string Y { get; set; }

        [XmlAttribute("z")]
        [JsonProperty("z")]
        public string Z { get; set; }

    }
}

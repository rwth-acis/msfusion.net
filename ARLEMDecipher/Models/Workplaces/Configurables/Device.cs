using ARLEMDecipher.Models.Common;
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
    public class Device : IWorkplaceItem
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int InternalID { get; set; }

        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("type")]
        [JsonProperty("type")]
        public DeviceType Type { get; set; }

        [XmlElement("Author")]
        [JsonProperty("author")]
        public Author Author { get; set; }
    }
}

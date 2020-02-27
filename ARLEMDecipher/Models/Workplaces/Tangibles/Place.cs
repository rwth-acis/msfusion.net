using ARLEMDecipher.Models.Common;
using ARLEMDecipher.Models.Workplaces.Triggers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARLEMDecipher.Models.Workplaces.Tangibles
{
    [Serializable()]
    public class Place : IWorkplaceItem
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int InertnalID { get; set; }

        [XmlAttribute("id_name")]
        [JsonProperty("id_name")]
        public string ID { get; set; }

        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("detectable")]
        [JsonProperty("detectable")]
        public Detectable Detectable { get; set; }

        [XmlElement("author")]
        [JsonProperty("author")]
        public Author Author { get; set; }

    }
}

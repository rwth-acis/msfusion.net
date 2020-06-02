using ARLEMDecipher.Models.Common;
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
    public class Primitive : IWorkplaceItem
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int InertnalID { get; set; }

        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("type")]
        [JsonProperty("type")]
        public PrimitiveTypes Type { get; set; }

        [XmlAttribute("symbol")]
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [XmlAttribute("url")]
        [JsonProperty("url")]
        public string URL { get; set; }

        [XmlAttribute("size")]
        [JsonProperty("size")]
        public double Size { get; set; }

        [XmlElement("author")]
        [JsonProperty("author")]
        public Author Author { get; set; }
    }
}

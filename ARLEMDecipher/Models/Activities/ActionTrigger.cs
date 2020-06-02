using ARLEMDecipher.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARLEMDecipher.Models.Activities
{
    [Serializable()]
    public class ActionTrigger
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int ID { get; set; }

        [XmlElement("mode")]
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [XmlAttribute("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [XmlAttribute("value")]
        [JsonProperty("value")]
        public string Value { get; set; }

        [XmlAttribute("removeSelf")]
        [JsonProperty("removeSelf")]
        public int RemoveSelf { get; set; }
       
        [XmlAttribute("entityId")]
        [JsonProperty("entityId")]
        public int Entity { get; set; }

        [XmlAttribute("modular")]
        [JsonProperty("modular")]
        public Module Modular { get; set; }

        [XmlAttribute("entityType")]
        [JsonProperty("entityType")]
        public string EntityType { get; set; }      

        [XmlAttribute("option")]
        [JsonProperty("option")]
        public string Options { get; set; }     

        [JsonProperty("viewport")]
        public ViewPort ViewPort { get; set; }
    }
}

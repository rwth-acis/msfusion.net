using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ARLEMDecipher.Models.Common;
using ARLEMDecipher.Models.Workplaces;
using Newtonsoft.Json;

namespace ARLEMDecipher.Models.Activities
{
    [Serializable()]
    public class Activity
    {
        [XmlElement("id")]
        [JsonProperty("id")]
        public int ID { get; set; }

        [XmlElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [XmlElement("language")]
        [JsonProperty("language")]
        public string Language { get; set; }

        [XmlElement("start")]
        [JsonProperty("start")]
        public int StartAction { get; set; }

        [XmlElement("author")]
        [JsonProperty("author")]
        public Author Author { get; set; }

        [XmlArray("actions")]
        [XmlArrayItem("action", typeof(Action))]
        [JsonProperty("actions")]
        public List<Action> Actions { get; set; }
    }
}

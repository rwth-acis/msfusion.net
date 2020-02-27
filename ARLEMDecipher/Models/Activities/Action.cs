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
    public class Action
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int ID { get; set; }

        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [XmlAttribute("instructionTitle")]
        [JsonProperty("instructionTitle")]
        public string InstructionTitle { get; set; }

        [XmlAttribute("instructionDescription")]
        [JsonProperty("instructionDescription")]
        public string InstructionDescription { get; set; }

        [XmlElement("author")]
        [JsonProperty("author")]
        public Author Author { get; set; }

        [XmlArray("triggers")]
        [XmlArrayItem("trigger", typeof(ActionTrigger))]
        [JsonProperty("actionTriggers")]
        public List<ActionTrigger> Triggers { get; set; }
    }
}

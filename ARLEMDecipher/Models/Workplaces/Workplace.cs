using ARLEMDecipher.Models.Workplaces.Configurables;
using ARLEMDecipher.Models.Workplaces.Tangibles;
using ARLEMDecipher.Models.Workplaces.Triggers;
using ARLEMDecipher.Models.Activities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARLEMDecipher.Models.Workplaces.Sensors;
using System.Xml.Serialization;
using ARLEMDecipher.Models.Common;

namespace ARLEMDecipher.Models.Workplaces
{
    [Serializable()]
    public class Workplace
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int InternalID { get; set; }

        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("tangibles")]
        [JsonProperty("tangibles")]
        public Tangible Tangibles { get; set; }

        [XmlElement("configurables")]
        [JsonProperty("configurables")]
        public Configurable Configurables { get; set; }

        [XmlElement("triggers")]
        [JsonProperty("triggers")]
        public Trigger Triggers { get; set; }

        [XmlElement("activities")]
        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }

        [XmlElement("sensors")]
        [JsonProperty("sensors")]
        public List<VirtualSensor> Sensors { get; set; }

        [XmlElement("author")]
        [JsonProperty("author")]
        public Author Author { get; set; }
    }
}

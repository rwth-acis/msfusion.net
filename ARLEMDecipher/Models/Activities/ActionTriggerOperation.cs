using ARLEMDecipher.Models.Common;
using ARLEMDecipher.Models.Workplaces;
using ARLEMDecipher.Models.Workplaces.Sensors;
using ARLEMDecipher.Models.Workplaces.Triggers;
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
    public class ActionTriggerOperation
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int InertnalID { get; set; }

        [XmlAttribute("is_active")]
        [JsonProperty("is_active")]
        public string IsActivate { get; set; }

        [XmlAttribute("entityId")]
        [JsonProperty("entityId")]
        public int Entity { get; set; }

        [XmlAttribute("entityType")]
        [JsonProperty("entityType")]
        public string EntityType { get; set; }

        [XmlElement("predicate")]
        [JsonProperty("predicate")]
        public Predicate Predicate { get; set; }

        [XmlAttribute("options")]
        [JsonProperty("options")]
        public string Options { get; set; }


        [JsonProperty("sensor")]
        public VirtualSensor Sensor { get; set; }

        [JsonProperty("viewport")]
        public ViewPort ViewPort { get; set; }
    }
}

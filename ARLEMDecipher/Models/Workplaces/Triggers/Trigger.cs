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
    public class Trigger
    {
        [XmlArray("detectables")]
        [XmlArrayItem("detectable", typeof(Detectable))]
        [JsonProperty("detectable")]
        public List<Detectable> Detectables { get; set; }

        [XmlArray("hazards")]
        [XmlArrayItem("hazard", typeof(Hazard))]
        [JsonProperty("hazard")]
        public List<Hazard> Hazards { get; set; }

        [XmlArray("predicates")]
        [XmlArrayItem("predicate", typeof(Predicate))]
        [JsonProperty("predicate")]
        public List<Predicate> Predicates { get; set; }

        [XmlArray("warnings")]
        [XmlArrayItem("warning", typeof(Warning))]
        [JsonProperty("warning")]
        public List<Warning> Warnings { get; set; }
    }
}

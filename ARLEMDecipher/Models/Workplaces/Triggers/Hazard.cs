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
    public class Hazard : IWorkplaceItem
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int InertnalID { get; set; }

        [XmlAttribute("content")]
        [JsonProperty("content")]
        public string Content { get; set; }

        public ViewPort ViewPort { get; set; }

        [XmlElement("author")]
        [JsonProperty("author")]
        public Author Author { get; set; }
    }
}

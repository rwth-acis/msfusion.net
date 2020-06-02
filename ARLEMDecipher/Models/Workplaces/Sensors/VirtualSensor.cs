using ARLEMDecipher.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARLEMDecipher.Models.Workplaces.Sensors
{
    [Serializable()]
    public class VirtualSensor : IWorkplaceItem
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public string ID { get; set; }

        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlAttribute("url")]
        [JsonProperty("url")]
        public string URI { get; set; }

       [XmlAttribute("username")]
        [JsonProperty("username")]
        public string UserName { get; set; }

        [XmlAttribute("password")]
        [JsonProperty("password")]
        public string Password { get; set; }

        [XmlElement("author")]
        [JsonProperty("author")]
        public Author Author { get; set; }
    }
}

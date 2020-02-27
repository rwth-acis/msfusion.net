using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARLEMDecipher.Models.Workplaces.Configurables
{
    public class DeviceType
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int InternalID { get; set; }

        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

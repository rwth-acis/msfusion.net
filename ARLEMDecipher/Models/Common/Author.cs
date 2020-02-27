using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARLEMDecipher.Models.Common
{
    [Serializable()]
    public class Author
    {
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public int InertnalID { get; set; }

        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlAttribute("email")]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}

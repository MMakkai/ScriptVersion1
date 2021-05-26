using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ScriptVersion1.Models
{
    [Serializable()]
    public class Login
    {
        [XmlElement]
        public string fnev { get; set; }
        [XmlElement]
        public string email { get; set; }
        [XmlElement]
        public string jelszo { get; set; }
        [XmlAttribute]
        public string accid { get; set; }
        [XmlAttribute]
        public string authlevel { get; set; }
    }
}
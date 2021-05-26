using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ScriptVersion1.Models
{
    [System.Xml.Serialization.XmlRootAttribute("Korhaz")]
    public class Korhaz
    {
        [XmlElement]
        public List<Orvos> orvos { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ScriptVersion1.Models
{
    [System.Xml.Serialization.XmlRootAttribute("felhasznalok")]
    public class Accounts
    {
        [XmlElement]
        public List<Login> felhasznalo { get; set; }
    }
}
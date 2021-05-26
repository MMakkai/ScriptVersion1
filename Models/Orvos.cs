using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ScriptVersion1.Models
{
    [Serializable()]
    public class Orvos
    {
        [Display(Name = "Name")]
        [XmlElement]
        public string nev { get; set; }
        [XmlElement]
        public string demail { get; set; }
        [XmlElement]
        public string beosztas { get; set; }
        [XmlElement]
        public int minugyelet { get; set; }
        [XmlElement]
        public int maxugyelet { get; set; }
        [XmlElement]
        public string ugyelet { get; set; }
        [XmlElement]
        public string szabadsag { get; set; }
        [XmlElement]
        public string tiltott { get; set; }
        [XmlElement]
        public bool szabadnap { get; set; }
        [XmlElement]
        public string szobaszam { get; set; }

    }
}
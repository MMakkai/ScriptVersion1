using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ScriptVersion1.Models
{
    [Serializable()]
    public class Workday
    {
        [XmlIgnore]
        public DateTime workdate { get; set; }

        [XmlElement("workdate")]
        public string workdateString
        {
            get { return this.workdate.ToString("yyyy-MM-dd"); }
            set { this.workdate = DateTime.Parse(value); }
        }

        [XmlIgnore]//XmlArray("OrvosList"),XmlArrayItem(typeof(Orvos), ElementName = "orvos")
        public List<Orvos> workorvos { get; set; }

        [XmlArray("workorvosname")]
        public List<string> workorvosString
        {
            get { return this.workorvos.Select(x => x.nev).ToList(); }
        }

        public Workday(DateTime workdate, List<Orvos> workorvos)
        {
            this.workdate = workdate;
            this.workorvos = workorvos;
        }

        public Workday()
        {
        }
    }
}
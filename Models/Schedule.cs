using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ScriptVersion1.Models
{
    [System.Xml.Serialization.XmlRootAttribute("Schedule")]
    public class Schedule
    {
        [XmlElement]
        public List<Workday> workdays { get; set; }

        public Schedule(List<Workday> workdays)
        {
            this.workdays = workdays;
        }

        public Schedule()
        {
            workdays = null;
        }
    }
}
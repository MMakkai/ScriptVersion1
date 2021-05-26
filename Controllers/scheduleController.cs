using ScriptVersion1.Models;
using ScriptVersion1.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScriptVersion1.Controllers
{
    public class scheduleController : Controller
    {
        // GET: schedule
        public ActionResult ScheduleGeneratePage()
        {
            return View();
        }

        public ActionResult Generate()
        {
            Korhaz korhaz1;
            List<Workday> monthlyschedule = new List<Workday>();
            SerializerHelper.KorhDeserialize(out korhaz1);
            DateTime currentdate = DateTime.Now;

            for (int i = 1; i <= DateTime.DaysInMonth(currentdate.Year, currentdate.Month); i++)
            {
                List<Orvos> workdocs = new List<Orvos>();
                DateTime actualday = new DateTime(currentdate.Year, currentdate.Month, i);
                string[][] testasd = korhaz1.orvos.Select(x => x.ugyelet.Split(',')).ToArray();
                string testasd2 = $"{currentdate.Year}-{currentdate.Month.ToString("D2")}-{i.ToString("D2")}";

                List<Orvos> preferorvos = korhaz1.orvos.Where(x => x.ugyelet.Split(',').Contains($"{currentdate.Year}-{currentdate.Month.ToString("D2")}-{i.ToString("D2")}")).Where(x => !x.szabadsag.Split(',').Contains($"{currentdate.Year}-{currentdate.Month.ToString("D2")}-{i.ToString("D2")}")).ToList();
                List<Orvos> nopreforvos = korhaz1.orvos.Where(x => !x.ugyelet.Split(',').Contains($"{currentdate.Year}-{currentdate.Month.ToString("D2")}-{i.ToString("D2")}")).Where(x => !x.tiltott.Split(',').Contains($"{currentdate.Year}-{currentdate.Month.ToString("D2")}-{i.ToString("D2")}")).Where(x => !x.szabadsag.Split(',').Contains($"{currentdate.Year}-{currentdate.Month.ToString("D2")}-{i.ToString("D2")}")).ToList();
                List<Orvos> tiltottorvos = korhaz1.orvos.Where(x => x.tiltott.Split(',').Contains($"{currentdate.Year}-{currentdate.Month.ToString("D2")}-{i.ToString("D2")}")).Where(x => !x.szabadsag.Split(',').Contains($"{currentdate.Year}-{currentdate.Month.ToString("D2")}-{i.ToString("D2")}")).ToList();

                var prefnomin = preferorvos.Where(x => x.minugyelet > monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).ToList();
                workdocs.AddRange(prefnomin);

                var prefabovemin = preferorvos.Where(x => x.minugyelet <= monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).Where(x => x.maxugyelet > monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).ToList();
                workdocs.AddRange(prefabovemin);


                var noprefnomin = nopreforvos.Where(x => x.minugyelet > monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).ToList();
                workdocs.AddRange(noprefnomin);

                var noprefabovemin = nopreforvos.Where(x => x.minugyelet <= monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).Where(x => x.maxugyelet > monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).ToList();
                workdocs.AddRange(noprefabovemin);


                var tiltottnomin = tiltottorvos.Where(x => x.minugyelet > monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).ToList();
                workdocs.AddRange(tiltottnomin);

                var tiltottabovemin = tiltottorvos.Where(x => x.minugyelet <= monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).Where(x => x.maxugyelet > monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).ToList();
                workdocs.AddRange(tiltottabovemin);


                var noprefabovemax = nopreforvos.Where(x => x.maxugyelet <= monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).ToList();
                workdocs.AddRange(noprefabovemax);

                var tiltottabovemax = tiltottorvos.Where(x => x.maxugyelet <= monthlyschedule.Count(y => y.workorvos.Any(z => z.demail == x.demail))).ToList();
                workdocs.AddRange(tiltottabovemax);

                var rnd = new Random(Guid.NewGuid().GetHashCode());

                monthlyschedule.Add(new Workday(actualday, workdocs.Take(6).OrderBy(x=>rnd.Next()).ToList()));
            }

            SerializerHelper.ScheSerialize(new Schedule(monthlyschedule));

            return RedirectToAction("Index", "doctor", new { area = "" });
        }

        public ActionResult CurrentSchedulePage()
        {
            Schedule currentmonth;

            SerializerHelper.ScheDeserialize(out currentmonth);

            Workday currentday = currentmonth.workdays.Where(x => x.workdate.Equals(DateTime.Now)).FirstOrDefault();

            return View();
        }

    }
}

/*
                     * 1st Layer : Preferált és nincs meg a minimál ügyelet
                     * 2nd Layer : Preferált és megvan a minimál ügyelet viszont nincs meg a maximum
                     * 3rd Layer : Nem Preferált és Nem Tiltott és nincs meg a minimál ügyelet
                     * 4th Layer : Nem Preferált és Nem Tiltott és megvan a minimál ügyelet viszont nincs meg a maximum
                     * 5th Layer : Tiltott és nincs meg a minimál ügyelet
                     * 6th layer : Tiltott és megvan a minimál ügyelet viszont nincs meg a maximum
                     * 7th Layer : Nem Preferált de megvan neki a maximum ügyelet
                     * 8th Layer : Tiltott de megvan neki a maximum ügyelet
                     * 
*/
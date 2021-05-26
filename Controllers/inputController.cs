using ScriptVersion1.Helper;
using ScriptVersion1.Models;
using ScriptVersion1.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace ScriptVersion1.Controllers
{
    public class inputController : Controller
    {
        // GET: Input
        public ActionResult Input()
        {
            var inViewModel = new inputViewModel();

            return View();
        }

        [HttpPost]
        public ActionResult Input(Orvos model)
        {
            Korhaz korhaz1;
            SerializerHelper.KorhDeserialize(out korhaz1);
            korhaz1.orvos.Add(model);
            SerializerHelper.KorhSerialize(korhaz1);

            ModelState.Clear();
            return View();
        }

        public ActionResult DatePicker()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UgyeletPicker(string ugyeletDateData)
        {
            Korhaz korhaz1;
            SerializerHelper.KorhDeserialize(out korhaz1);

            korhaz1.orvos.Where(x => x.demail == (string)Session["Email"]).FirstOrDefault().ugyelet = ugyeletDateData;
            korhaz1.orvos.ForEach(x => x.ugyelet = string.Concat(x.ugyelet.Where(c => !Char.IsWhiteSpace(c))));

            SerializerHelper.KorhSerialize(korhaz1);
            return View(@"~\Views\input\DatePicker.cshtml");
        }
        [HttpPost]
        public ActionResult SzabadsagPicker(string szabadsagDateData)
        {
            Korhaz korhaz1;
            SerializerHelper.KorhDeserialize(out korhaz1);

            korhaz1.orvos.Where(x => x.demail == (string)Session["Email"]). FirstOrDefault().szabadsag = szabadsagDateData;
            korhaz1.orvos.ForEach(x => x.szabadsag = string.Concat(x.szabadsag.Where(c => !Char.IsWhiteSpace(c))));

            SerializerHelper.KorhSerialize(korhaz1);
            return RedirectToAction("DatePicker", "input", new { area = "" });
        }
        [HttpPost]
        public ActionResult TiltottPicker(string tiltottDateData)
        {
            Korhaz korhaz1;
            SerializerHelper.KorhDeserialize(out korhaz1);

            korhaz1.orvos.Where(x => x.demail == (string)Session["Email"]).FirstOrDefault().tiltott = tiltottDateData;
            korhaz1.orvos.ForEach(x => x.tiltott = string.Concat(x.tiltott.Where(c => !Char.IsWhiteSpace(c))));

            SerializerHelper.KorhSerialize(korhaz1);
            return RedirectToAction("DatePicker", "input", new { area = "" });
        }
        public ActionResult UgyeletSzamInput()
        {
            return View();
        }
        public ActionResult UgyeletInput(string demail, int minugyelet, int maxugyelet)
        {
            Korhaz korhaz1;
            SerializerHelper.KorhDeserialize(out korhaz1);

            korhaz1.orvos.Where(x => x.demail == demail).FirstOrDefault().minugyelet = minugyelet;
            korhaz1.orvos.Where(x => x.demail == demail).FirstOrDefault().maxugyelet = maxugyelet;

            SerializerHelper.KorhSerialize(korhaz1);

            return RedirectToAction("UgyeletSzamInput", "input", new { area = "" });
        }
    }
}
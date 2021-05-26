using ScriptVersion1.Models;
using ScriptVersion1.ViewModels;
using ScriptVersion1.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace ScriptVersion1.Controllers
{
    public class orvosController : Controller
    {
        // GET: doctor
        public ActionResult Index()
        { 
            return View();
        }
        public ActionResult GeneralInfo()
        {
            Korhaz korhaz1;
            SerializerHelper.KorhDeserialize(out korhaz1);
            KorhazViewModel oViewModel;

            if (Session["idUser"] == null)
            {
                return RedirectToAction("LoginPage", "login", new { area = ""});
            }
            else if((string)Session["AuthType"] == "1")
            {
                oViewModel = new KorhazViewModel
                {
                    orvoslist = korhaz1.orvos
                };
            }
            else
            {
                oViewModel = new KorhazViewModel
                {
                    orvoslist = korhaz1.orvos.Where(x => x.demail == (string)Session["email"]).ToList()
                };
            }
            return View(oViewModel);
        }
    }
}
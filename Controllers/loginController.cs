using ScriptVersion1.Helper;
using ScriptVersion1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ScriptVersion1.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string jelszo)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string routeDirectory = Path.GetDirectoryName(path);

            if (ModelState.IsValid)
            {
                XElement xelement = XElement.Load(Path.Combine(routeDirectory, "XML\\Accounts.xml"));

                var user = xelement.Elements("felhasznalo").FirstOrDefault(x => x.Descendants("email").FirstOrDefault().Value == email);

                if (user.Descendants("jelszo").FirstOrDefault().Value == jelszo)
                {
                    Session["idUser"] = user.Attribute("accid").Value;
                    Session["AuthType"] = user.Attribute("authlevel").Value;
                    Session["Email"] = email;
                }
            }
            return RedirectToAction("Index", "doctor", new { area = "" });

        }
        public ActionResult LogOut()
        {
            Session["idUser"] = null;
            Session["AuthType"] = null;
            Session["Email"] = null;
            return RedirectToAction("Index", "doctor", new { area = "" });
        }
        public ActionResult RegisterPage()
        {
            return View();
        }
        public ActionResult Register(Login model)
        {
            Accounts users;
            SerializerHelper.AccDeserialize(out users);

            model.accid = Guid.NewGuid().ToString();
            model.authlevel = "0";

            users.felhasznalo.Add(model);

            SerializerHelper.AccSerialize(users);

            return RedirectToAction("Index", "doctor", new { area = "" });
        }
    }
}
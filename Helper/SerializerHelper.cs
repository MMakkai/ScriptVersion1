using ScriptVersion1.Models;
using ScriptVersion1.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;

namespace ScriptVersion1.Helper
{
    public static class SerializerHelper
    {
        static XmlSerializer korhazSerializer = new XmlSerializer(typeof(Korhaz));
        static XmlSerializer scheduleSerializer = new XmlSerializer(typeof(Schedule));
        static XmlSerializer accountSerializer = new XmlSerializer(typeof(Accounts));



        static string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        static UriBuilder uri = new UriBuilder(codeBase);
        static string path = Uri.UnescapeDataString(uri.Path);
        static string routeDirectory = Path.GetDirectoryName(path);

        public static void KorhDeserialize(out Korhaz korhaz)
        {
            using (Stream korhazReader = new FileStream(Path.Combine(routeDirectory, "XML\\Database.xml"), FileMode.Open))
            {
                korhaz = (Korhaz)korhazSerializer.Deserialize(korhazReader);
            }
        }

        public static void KorhSerialize(Korhaz korhaz)
        {
            using (Stream korhazReader = new FileStream(Path.Combine(routeDirectory, "XML\\Database.xml"), FileMode.Create))
            {
                korhazSerializer.Serialize(korhazReader, korhaz);
            }
        }

        public static void ScheDeserialize(out Schedule schedule)
        {
            try
            {
                using (Stream scheduleReader = new FileStream(Path.Combine(routeDirectory, "XML\\Schedule.xml"), FileMode.Open))
                {
                    schedule = (Schedule)scheduleSerializer.Deserialize(scheduleReader);
                }
            }
            catch (Exception)
            {
                schedule = new Schedule();
            }
            
        }

        public static void ScheSerialize(Schedule schedule)
        {
            using (Stream scheduleReader = new FileStream(Path.Combine(routeDirectory, "XML\\Schedule.xml"), FileMode.Create))
            {
                scheduleSerializer.Serialize(scheduleReader, schedule);
            }
        }

        public static void AccDeserialize(out Accounts accounts)
        {
            using (Stream accountReader = new FileStream(Path.Combine(routeDirectory, "XML\\Accounts.xml"), FileMode.Open))
            {
                accounts = (Accounts)accountSerializer.Deserialize(accountReader);
            }
        }

        public static void AccSerialize(Accounts accounts)
        {
            using (Stream accountReader = new FileStream(Path.Combine(routeDirectory, "XML\\Accounts.xml"), FileMode.Create))
            {
                accountSerializer.Serialize(accountReader, accounts);
            }
        }


    }
}
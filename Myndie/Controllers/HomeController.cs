using Myndie.DAO;
using Myndie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myndie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                ApplicationDAO appdao = new ApplicationDAO();
                UpdateNotesDAO updao = new UpdateNotesDAO();
                CartDAO cdao = new CartDAO();
                IList<Application> apps = appdao.ListLast10();
                IList<Application> bapps = appdao.ListTop10();
                IList<UpdateNotes> updates = updao.GetLast3();
                IList<Application> appsinupdates = new List<Application>();
                foreach (var u in updates)
                {
                    if (u.Value.Length > 144)
                    {
                        u.Value = u.Value.Substring(0, 144);
                    }
                    appsinupdates.Add(appdao.SearchById(u.ApplicationId));
                }
                if (Session["Id"] != null)
                {
                    ViewBag.Cart = cdao.SearchCartUser(int.Parse(Session["Id"].ToString()));
                }
                ViewBag.Updates = updates;
                ViewBag.AppsinUpdates = appsinupdates;
                ViewBag.BApps = bapps;
                ViewBag.NApps = apps;
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using Myndie.DAO;
using Myndie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myndie.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
            ApplicationDAO appdao = new ApplicationDAO();
            CartDAO cdao = new CartDAO();
            IList<Application> apps = appdao.ListLast10();
            IList<Application> bapps = appdao.ListTop10();
            if(Session["Id"] != null)
            {
                ViewBag.Cart = cdao.SearchCartUser(int.Parse(Session["Id"].ToString()));
            }            
            ViewBag.BApps = bapps;
            ViewBag.NApps = apps;
            return View();
		}

		public ActionResult About() {
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.Models;
using Myndie.DAO;

namespace Myndie.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            if (Session["ModId"] != null)
            {
                ViewBag.Country = new Country();
                return View();
            }
            return RedirectToAction("../Home/Index");
            
        }

        public ActionResult Validate(Country country)
        {
            if (ModelState.IsValid)
            {
                CountryDAO dao = new CountryDAO();
                dao.Add(country);
                return RedirectToAction("Register");
            }
            ViewBag.Country = country;
            return View("Register");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.DAO;
using Myndie.Models;

namespace Myndie.Controllers
{
    public class PegiController : Controller
    {
        // GET: Pegi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            if (Session["ModId"] != null)
            {
                ViewBag.Pegi = new Pegi();
                ViewBag.Class = "";
                return View();
            }
            return RedirectToAction("../Home/Index");            
        }

        public ActionResult Validate(Pegi pegi)
        {
            if (ModelState.IsValid)
            {
                PegiDAO dao = new PegiDAO();
                dao.Add(pegi);
                return RedirectToAction("Register");
            }
            ViewBag.Class = "alert alert-danger";
            ViewBag.Pegi = pegi;
            return View("Register");
        }
    }
}
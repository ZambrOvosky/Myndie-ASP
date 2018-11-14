using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.DAO;
using Myndie.Models;

namespace Myndie.Controllers
{
    public class TypeAppController : Controller
    {
        // GET: TypeApp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            if (Session["ModId"] != null)
            {
                ViewBag.Type = new TypeApp();
                ViewBag.Class = "";
                return View();
            }
            return RedirectToAction("../Home/Index");            
        }

        public ActionResult Validate(TypeApp typeapp)
        {
            if (ModelState.IsValid)
            {
                TypeAppDAO dao = new TypeAppDAO();
                dao.Add(typeapp);
                return RedirectToAction("Register");
            }
            ViewBag.Lang = typeapp;
            return View("Register");
        }
    }
}
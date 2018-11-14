using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.DAO;
using Myndie.Models;

namespace Myndie.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            if (Session["ModId"] != null)
            {
                ViewBag.Lang = new Language();
                return View();
            }
            return RedirectToAction("../Home/Index");            
        }

        public ActionResult Validate(Language language)
        {
            if (ModelState.IsValid)
            {
                LanguageDAO dao = new LanguageDAO();
                dao.Add(language);
                return RedirectToAction("Register");
            }
            ViewBag.Lang = language;
            return View("Register");
        }
    }
}
using Myndie.DAO;
using Myndie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myndie.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            if (Session["ModId"] != null)
            {
                ViewBag.Genre = new Genre();
                return View();
            }
            return RedirectToAction("../Home/Index");
            
        }

        public ActionResult Validate(Genre genre)
        {
            if (ModelState.IsValid)
            {
                GenreDAO dao = new GenreDAO();
                dao.Add(genre);
                return RedirectToAction("Register");
            }
            ViewBag.Genre= genre;
            return View("Register");
        }
    }
}
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
            if (Session["ModId"] != null)
            {
                PegiDAO dao = new PegiDAO();
                UserDAO udao = new UserDAO();
                User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                ViewBag.User = u;
                ViewBag.Pegi = new Pegi();
                ViewBag.Pegis = dao.List();
                return View();
            }
            return RedirectToAction("../Home/Index");
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
                if (dao.IsUnique(pegi))
                {
                    dao.Add(pegi);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            ViewBag.Pegi = pegi;
            return View("Index");
        }
    }
}
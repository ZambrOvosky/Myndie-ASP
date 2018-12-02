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
            try
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
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Register()
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    ViewBag.Pegi = new Pegi();
                    ViewBag.Class = "";
                    return View();
                }
                return RedirectToAction("../Home/Index");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Validate(Pegi pegi)
        {
            try
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
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    PegiDAO dao = new PegiDAO();
                    UserDAO udao = new UserDAO();
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.User = u;
                    ViewBag.Pegi = dao.SearchById(id);
                    return View();
                }
                else
                {
                    return RedirectToAction("../Home/Index");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditConfirm(Pegi pegi)
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    PegiDAO dao = new PegiDAO();
                    Pegi p = dao.SearchById(pegi.Id);
                    p.Age = pegi.Age;
                    p.Desc = pegi.Desc;
                    p.PhotoPath = pegi.PhotoPath;
                    dao.Update();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("../Home/Index");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Remove(int id)
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    PegiDAO dao = new PegiDAO();
                    Pegi p = dao.SearchById(id);
                    dao.Remove(p);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("../Home/Index");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
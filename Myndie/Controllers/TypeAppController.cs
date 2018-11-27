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
        //public ActionResult Index()
        //{
        //    return View();
        //}

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

        public ActionResult Index()
        {
            if (Session["ModId"] != null)
            {
                TypeAppDAO dao = new TypeAppDAO();
                UserDAO udao = new UserDAO();
                ModeratorDAO mdao = new ModeratorDAO();
                ViewBag.Mod = mdao.SearchById(int.Parse(Session["ModId"].ToString()));
                User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                ViewBag.User = u;
                ViewBag.Type = new TypeApp();
                ViewBag.Types = dao.List();
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public ActionResult Validate(TypeApp typeapp)
        {
            if (ModelState.IsValid)
            {
                TypeAppDAO dao = new TypeAppDAO();
                if (dao.IsUnique(typeapp))
                {
                    dao.Add(typeapp);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            ViewBag.Lang = typeapp;
            return View("Register");
        }

        public ActionResult Edit(int id)
        {
            if (Session["ModId"] != null)
            {
                TypeAppDAO dao = new TypeAppDAO();
                UserDAO udao = new UserDAO();
                User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                ViewBag.User = u;
                ViewBag.Type = dao.SearchById(id);
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public ActionResult EditConfirm(TypeApp TypeApp)
        {
            if (Session["ModId"] != null)
            {
                TypeAppDAO dao = new TypeAppDAO();
                TypeApp t = dao.SearchById(TypeApp.Id);
                t.Name = TypeApp.Name;
                dao.Update();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public ActionResult Remove(int id)
        {
            if (Session["ModId"] != null)
            {
                TypeAppDAO dao = new TypeAppDAO();
                TypeApp t = dao.SearchById(id);
                dao.Remove(t);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public PartialViewResult TypesAppBar(int id)
        {
            TypeAppDAO dao = new TypeAppDAO();
            ViewBag.Id = id;
            ViewBag.TypesBar = dao.List();
            return PartialView();
        }
    }
}
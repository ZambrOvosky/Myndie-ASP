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
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Register()
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    ViewBag.Lang = new Language();
                    return View();
                }
                return RedirectToAction("../Home/Index");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Index()
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    LanguageDAO dao = new LanguageDAO();
                    UserDAO udao = new UserDAO();
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.User = u;
                    ViewBag.Lang = new Language();
                    ViewBag.Langs = dao.List();
                    return View();
                }
                return RedirectToAction("../Home/Index");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Validate(Language language)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LanguageDAO dao = new LanguageDAO();
                    if (dao.IsUnique(language))
                    {
                        dao.Add(language);
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index");
                }
                ViewBag.Lang = language;
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
                    LanguageDAO dao = new LanguageDAO();
                    UserDAO udao = new UserDAO();
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.User = u;
                    ViewBag.Language = dao.SearchById(id);
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

        public ActionResult EditConfirm(Language language)
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    LanguageDAO dao = new LanguageDAO();
                    Language l = dao.SearchById(language.Id);
                    l.Name = language.Name;
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
                    LanguageDAO dao = new LanguageDAO();
                    Language l = dao.SearchById(id);
                    dao.Remove(l);
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
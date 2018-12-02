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
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Register()
        {
            try
            {
                string x = Session["ModId"].ToString();
                if (Session["ModId"] != null)
                {
                    ViewBag.Country = new Country();
                    return View();
                }
                return RedirectToAction("../Home/Index");
            }
            catch
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public ActionResult Index()
        {
            try
            {
                string x = Session["ModId"].ToString();
                if (Session["ModId"] != null)
                {
                    CountryDAO dao = new CountryDAO();
                    UserDAO udao = new UserDAO();
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.User = u;
                    ViewBag.Country = new Country();
                    ViewBag.Countries = dao.List();
                    return View();
                }
                return RedirectToAction("../Home/Index");
            }
            catch
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public ActionResult Validate(Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CountryDAO dao = new CountryDAO();
                    if (dao.IsUnique(country))
                    {
                        dao.Add(country);
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index");
                }
                ViewBag.Country = country;
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
                    CountryDAO dao = new CountryDAO();
                    UserDAO udao = new UserDAO();
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.User = u;
                    ViewBag.Country = dao.SearchById(id);
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

        public ActionResult EditConfirm(Country country)
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    CountryDAO dao = new CountryDAO();
                    Country g = dao.SearchById(country.Id);
                    g.Name = country.Name;
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
                    CountryDAO dao = new CountryDAO();
                    Country c = dao.SearchById(id);
                    dao.Remove(c);
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
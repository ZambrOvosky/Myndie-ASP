using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.Models;
using Myndie.DAO;

namespace Myndie.Controllers
{
    public class DeveloperController : Controller
    {
        // GET: Developer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            try
            {
                if (Session["Id"] != null)
                {
                    DeveloperDAO dao = new DeveloperDAO();
                    ViewBag.Dev = new Developer();
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

        public ActionResult Validate(Developer dev)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DeveloperDAO dao = new DeveloperDAO();
                    dev.NumSoft = 0;
                    dao.Add(dev);
                    Developer devel = dao.AttachDevUser(dev);

                    UserDAO udao = new UserDAO();
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    u.DeveloperId = devel.Id;
                    udao.Update();
                    Session["DevId"] = u.DeveloperId;
                    return RedirectToAction("ProfileView", "Developer");
                }
                ViewBag.Class = "alert alert-danger";
                ViewBag.Dev = dev;
                return View("Register");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ProfileView()
        {
            try
            {
                if (Session["DevId"] != null)
                {
                    DeveloperDAO dao = new DeveloperDAO();
                    ApplicationDAO appdao = new ApplicationDAO();
                    UserDAO udao = new UserDAO();
                    CountryDAO cdao = new CountryDAO();
                    Developer dev = dao.SearchById(int.Parse(Session["DevId"].ToString()));
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));

                    CartDAO cardao = new CartDAO();
                    if (Session["Id"] != null)
                    {
                        ViewBag.Cart = cardao.SearchCartUser(int.Parse(Session["Id"].ToString()));
                    }

                    ViewBag.DevGames = appdao.GetDevGames(dev.Id);
                    ViewBag.Dev = dev;
                    ViewBag.User = u;
                    ViewBag.CountryUser = cdao.SearchById(u.CountryId);
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

        public ActionResult Moderator()
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    DeveloperDAO dao = new DeveloperDAO();
                    UserDAO udao = new UserDAO();
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.User = u;
                    ViewBag.Devs = dao.List();
                    return View();
                }
                return RedirectToAction("../Home/Index");
            }
            catch
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public ActionResult Summary()
        {
            try
            {
                if (Session["DevId"] != null)
                {
                    DeveloperDAO dao = new DeveloperDAO();
                    UserDAO udao = new UserDAO();
                    ApplicationDAO adao = new ApplicationDAO();
                    ViewBag.User = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    Developer d = dao.SearchById(int.Parse(Session["DevId"].ToString()));
                    ViewBag.Apps = adao.GetDevGames(d.Id);
                    ViewBag.Dev = d;
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Sales()
        {
            try
            {
                if (Session["DevId"] != null)
                {
                    DeveloperDAO dao = new DeveloperDAO();
                    UserDAO udao = new UserDAO();
                    SellDAO sdao = new SellDAO();
                    Developer d = dao.SearchById(int.Parse(Session["DevId"].ToString()));
                    ViewBag.User = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.Dev = d;
                    ViewBag.SaleCount = sdao.DevGet7DaysSells(int.Parse(Session["DevId"].ToString()));
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
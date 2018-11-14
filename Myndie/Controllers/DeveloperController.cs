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
            if (Session["Id"] != null)
            {
                DeveloperDAO dao = new DeveloperDAO();
                ViewBag.Dev = new Developer();
                ViewBag.Class = "";
                return View();
            }
            return RedirectToAction("../Home/Index");           
        }

        public ActionResult Validate(Developer dev)
        {
            if(ModelState.IsValid){
                DeveloperDAO dao = new DeveloperDAO();
                dev.NumSoft = 0;
                dao.Add(dev);
                Developer devel = dao.AttachDevUser(dev);

                UserDAO udao = new UserDAO();
                User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                u.DeveloperId = devel.Id;
                udao.Update();
                Session["DevId"] = u.DeveloperId;
                return RedirectToAction("Register");
            }
            ViewBag.Class = "alert alert-danger";
            ViewBag.Dev = dev;
            return View("Register");
        }

        public ActionResult ProfileView()
        {
            if (Session["DevId"] != null)
            {
                DeveloperDAO dao = new DeveloperDAO();
                ApplicationDAO appdao = new ApplicationDAO();
                UserDAO udao = new UserDAO();
                CountryDAO cdao = new CountryDAO();
                Developer dev = dao.SearchById(int.Parse(Session["DevId"].ToString()));
                User u = udao.SearchById(int.Parse(Session["Id"].ToString()));

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
    }
}
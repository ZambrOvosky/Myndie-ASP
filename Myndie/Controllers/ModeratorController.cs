using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.Models;
using Myndie.DAO;

namespace Myndie.Controllers
{
    public class ModeratorController : Controller
    {
        // GET: Moderator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    UserDAO dao = new UserDAO();
                    User u = dao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.User = u;
                    ViewBag.Users = dao.List();
                    return View();
                }
                return RedirectToAction("../Home/Index");
            }
            catch
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public ActionResult ProfileView()
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    UserDAO udao = new UserDAO();
                    ModeratorDAO dao = new ModeratorDAO();
                    SellDAO sdao = new SellDAO();
                    ApplicationDAO adao = new ApplicationDAO();
                    ViewBag.Mod = dao.SearchById(int.Parse(Session["ModId"].ToString()));
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.NewUser = udao.GetMonthUsers().Count;
                    ViewBag.NewGame = adao.GetGamesMonth().Count;
                    ViewBag.NewSoft = adao.GetSoftwareMonth().Count;
                    ViewBag.NewMob = adao.GetMobileMonth().Count;
                    ViewBag.SaleCount = sdao.Get7DaysSells();
                    ViewBag.User = u;
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

        public ActionResult Promote(int id)
        {
            try
            {
                if (int.Parse(Session["Id"].ToString()) != id)
                {
                    ModeratorDAO dao = new ModeratorDAO();
                    UserDAO udao = new UserDAO();
                    if (dao.SearchByUserId(id) == null)
                    {
                        Moderator m = new Moderator();
                        m.UserId = id;
                        dao.Add(m);
                        Moderator mod = dao.SearchByUserId(id);
                        User u = udao.SearchById(id);
                        u.ModeratorId = mod.Id;
                        udao.Update();
                    }
                }
                return RedirectToAction("Register");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Demote(int id)
        {
            try
            {
                if (int.Parse(Session["Id"].ToString()) != id)
                {
                    ModeratorDAO dao = new ModeratorDAO();
                    UserDAO udao = new UserDAO();
                    if (dao.SearchByUserId(id) != null)
                    {
                        User u = udao.SearchById(id);
                        Moderator m = dao.SearchByUserId(id);
                        u.ModeratorId = null;
                        udao.Update();
                        dao.Remove(m);
                    }
                }
                return RedirectToAction("Register");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public PartialViewResult GenreIndex()
        {
            if (Session["ModId"] != null)
            {
                GenreDAO dao = new GenreDAO();
                UserDAO udao = new UserDAO();
                ModeratorDAO mdao = new ModeratorDAO();
                ViewBag.Mod = mdao.SearchById(int.Parse(Session["ModId"].ToString()));
                User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                ViewBag.User = u;
                ViewBag.Genre = new Genre();
                ViewBag.Genres = dao.List();
                return PartialView();
            }
            else
            {
                return PartialView();
            }


        }
    }
}
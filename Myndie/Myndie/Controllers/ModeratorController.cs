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
            if (Session["ModId"] != null)
            {
                UserDAO udao = new UserDAO();
                ViewBag.Users = udao.List();
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Index");
            }

        }

        public ActionResult ProfileView()
        {
            if(Session["ModId"] != null)
            {
                ModeratorDAO dao = new ModeratorDAO();
                ViewBag.Mod = dao.SearchById(int.Parse(Session["ModId"].ToString()));
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public ActionResult Promote(int id)
        {
            if(int.Parse(Session["Id"].ToString()) != id)
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

        public ActionResult Demote(int id)
        {
            if(int.Parse(Session["Id"].ToString()) != id)
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
    }
}
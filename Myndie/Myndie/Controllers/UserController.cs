﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.Models;
using Myndie.DAO;

namespace Myndie.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            UserDAO dao = new UserDAO();
            CountryDAO cdao = new CountryDAO();
            LanguageDAO ldao = new LanguageDAO();
            ViewBag.Class = "";
            ViewBag.User = new User();
            ViewBag.Country = cdao.List();
            ViewBag.Lang = ldao.List();
            return View();
        }

        public ActionResult Validate(User user)
        {
            if (ModelState.IsValid)
            {
                if(user.Password.Length > 4)
                {
                    user.CrtDate = DateTime.Now;
                    user.Picture = "";
                    UserDAO dao = new UserDAO();
                    dao.Add(user);
                    return RedirectToAction("Register");
                }
                else{ ModelState.AddModelError("user.Password4", "Your password needs to be at least 5 characters");

                }
                 
            }
            CountryDAO cdao = new CountryDAO();
            LanguageDAO ldao = new LanguageDAO();
            ViewBag.User = user;
            ViewBag.Class = "alert alert-danger";
            ViewBag.Country = cdao.List();
            ViewBag.Lang = ldao.List();
            return View("Register");
        }

        public ActionResult Login(User user)
        {
            UserDAO dao = new UserDAO();
            User u = dao.Login(user);
            var result = "";
            if(u != null)
            {
                Session["Id"] = u.Id;
                Session["Username"] = u.Username;                
                if (u.DeveloperId != null)
                {
                    DeveloperDAO ddao = new DeveloperDAO();
                    Developer d = ddao.SearchById(int.Parse(u.DeveloperId.ToString()));
                    Session["DevId"] = d.Id;
                }
                if (u.ModeratorId != null)
                {
                    ModeratorDAO mdao = new ModeratorDAO();
                    Moderator m = mdao.SearchById(int.Parse(u.ModeratorId.ToString()));
                    Session["ModId"] = m.Id;
                }
                result = "Logged In";
                return Json(result, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("../Home/Index");
            }
            result = "An error occurred. Try again";
            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("../Home/Index");
        }

        public ActionResult ProfileView()
        {
            if (Session["Id"] != null)
            {
                UserDAO dao = new UserDAO();
                User u = dao.SearchById(int.Parse(Session["Id"].ToString()));
                CountryDAO cdao = new CountryDAO();
                LanguageDAO ldao = new LanguageDAO();
                ViewBag.User = u;
                ViewBag.Country = cdao.List();
                ViewBag.Lang = ldao.List();
                return View();
            }
            return RedirectToAction("../Home/Index");            
        }

        public ActionResult Update(User user)
        {
            var result = "";
            try {             
            if (user.CountryId != 0 && user.LanguageId != 0 && user.Name != null && user.Email != null){
                UserDAO dao = new UserDAO();
                User u = dao.SearchById(int.Parse(Session["Id"].ToString()));
                u.CountryId = user.CountryId;
                u.LanguageId = user.LanguageId;
                u.Name = user.Name;
                u.Email = user.Email;
                dao.Update();
                result = "Profile Updated";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            result = "An error ocurred";
            return Json(result, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Profile");
            }
            catch {
                result = "An error ocurred";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChangePassword(string psw, string cpsw)
        {
            UserDAO dao = new UserDAO();
            //var result = "";
            if (psw != null && cpsw != null && psw == cpsw){
                if(psw.Length > 4)
                {
                    User u = dao.SearchById(int.Parse(Session["Id"].ToString()));
                    u.Password = psw;
                    dao.Update();
                    //result = "Password Changed";
                    //return Json(result, JsonRequestBehavior.AllowGet);
                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError("user.Password4", "Your password needs to be at least 5 characters");
                }                
            }
            else
            {
                ModelState.AddModelError("user.PasswordDM", "Your passwords don't match");
            }
            User u2 = dao.SearchById(int.Parse(Session["Id"].ToString()));
            CountryDAO cdao = new CountryDAO();
            LanguageDAO ldao = new LanguageDAO();
            ViewBag.User = u2;
            ViewBag.Country = cdao.List();
            ViewBag.Lang = ldao.List();
            //result = "Error";
            //return Json(result, JsonRequestBehavior.AllowGet);
            return View("Profile");
        }

        public ActionResult Logout()
        {
            Session["Id"] = null;
            Session["Username"] = null;
            Session["DevId"] = null;
            Session["ModId"] = null;
            return RedirectToAction("../Home/Index");
        }
    }    
}
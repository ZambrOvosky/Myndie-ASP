﻿using System;
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
            if (Session["ModId"] != null)
            {
                ViewBag.Lang = new Language();
                return View();
            }
            return RedirectToAction("../Home/Index");            
        }

        public ActionResult Index()
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

        public ActionResult Validate(Language language)
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.DAO;
using Myndie.Models;

namespace Myndie.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            if(Session["DevId"] != null)
            {
                TypeAppDAO tdao = new TypeAppDAO();
                PegiDAO pdao = new PegiDAO();
                ViewBag.Class = "";
                ViewBag.Type = tdao.List();
                ViewBag.Pegi = pdao.List();
                ViewBag.App = new Application();
                return View();
            }
            return RedirectToAction("../Home/Index");
            
        }

        public ActionResult Validate(Application app)
        {
            var result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    app.Approved = 0;
                    ApplicationDAO dao = new ApplicationDAO();
                    Application uniq = dao.IsUnique(app);
                    DeveloperDAO ddao = new DeveloperDAO();
                    Developer Dev = ddao.SearchById(int.Parse(Session["DevId"].ToString()));
                    if(Dev != null)
                    {
                        if(uniq == null)
                        {
                            app.DeveloperId = Dev.Id;
                            dao = new ApplicationDAO();
                            dao.Add(app);
                            Dev.NumSoft++;
                            ddao.Update();
                            result = "Successfully Registered";
                            return Json(result, JsonRequestBehavior.AllowGet);
                            //return RedirectToAction("Register");
                        }
                        else { result = "There is already a game with this name"; return Json(result, JsonRequestBehavior.AllowGet); }
                        
                    }
                    else { result = "You are not a Developer"; return Json(result, JsonRequestBehavior.AllowGet); }                    
                }
                catch
                {                    
                    ViewBag.App = app;
                    result = "An Error Occurred";
                    return Json(result, JsonRequestBehavior.AllowGet);                    
                    //return RedirectToAction("Register");
                }                
            }            
            ViewBag.App = app;
            ViewBag.Class = "alert alert-danger";
            result = "An Error Occurred";
            return Json(result, JsonRequestBehavior.AllowGet);
            //return View("Register");
        }

        public ActionResult Product (int id)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                DeveloperDAO ddao = new DeveloperDAO();
                Application app = dao.SearchById(id);
                Developer dev = ddao.SearchById(app.DeveloperId);
                ViewBag.App = app;
                ViewBag.Dev = dev;
                return View();
            }
            catch
            {
                return RedirectToAction("../Home/Index");
            }
        }
    }
}
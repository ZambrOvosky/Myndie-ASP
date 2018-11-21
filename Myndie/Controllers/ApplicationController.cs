using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.DAO;
using Myndie.Models;
using System.IO;
using System.Web.Script.Serialization;

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

        public ActionResult Validate(Application app, IList<HttpPostedFileBase> images, HttpPostedFileBase File)
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
                    ImageDAO idao = new ImageDAO();
                    Developer Dev = ddao.SearchById(int.Parse(Session["DevId"].ToString()));
                    if (Dev != null)
                    {
                        if (uniq == null)
                        {
                            app.DeveloperId = Dev.Id;
                            dao = new ApplicationDAO();
                            dao.Add(app);
                            Dev.NumSoft++;
                            ddao.Update();

                            Application appreg = dao.GetDevLastGame(Dev.Id);
                            try
                            {
                                
                                //Images
                                int count = 0;
                                foreach (var img in images)
                                {
                                    string filePath = Guid.NewGuid() + Path.GetExtension(img.FileName);
                                    img.SaveAs(Path.Combine(Server.MapPath("~/media/app"), filePath));
                                    Image i = new Image();
                                    i.Url = "../../../media/app/" + filePath;
                                    i.UserId = int.Parse(Session["Id"].ToString());
                                    i.ApplicationId = appreg.Id;
                                    idao.Add(i);
                                    if (count == 0)
                                    {
                                        appreg.ImageUrl = i.Url;
                                        count++;
                                        dao.Update();
                                    }
                                }
                                //
                            }
                            catch
                            {
                                appreg.ImageUrl = "../../../assets/images/game-kingdoms-of-amalur-reckoning-4-500x375.jpg";
                                dao.Update();
                                result = "Error on Uploading Images, try later"; return Json(result, JsonRequestBehavior.AllowGet);
                            }
                            try
                            {
                                //File
                                string filePath2 = Guid.NewGuid() + Path.GetExtension(File.FileName);
                                if (!Directory.Exists(Server.MapPath("~/media/appfiles/" + appreg.Id)))
                                {
                                    Directory.CreateDirectory(Server.MapPath("~/media/appfiles/" + appreg.Id));
                                }
                                File.SaveAs(Path.Combine(Server.MapPath("~/media/appfiles/" + appreg.Id), filePath2));
                                appreg.Archive = "../../../media/appfiles/" + appreg.Id + "/" + filePath2;
                                dao.Update();
                                //
                            }
                            catch
                            {
                                result = "Error on Uploading Files, try later"; return Json(result, JsonRequestBehavior.AllowGet);
                            }
                            

                            

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
                ImageDAO idao = new ImageDAO();
                Application app = dao.SearchById(id);
                Developer dev = ddao.SearchById(app.DeveloperId);
                CartDAO cdao = new CartDAO();
                if (Session["Id"] != null)
                {
                    ViewBag.Cart = cdao.SearchCartUser(int.Parse(Session["Id"].ToString()));
                }
                ViewBag.App = app;
                ViewBag.Dev = dev;
                ViewBag.Img = idao.SearchAppImages(id);
                ViewBag.Similar = dao.ListLast10();
                return View();
            }
            catch
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public ActionResult Search(string search)
        {
            ApplicationDAO dao = new ApplicationDAO();
            ViewBag.Apps = dao.Search(search);
            CartDAO cdao = new CartDAO();
            if (Session["Id"] != null)
            {
                ViewBag.Cart = cdao.SearchCartUser(int.Parse(Session["Id"].ToString()));
            }
            return View();
        }

        public ActionResult SearchByType(string Type)
        {
            ApplicationDAO dao = new ApplicationDAO();
            ViewBag.Apps = dao.SearchByType(Type);
            CartDAO cdao = new CartDAO();
            if (Session["Id"] != null)
            {
                ViewBag.Cart = cdao.SearchCartUser(int.Parse(Session["Id"].ToString()));
            }
            return View("Search");
        }

        public string GetAppImage(int id)
        {
            ApplicationDAO dao = new ApplicationDAO();
            Application a = dao.SearchById(id);
            return a.ImageUrl;
        }

        public string GetAppName(int id)
        {
            ApplicationDAO dao = new ApplicationDAO();
            Application a = dao.SearchById(id);
            return a.Name;
        }
    }
}
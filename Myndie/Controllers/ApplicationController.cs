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
            if (Session["DevId"] != null)
            {
                TypeAppDAO tdao = new TypeAppDAO();
                PegiDAO pdao = new PegiDAO();
                UserDAO udao = new UserDAO();
                ViewBag.Class = "";
                ViewBag.Type = tdao.List();
                ViewBag.Pegi = pdao.List();
                ViewBag.App = new Application();
                ViewBag.User = udao.SearchById(int.Parse(Session["Id"].ToString()));
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
                        if (app.ReleaseDate.Year >= 1900 && app.ReleaseDate.Year <= DateTime.Now.Year + 1)
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
                                    if (!Directory.Exists(Server.MapPath("~/apps/appfiles/" + appreg.Id)))
                                    {
                                        Directory.CreateDirectory(Server.MapPath("~/apps/appfiles/" + appreg.Id));
                                    }
                                    File.SaveAs(Path.Combine(Server.MapPath("~/apps/appfiles/" + appreg.Id), filePath2));
                                    appreg.Archive = "../../../apps/appfiles/" + appreg.Id + "/" + filePath2;
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
                        else { result = "The release date is not acceptable"; return Json(result, JsonRequestBehavior.AllowGet); }
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

        public ActionResult Product(int id)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                DeveloperDAO ddao = new DeveloperDAO();
                ImageDAO idao = new ImageDAO();
                SellItemDAO sidao = new SellItemDAO();
                Application app = dao.SearchById(id);
                Developer dev = ddao.SearchById(app.DeveloperId);
                CartDAO cdao = new CartDAO();
                ViewBag.SellItem = false;
                if (Session["Id"] != null)
                {
                    ViewBag.Cart = cdao.SearchCartUser(int.Parse(Session["Id"].ToString()));
                    ViewBag.SellItem = sidao.SearchUserApp(int.Parse(Session["Id"].ToString()), id);
                    WishlistDAO wdao = new WishlistDAO();
                    ViewBag.IsInWish = wdao.IsInWishList(int.Parse(Session["Id"].ToString()), id);
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

        public ActionResult SearchByType(int Type)
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

        public ActionResult Moderator(int id)
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    ApplicationDAO dao = new ApplicationDAO();
                    UserDAO udao = new UserDAO();
                    DeveloperDAO ddao = new DeveloperDAO();
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.User = u;
                    ViewBag.Apps = dao.SearchByType(id);
                    ViewBag.AppsToApprove = dao.AppsToApprove();
                    ViewBag.Devs = ddao.List();
                    return View();
                }
                else
                {
                    return RedirectToAction("../Home/Index");
                }
            }
            catch
            {
                return RedirectToAction("../Home/Index");
            }
        }

        public PartialViewResult _GenreGameRightSide()
        {
            GenreDAO gdao = new GenreDAO();
            ApplicationDAO appdao = new ApplicationDAO();
            IList<Genre> g = gdao.GetTop10();
            ViewBag.GenreSide = g;
            IList<Application> a = appdao.GetTop5();
            ViewBag.AppsSide = a;
            return PartialView();
        }

        public ActionResult EditGame(int id)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                Application App = dao.SearchById(id);
                UserDAO udao = new UserDAO();
                TypeAppDAO tdao = new TypeAppDAO();
                PegiDAO pdao = new PegiDAO();
                if (Session["ModId"] != null || App.DeveloperId == int.Parse(Session["DevId"].ToString()))
                {
                    ViewBag.User = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ImageDAO idao = new ImageDAO();
                    ViewBag.Imgs = idao.SearchAppImages(App.Id);
                    ViewBag.App = App;
                    ViewBag.Types = tdao.List();
                    ViewBag.Pegis = pdao.List();
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult UpdateInfo(Application app)
        {
            if (Session["ModId"] != null || app.DeveloperId == int.Parse(Session["DevId"].ToString()))
            {
                ApplicationDAO dao = new ApplicationDAO();
                Application a = dao.SearchById(app.Id);
                a.Name = app.Name;
                a.Desc = app.Desc;
                a.Price = app.Price;
                a.TypeAppId = app.TypeAppId;
                a.PegiId = app.PegiId;
                dao.Update();
                return RedirectToAction("EditGame", "Application", new { id = app.Id });
            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Remove(int id)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                ImageDAO idao = new ImageDAO();
                Application a = dao.SearchById(id);
                if (Session["ModId"] != null || int.Parse(Session["DevId"].ToString()) == a.DeveloperId)
                {
                    try
                    {
                        IList<Image> imgs = idao.SearchAppImages(a.Id);
                        foreach (var i in imgs)
                        {
                            string p = i.Url;
                            p = p.Replace("../../..", "../..");
                            string fullPath = Request.MapPath(p);
                            idao.Remove(i);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        }

                    }
                    catch { }
                    try
                    {
                        string pa = a.Archive;
                        pa = pa.Replace("../../..", "../..");
                        string fullPathArch = Request.MapPath(pa);
                        if (System.IO.File.Exists(fullPathArch))
                        {
                            System.IO.File.Delete(fullPathArch);
                        }
                        
                    }
                    catch { }
                    dao.Remove(a);
                    return RedirectToAction("Summary", "Developer");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditPhoto(int id, int PhotoId, HttpPostedFileBase img)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                Application a = dao.SearchById(id);
                if (Session["ModId"] != null || int.Parse(Session["DevId"].ToString()) == a.DeveloperId)
                {
                    ImageDAO idao = new ImageDAO();

                    Image i = idao.SearchById(PhotoId);
                    bool b = false;
                    if (a.ImageUrl == i.Url)
                    {
                        b = true;
                    }
                    //Delete Photo in Server
                    string p = i.Url;
                    p = p.Replace("../../..", "..");
                    string fullPath = Request.MapPath(p);
                    idao.Remove(i);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    //Upload
                    string filePath = Guid.NewGuid() + Path.GetExtension(img.FileName);
                    img.SaveAs(Path.Combine(Server.MapPath("~/media/app"), filePath));
                    Image im = new Image();
                    im.Url = "../../../media/app/" + filePath;
                    im.UserId = int.Parse(Session["Id"].ToString());
                    im.ApplicationId = a.Id;
                    idao.Add(im);

                    if (b)
                    {
                        a.ImageUrl = im.Url;
                        dao.Update();
                    }

                    return RedirectToAction("EditGame", "Application", new { id = a.Id });
                }
                return RedirectToAction("Index", "Home");
            }
            catch { return RedirectToAction("Index", "Home"); }
        }

        public ActionResult TurntoMain(int id, int AppId)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                Application a = dao.SearchById(AppId);
                if (Session["ModId"] != null || int.Parse(Session["DevId"].ToString()) == a.DeveloperId)
                {
                    ImageDAO idao = new ImageDAO();
                    Image i = idao.SearchById(id);
                    a.ImageUrl = i.Url;
                    dao.Update();
                    return RedirectToAction("EditGame", "Application", new { id = a.Id });
                }
                return RedirectToAction("Index", "Home");
            }
            catch { return RedirectToAction("Index", "Home"); }
        }

        public ActionResult NewPhoto(int id, HttpPostedFileBase img)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                Application a = dao.SearchById(id);
                if (Session["ModId"] != null || int.Parse(Session["DevId"].ToString()) == a.DeveloperId)
                {
                    ImageDAO idao = new ImageDAO();
                    string filePath = Guid.NewGuid() + Path.GetExtension(img.FileName);
                    img.SaveAs(Path.Combine(Server.MapPath("~/media/app"), filePath));
                    Image im = new Image();
                    im.Url = "../../../media/app/" + filePath;
                    im.UserId = int.Parse(Session["Id"].ToString());
                    im.ApplicationId = a.Id;
                    idao.Add(im);
                    return RedirectToAction("EditGame", "Application", new { id = a.Id });
                }
                return RedirectToAction("Index", "Home");
            }
            catch { return RedirectToAction("Index", "Home"); }
        }

        //public ActionResult 
    }
}
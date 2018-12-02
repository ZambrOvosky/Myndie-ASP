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
            try
            {
                if (Session["DevId"] != null)
                {
                    TypeAppDAO tdao = new TypeAppDAO();
                    PegiDAO pdao = new PegiDAO();
                    UserDAO udao = new UserDAO();
                    GenreDAO gdao = new GenreDAO();
                    ViewBag.Class = "";
                    ViewBag.Type = tdao.List();
                    ViewBag.Pegi = pdao.List();
                    ViewBag.App = new Application();
                    ViewBag.Genres = gdao.ListId();
                    ViewBag.User = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    return View();
                }
                return RedirectToAction("../Home/Index");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Validate(Application app, IList<HttpPostedFileBase> images, HttpPostedFileBase File, IList<Genre> genres)
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
                    ApplicationGenreDAO agdao = new ApplicationGenreDAO();
                    Developer Dev = ddao.SearchById(int.Parse(Session["DevId"].ToString()));
                    bool ImgError = false, FileError = false, AGError = false;
                    if (Dev != null)
                    {
                        if (app.ReleaseDate.Year >= 1900 && app.ReleaseDate.Year <= DateTime.Now.Year + 1)
                        {
                            if (app.Price >= 0 && app.Price <= 1000)
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
                                        foreach (var g in genres)
                                        {
                                            if (g.IsChecked == true)
                                            {
                                                ApplicationGenre ag = new ApplicationGenre();
                                                ag.ApplicationId = appreg.Id;
                                                ag.GenreId = g.Id;
                                                agdao.Add(ag);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        AGError = true;
                                    }

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
                                        ImgError = true;
                                        result = result + "Error on Uploading Images, try later";
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
                                        FileError = true;
                                        result = result + "Error on Uploading Files, try later";
                                    }

                                    if (ImgError || FileError || AGError)
                                    {
                                        return Json(result, JsonRequestBehavior.AllowGet);
                                    }


                                    result = "Successfully Registered";
                                    return Json(result, JsonRequestBehavior.AllowGet);
                                    //return RedirectToAction("Register");
                                }
                                else { result = "There is already a game with this name"; return Json(result, JsonRequestBehavior.AllowGet); }
                            }
                            else { result = "Application Price is not acceptable"; return Json(result, JsonRequestBehavior.AllowGet); }
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
                UserDAO udao = new UserDAO();
                SellItemDAO sidao = new SellItemDAO();
                Application app = dao.SearchById(id);
                Developer dev = ddao.SearchById(app.DeveloperId);
                CartDAO cdao = new CartDAO();
                PegiDAO pdao = new PegiDAO();
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
                ViewBag.Pegi = pdao.SearchById(app.PegiId);
                ViewBag.DevUser = udao.SearchByDev(dev.Id);
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
            try
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
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SearchByType(int Type)
        {
            try
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
            catch
            {
                return RedirectToAction("Index", "Home");
            }
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

        public ActionResult EditApp(int id)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                Application App = dao.SearchById(id);
                UserDAO udao = new UserDAO();
                TypeAppDAO tdao = new TypeAppDAO();
                PegiDAO pdao = new PegiDAO();
                GenreDAO gdao = new GenreDAO();
                ApplicationGenreDAO agdao = new ApplicationGenreDAO();
                if (Session["ModId"] != null || App.DeveloperId == int.Parse(Session["DevId"].ToString()))
                {
                    ViewBag.User = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ImageDAO idao = new ImageDAO();
                    ViewBag.Imgs = idao.SearchAppImages(App.Id);
                    ViewBag.App = App;
                    ViewBag.Types = tdao.List();
                    ViewBag.Pegis = pdao.List();
                    IList<Genre> genres = gdao.ListId();
                    IList<ApplicationGenre> agens = agdao.ListByApplication(id);
                    foreach (var ag in agens)
                    {
                        foreach (var g in genres)
                        {
                            if (ag.GenreId == g.Id)
                            {
                                g.IsChecked = true;
                            }
                        }
                    }
                    ViewBag.Genres = genres;
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
            try
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
                    return RedirectToAction("EditApp", "Application", new { id = app.Id });
                }
                return RedirectToAction("Index", "Home");
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

                    return RedirectToAction("EditApp", "Application", new { id = a.Id });
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
                    return RedirectToAction("EditApp", "Application", new { id = a.Id });
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
                    return RedirectToAction("EditApp", "Application", new { id = a.Id });
                }
                return RedirectToAction("Index", "Home");
            }
            catch { return RedirectToAction("Index", "Home"); }
        }

        public ActionResult Explore(string type)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                CartDAO cdao = new CartDAO();
                if (Session["Id"] != null)
                {
                    ViewBag.Cart = cdao.SearchCartUser(int.Parse(Session["Id"].ToString()));
                }
                if (type.Equals("Featured"))
                {
                    ViewBag.Apps = dao.GetTopApps();
                }
                else if (type.Equals("Explore"))
                {
                    ViewBag.Apps = dao.List();
                }
                else if (type.Equals("Free"))
                {
                    ViewBag.Apps = dao.GetFreeApps();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                return View("Search");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult RemoveImage(int id, int AppId)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                Application a = dao.SearchById(id);
                if (Session["ModId"] != null || int.Parse(Session["DevId"].ToString()) == a.DeveloperId)
                {
                    ImageDAO idao = new ImageDAO();
                    Image i = idao.SearchById(id);
                    idao.Remove(i);
                    return RedirectToAction("EditApp", "Application", new { id = AppId });
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult UpdateApp(int id)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                UserDAO udao = new UserDAO();
                Application app = dao.SearchById(id);
                if (int.Parse(Session["DevId"].ToString()) == app.DeveloperId)
                {
                    ViewBag.User = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ViewBag.App = app;
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult UpdateAppData(UpdateNotes update, HttpPostedFileBase file)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                UpdateNotesDAO updao = new UpdateNotesDAO();
                Application app = dao.SearchById(update.ApplicationId);
                if (int.Parse(Session["DevId"].ToString()) == app.DeveloperId && update.Value != null)
                {
                    updao.Add(update);

                    //Delete File in Server
                    string p = app.Archive;
                    p = p.Replace("../../..", "..");
                    string fullPath = Request.MapPath(p);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    //File
                    string filePath2 = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    if (!Directory.Exists(Server.MapPath("~/apps/appfiles/" + app.Id)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/apps/appfiles/" + app.Id));
                    }
                    file.SaveAs(Path.Combine(Server.MapPath("~/apps/appfiles/" + app.Id), filePath2));
                    app.Archive = "../../../apps/appfiles/" + app.Id + "/" + filePath2;
                    dao.Update();
                    //
                    return RedirectToAction("Summary", "Developer");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult UpdateGenres(IList<Genre> genres, int appId)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                Application app = dao.SearchById(appId);
                if (Session["ModId"] != null || app.DeveloperId == int.Parse(Session["DevId"].ToString()))
                {

                    ApplicationGenreDAO agdao = new ApplicationGenreDAO();
                    IList<ApplicationGenre> ags = agdao.ListByApplication(appId);
                    foreach (var g in genres)
                    {
                        foreach (var ag in ags)
                        {
                            if (ag.GenreId == g.Id)
                            {
                                if (!g.IsChecked)
                                {
                                    agdao.Remove(ag);
                                }
                            }
                        }
                        if (g.IsChecked == true)
                        {
                            ApplicationGenre ag = new ApplicationGenre();
                            ag.ApplicationId = appId;
                            ag.GenreId = g.Id;
                            agdao.Add(ag);
                        }
                    }
                    return RedirectToAction("EditApp", "Application", new { id = appId });
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SearchByGenre(int id)
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                ApplicationGenreDAO agdao = new ApplicationGenreDAO();
                IList<ApplicationGenre> agens = agdao.ListByGenre(id);
                IList<Application> apps = new List<Application>();
                CartDAO cdao = new CartDAO();
                if (Session["Id"] != null)
                {
                    ViewBag.Cart = cdao.SearchCartUser(int.Parse(Session["Id"].ToString()));
                }
                foreach (var ag in agens)
                {
                    apps.Add(dao.SearchById(ag.ApplicationId));
                }
                ViewBag.Apps = apps;
                return View("Search");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult SearchAllGenres()
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                GenreDAO gdao = new GenreDAO();
                CartDAO cdao = new CartDAO();
                if (Session["Id"] != null)
                {
                    ViewBag.Cart = cdao.SearchCartUser(int.Parse(Session["Id"].ToString()));
                }
                ViewBag.Apps = dao.ListTop10();
                ViewBag.Genres = gdao.List();
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult ApproveApp(int id)
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    UserDAO udao = new UserDAO();
                    User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    ImageDAO idao = new ImageDAO();
                    ApplicationDAO dao = new ApplicationDAO();
                    TypeAppDAO tdao = new TypeAppDAO();
                    GenreDAO gdao = new GenreDAO();
                    PegiDAO pdao = new PegiDAO();
                    ApplicationGenreDAO agdao = new ApplicationGenreDAO();

                    ViewBag.Types = tdao.List();
                    ViewBag.Pegis = pdao.List();
                    ViewBag.AppGens = agdao.ListByApplication(id);
                    ViewBag.Genres = gdao.List();
                    ViewBag.User = u;
                    ViewBag.App = dao.SearchById(id);
                    ViewBag.Img = idao.SearchAppImages(id);
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            catch { return RedirectToAction("Index", "Home"); }
        }

        public ActionResult Approve(int id)
        {
            try
            {
                if (Session["ModId"] != null)
                {
                    ApplicationDAO dao = new ApplicationDAO();
                    Application a = dao.SearchById(id);
                    a.Approved = 1;
                    dao.Update();
                    return RedirectToAction("ProfileView", "Moderator");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public PartialViewResult _SearchAuto()
        {
            ApplicationDAO dao = new ApplicationDAO();
            ViewBag.AppName = dao.GetAppsName();
            return PartialView();
        }

        public ActionResult SearchMyndie()
        {
            try
            {
                ApplicationDAO dao = new ApplicationDAO();
                Application a = dao.SearchById(39);
                return RedirectToAction("Product", "Application", new { id = a.Id });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.DAO;
using Myndie.Models;

namespace Myndie.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(Review review)
        {
            try
            {
                ReviewDAO dao = new ReviewDAO();
                UserDAO udao = new UserDAO();
                review.Date = DateTime.Now;
                User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
                review.UserId = u.Id;
                Review rev = dao.SearchByUserApp(review.UserId, review.ApplicationId);
                if (rev != null)
                {
                    dao.Remove(rev);
                }
                dao.Add(review);
                IList<Review> revs = dao.SearchByAppId(review.ApplicationId);
                double totalrate = 0;
                foreach (var r in revs)
                {
                    totalrate += r.Value;
                }
                totalrate = Math.Round(totalrate / revs.Count);
                ApplicationDAO appdao = new ApplicationDAO();
                Application a = appdao.SearchById(review.ApplicationId);
                a.Value = int.Parse(totalrate.ToString());
                appdao.Update();
                return RedirectToAction("Product", "Application", new { id = review.ApplicationId });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ChildActionOnly]
        public PartialViewResult _ListReview(int AppId)
        {
            ReviewDAO dao = new ReviewDAO();
            UserDAO udao = new UserDAO();
            IList<Review> list = dao.SearchByAppId(AppId);
            IList<User> users = new List<User>();
            bool b = false;
            foreach (var r in list)
            {
                b = false;
                foreach (var u in users)
                {
                    if (u.Id == r.UserId)
                    {
                        b = true;
                    }
                }
                if (!b)
                {
                    users.Add(udao.SearchById(r.UserId));
                }

            }
            ViewBag.Revs = list;
            ViewBag.UserRevs = users;
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _ListReviewDev(int AppId)
        {
            ReviewDAO dao = new ReviewDAO();
            UserDAO udao = new UserDAO();
            IList<Review> list = dao.SearchByAppId(AppId);
            IList<User> users = new List<User>();
            bool b = false;
            foreach (var r in list)
            {
                b = false;
                foreach (var u in users)
                {
                    if (u.Id == r.UserId)
                    {
                        b = true;
                    }
                }
                if (!b)
                {
                    users.Add(udao.SearchById(r.UserId));
                }

            }
            ViewBag.Revs = list;
            ViewBag.UserRevs = users;
            return PartialView();
        }

        public ActionResult YourReviews()
        {
            try
            {
                if (Session["DevId"] != null)
                {
                    ApplicationDAO appdao = new ApplicationDAO();
                    ReviewDAO rdao = new ReviewDAO();
                    IList<Application> apps = appdao.GetDevGames(int.Parse(Session["DevId"].ToString()));
                    UserDAO udao = new UserDAO();
                    ViewBag.User = udao.SearchById(int.Parse(Session["Id"].ToString()));
                    //IList<Review> revs = new List<Review>();
                    //foreach (var a in apps)
                    //{
                    //    IList<Review> rev = rdao.SearchByAppId(a.Id);
                    //    foreach(var r in rev)
                    //    {
                    //        revs.Add(r);
                    //    }                    
                    //}
                    ViewBag.Apps = apps;
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public PartialViewResult _GameValue(int id)
        {

            ApplicationDAO adao = new ApplicationDAO();
            ViewBag.App = adao.SearchById(id);
            return PartialView();
        }
    }
}
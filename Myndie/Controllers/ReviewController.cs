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
            ReviewDAO dao = new ReviewDAO();
            UserDAO udao = new UserDAO();
            review.Date = DateTime.Now;
            User u = udao.SearchById(int.Parse(Session["Id"].ToString()));
            review.UserId = u.Id;
            dao.Add(review);
            return RedirectToAction("Product","Application", new { id = review.ApplicationId});
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
    }
}
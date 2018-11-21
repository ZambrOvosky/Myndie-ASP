using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myndie.DAO;
using Myndie.Models;

namespace Myndie.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddtoCart(int appid)
        {
            CartDAO dao = new CartDAO();
            if(!dao.CheckCart(appid, int.Parse(Session["Id"].ToString())))
            {
                ApplicationDAO appdao = new ApplicationDAO();
                Cart c = new Cart();
                c.UserId = int.Parse(Session["Id"].ToString());
                c.ApplicationId = appid;
                Application app = appdao.SearchById(appid);
                c.Price = app.Price;
                dao.Add(c);
            }
            return RedirectToAction("Product","Application", new { id = appid });
        }

        public ActionResult Cart()
        {
            CartDAO dao = new CartDAO();
            if (Session["Id"] != null)
            {
                ViewBag.FullCart = dao.GetUserCart(int.Parse(Session["Id"].ToString()));
                return View();
            }
            return RedirectToAction("../Home/Index");
        }

        public ActionResult RemoveFromCart(int appid)
        {
            try
            {
                CartDAO dao = new CartDAO();
                Cart c = dao.SearchCart(appid, int.Parse(Session["Id"].ToString()));
                dao.Remove(c);

            }
            catch
            {

            }
            return RedirectToAction("Cart");
        }

        public double GetSubtotal()
        {
            CartDAO dao = new CartDAO();
            return dao.GetSubtotal(int.Parse(Session["Id"].ToString()));
        }
    }
}
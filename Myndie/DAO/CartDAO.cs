using Myndie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myndie.DAO
{
    public class CartDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Cart cart)
        {
            context.Carts.Add(cart);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public void Remove(Cart cart)
        {
            context.Carts.Remove(cart);
            Update();
        }

        public Cart SearchCart(int appid, int userid)
        {
            Cart cart = (from c in context.Carts where c.ApplicationId == appid && c.UserId == userid select c).FirstOrDefault();
            return cart;
        }

        public bool CheckCart(int appid, int userid)
        {
            try
            {
                Cart cart = SearchCart(appid, userid);
                if (cart != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }       
        }

        public IList<Cart> SearchCartUser(int UserId)
        {
            return (from c in context.Carts where c.UserId == UserId select c).Take(5).ToList();
        }

        public double GetSubtotal(int UserId)
        {
            try
            {
                return (from c in context.Carts where c.UserId == UserId select c.Price).Sum();
            }
            catch
            {
                return 0.00;
            }


        }

        public IList<Cart> GetUserCart(int UserId)
        {
            return (from c in context.Carts where c.UserId == UserId select c).ToList();
        }
    }
}
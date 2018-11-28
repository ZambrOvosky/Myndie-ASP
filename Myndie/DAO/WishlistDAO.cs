using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class WishlistDAO
    {
        private EntityContext context = new EntityContext();

        public void Update()
        {
            context.SaveChanges();
        }

        public void Add(Wishlist wish)
        {
            context.Wishlists.Add(wish);
            Update();
        }

        public IList<Wishlist> List()
        {
            return context.Wishlists.ToList();
        }

        public void Remove(Wishlist wish)
        {
            context.Wishlists.Remove(wish);
            Update();
        }

        public IList<Wishlist> GetUserList(int UserID)
        {
            return (from a in context.Wishlists where a.UserId == UserID select a).ToList();
        }

        public bool IsInWishList(int UserId, int AppId)
        {
            IList<Wishlist> wishs = (from w in context.Wishlists where w.UserId == UserId && w.ApplicationId == AppId select w).ToList();
            bool b = false;
            if (wishs.Count > 0)
            {
                b = true;
            }
            return b;
        }
        public Wishlist GetWishItem(int UserId, int AppId)
        {
            return (from w in context.Wishlists where w.UserId == UserId && w.ApplicationId == AppId select w).FirstOrDefault();
        }
    }
}
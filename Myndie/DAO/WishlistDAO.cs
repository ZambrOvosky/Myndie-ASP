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

        public void Add(Wishlist w)
        {
            context.Wishlists.Add(w);
            Update();
        }
        public IList<Wishlist> List()
        {
            return context.Wishlists.ToList();
        }
        public void Remove(Wishlist app)
        {
            context.Wishlists.Remove(app);
            Update();
        }
        public IList<Wishlist> GetUserList(int UserID)
        {
            return (from a in context.Wishlists where a.UserId == UserID select a).ToList();
        }
    }
}
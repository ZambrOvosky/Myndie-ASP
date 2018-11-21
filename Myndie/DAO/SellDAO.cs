using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class SellDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Sell sell)
        {
            context.Sells.Add(sell);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Sell> List()
        {
            return context.Sells.ToList();
        }

        public Sell SearchById(int id)
        {
            return context.Sells.FirstOrDefault(s => s.Id == id);
        }

        public void Remove(Sell sell)
        {
            context.Sells.Remove(sell);
            Update();
        }

        public IList<Sell> GetUserSells(int id)
        {
            return (from s in context.Sells where s.UserId == id select s).ToList();
        }
    }
}
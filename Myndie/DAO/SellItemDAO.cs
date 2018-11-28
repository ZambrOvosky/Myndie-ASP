using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class SellItemDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(SellItem sellit)
        {
            context.SellItems.Add(sellit);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<SellItem> List()
        {
            return context.SellItems.ToList();
        }

        public SellItem SearchById(int id)
        {
            return context.SellItems.FirstOrDefault(si => si.Id == id);
        }

        public void Remove(SellItem sellit)
        {
            context.SellItems.Remove(sellit);
            Update();
        }

        public IList<SellItem> GetUserApps(int userid)
        {
            IList<Sell> sells = (from s in context.Sells where s.UserId == userid select s).ToList();
            IList<SellItem> sitem = new List<SellItem>();
            foreach (var s in sells)
            {
                IList<SellItem> si2 = (from si in context.SellItems where si.SellId == s.Id select si).ToList();
                foreach(var si in si2)
                {
                    sitem.Add(si);
                }
            }
            return sitem;
        }

        public bool SearchUserApp(int Uid, int Aid)
        {
            dynamic x = (from si in context.SellItems join sell in context.Sells on si.SellId equals sell.Id where si.ApplicationId == Aid && sell.UserId == Uid select new { Id = si.Id, PriceItem = si.PriceItem, ApplicationId = si.ApplicationId, SellId = si.SellId}).ToList();
            bool r = false;
            if (x.Count != 0)
                 r = true;
            return r;

        }
    }
}
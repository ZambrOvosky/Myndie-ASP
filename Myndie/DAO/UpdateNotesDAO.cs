using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class UpdateNotesDAO
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
            return context.SellItems.FirstOrDefault(p => p.Id == id);
        }

        public void Remove(SellItem sellit)
        {
            context.SellItems.Remove(sellit);
            Update();
        }
    }
}
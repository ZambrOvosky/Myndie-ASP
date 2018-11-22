using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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

        public IList<Sell> Get7DaysSells()
        {
            //IList<Sell> a = (from s in context.Sells select s).Count(s => s.Id);
            //var a = context.Sells.OrderBy(s => s.Date).GroupBy(s => s.Date.Date);
            //var res = (from s in context.Sells where s.Date.Year.Equals(DateTime.Now.Year) && s.Date.Month.Equals(DateTime.Now.Month) && (s.Date.Day >= (DateTime.Now.Day - 7) && s.Date.Day <= DateTime.Now.Day)
            //           group s by s.Date.Day orderby s.Date descending select s).ToList(); //TotalPrice = g.Sum(x => x.TotalPrice)
            //var period = (from s in context.Sells select s).GroupBy(s => s.Id).OrderBy(s => s.Date).ToList();

            var list = (from s in context.Sells where s.Date.Year.Equals(DateTime.Now.Year) && s.Date.Month.Equals(DateTime.Now.Month) && (s.Date.Day >= (DateTime.Now.Day - 7) && s.Date.Day <= DateTime.Now.Day) orderby s.Date descending select s).ToList();
            foreach(var i in list)
            {
                i.Date = i.Date.Date;
            }
            var sells = (from s in list select s).GroupBy(s => s.Date).Select(g => new { Date = g.Key, TotalPrice = g.Sum(s => s.TotalPrice), Count = g.Count()}).ToList();
            return new List<Sell>();
        }
    }
}
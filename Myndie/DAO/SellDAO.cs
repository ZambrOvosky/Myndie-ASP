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

        public dynamic Get7DaysSells()
        {
            //IList<Sell> a = (from s in context.Sells select s).Count(s => s.Id);
            //var a = context.Sells.OrderBy(s => s.Date).GroupBy(s => s.Date.Date);
            //var res = (from s in context.Sells where s.Date.Year.Equals(DateTime.Now.Year) && s.Date.Month.Equals(DateTime.Now.Month) && (s.Date.Day >= (DateTime.Now.Day - 7) && s.Date.Day <= DateTime.Now.Day)
            //           group s by s.Date.Day orderby s.Date descending select s).ToList(); //TotalPrice = g.Sum(x => x.TotalPrice)
            //var period = (from s in context.Sells select s).GroupBy(s => s.Id).OrderBy(s => s.Date).ToList();
            
            //WHERE = s.Date.Year >= max.Year && s.Date.Year <= DateTime.Now.Year
                        //&& s.Date.Month >= max.Month && s.Date.Month <= DateTime.Now.Month
                        //&& (s.Date.Day >= max.Day)
                        //&& s.Date.Day <= DateTime.Now.Day

            DateTime max = DateTime.Now.AddDays(-7);
            var list = (from s in context.Sells where s.Date >= max && s.Date <= DateTime.Now
                        orderby s.Date descending select s).ToList();
            //foreach(var i in list)
            //{
            //    i.Date = i.Date.Date;
            //}
            var sells = (from s in list select s).GroupBy(s => s.Date.Date).Select(g => new { Date = g.Key.Day, TotalPrice = g.Sum(s => s.TotalPrice), Count = g.Count()}).ToList();
            return sells;
        }

        public dynamic GetMonthlySells()
        {
            var list = (from s in context.Sells where (s.Date.Month <= (DateTime.Now.Month) && s.Date.Year.Equals(DateTime.Now.Year)) || (s.Date.Month >= DateTime.Now.Month && s.Date.Year.Equals(DateTime.Now.Year - 1)) orderby s.Date descending select s).ToList();
            var sells = (from s in list select s).GroupBy(s => s.Date.Month).Select(g => new { Date = g.Key, TotalPrice = g.Sum(s => s.TotalPrice)}).ToList();
            return sells;
        }

        public dynamic GetMonthlySellsLY()
        {
            var list = (from s in context.Sells where (s.Date.Month <= (DateTime.Now.Month) && s.Date.Year.Equals(DateTime.Now.Year-1)) || (s.Date.Month >= DateTime.Now.Month && s.Date.Year.Equals(DateTime.Now.Year - 2)) orderby s.Date descending select s).ToList();
            var sells = (from s in list select s).GroupBy(s => s.Date.Month).Select(g => new { Date = g.Key, TotalPrice = g.Sum(s => s.TotalPrice) }).ToList();
            return sells;
        }

        public dynamic DevGet7DaysSells(int id)
        {
            var sell = (from si in context.SellItems join a in context.Applications on si.ApplicationId equals a.Id join s in context.Sells on si.SellId equals s.Id where a.DeveloperId == id select s).ToList();
           
            DateTime max = DateTime.Now.AddDays(-7);
            var list = (from s in sell
                        where s.Date >= max && s.Date <= DateTime.Now
                        orderby s.Date descending
                        select s).ToList();
            var sells = (from s in list select s).GroupBy(s => s.Date.Date).Select(g => new { Date = g.Key.Day, TotalPrice = g.Sum(s => s.TotalPrice), Count = g.Count() }).ToList();
            return sells;
        }
    }
}
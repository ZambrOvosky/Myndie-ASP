using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class ApplicationDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Application app)
        {
            context.Applications.Add(app);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Application> List()
        {
            return context.Applications.ToList();
        }

        public IList<Application> ListLast10()
        {
            return (from a in context.Applications where a.Approved == 1 select a).OrderByDescending(a => a.Id).Take(10).ToList();
        }

        public IList<Application> ListTop10()
        {
            var si = (from s in context.SellItems select s).GroupBy(s => s.ApplicationId).Select(s => new { Count = s.Count(), Id = s.Key }).OrderByDescending(s => s.Count).Take(10).ToList();
            IList<Application> apps = new List<Application>();
            foreach (var s in si)
            {
                apps.Add(SearchById(s.Id));
            }
            return apps;
        }

        public Application SearchById(int id)
        {
            return context.Applications.FirstOrDefault(a => a.Id == id);
        }

        public void Remove(Application app)
        {
            context.Applications.Remove(app);
            Update();
        }

        public Application IsUnique(Application app)
        {
            return context.Applications.FirstOrDefault(a => a.Name == app.Name);
        }

        public IList<Application> GetDevGames(int devId)
        {
            return (from a in context.Applications where a.DeveloperId == devId select a).ToList();
        }

        public Application GetDevLastGame(int devId)
        {
            return (from a in context.Applications where a.DeveloperId == devId select a).OrderByDescending(a => a.Id).FirstOrDefault();
            //return context.Applications.OrderBy(descending).FirstOrDefault(a => a.DeveloperId == devId);
        }

        public IList<Application> Search(string s)
        {
            return (from a in context.Applications where a.Name.Contains(s) && a.Approved == 1 select a).ToList();
        }

        public IList<Application> SearchByType(int id)
        {
            return (from a in context.Applications where a.TypeAppId == id && a.Approved == 1 select a).ToList();
        }

        public IList<Application> AppsToApprove()
        {
            return (from a in context.Applications where a.Approved == 0 select a).ToList();
        }

        public IList<Application> GetAppsInLibrary(IList<SellItem> sis)
        {
            IList<Application> Apps = new List<Application>();
            foreach(var si in sis)
            {
                Apps.Add(SearchById(si.ApplicationId));
            }
            return Apps;
        }

        public IList<Application> GetTop5()
        {
            var si = (from s in context.SellItems select s).GroupBy(s => s.ApplicationId).Select(s => new { Count = s.Count(), Id = s.Key }).OrderByDescending(s => s.Count).Take(5).ToList();
            IList <Application> apps = new List<Application>();
            foreach (var s in si)
            {
                apps.Add(SearchById(s.Id));
            }
            return apps;
        }

        public IList<Application> GetTopApps()
        {
            var si = (from s in context.SellItems select s).GroupBy(s => s.ApplicationId).Select(s => new { Count = s.Count(), Id = s.Key }).OrderByDescending(s => s.Count).ToList();
            IList<Application> apps = new List<Application>();
            foreach (var s in si)
            {
                apps.Add(SearchById(s.Id));
            }
            return apps;
        }

        public IList<Application> GetFreeApps()
        {
            return (from a in context.Applications where a.Price == 0 && a.Approved == 1 select a).ToList();
        }

        public IList<Application> GetGamesMonth()
        {
            DateTime max = DateTime.Now.AddDays(-30);
            var type = (from t in context.TypeApps where t.Name == "Game" select t).FirstOrDefault();
            return (from a in context.Applications where a.TypeAppId == type.Id && a.ReleaseDate >= max select a).ToList();
        }

        public IList<Application> GetSoftwareMonth()
        {
            DateTime max = DateTime.Now.AddDays(-30);
            var type = (from t in context.TypeApps where t.Name == "Software" select t).FirstOrDefault();
            return (from a in context.Applications where a.TypeAppId == type.Id && a.ReleaseDate >= max select a).ToList();
        }

        public IList<Application> GetMobileMonth()
        {
            DateTime max = DateTime.Now.AddDays(-30);
            var type = (from t in context.TypeApps where t.Name == "Mobile" select t).FirstOrDefault();
            return (from a in context.Applications where a.TypeAppId == type.Id && a.ReleaseDate >= max select a).ToList();
        }

        public IList<string> GetAppsName()
        {
            return (from a in context.Applications where a.Approved == 1 select a.Name).ToList();
        }
    }
}
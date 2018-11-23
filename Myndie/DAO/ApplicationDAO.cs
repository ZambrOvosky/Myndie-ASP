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
            return (from a in context.Applications select a).OrderByDescending(a => a.Id).Take(10).ToList();
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
            return (from a in context.Applications where a.Name.Contains(s) select a).ToList();
        }

        public IList<Application> SearchByType(int id)
        {
            return (from a in context.Applications where a.TypeAppId == id select a).ToList();
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
    }
}
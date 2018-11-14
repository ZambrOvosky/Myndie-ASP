using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class ApplicationGenreDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(ApplicationGenre app)
        {
            context.ApplicationGenres.Add(app);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<ApplicationGenre> List()
        {
            return context.ApplicationGenres.ToList();
        }

        public void Remove(ApplicationGenre app)
        {
            context.ApplicationGenres.Remove(app);
            Update();
        }
    }
}
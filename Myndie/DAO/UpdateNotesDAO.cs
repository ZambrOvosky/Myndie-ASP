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

        public void Add(UpdateNotes update)
        {
            context.Updates.Add(update);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<UpdateNotes> List()
        {
            return context.Updates.ToList();
        }

        public UpdateNotes SearchById(int id)
        {
            return context.Updates.FirstOrDefault(up => up.Id == id);
        }

        public void Remove(UpdateNotes update)
        {
            context.Updates.Remove(update);
            Update();
        }

        public IList<UpdateNotes> GetLast3()
        {
            return (from u in context.Updates orderby u.Id descending select u).Take(3).ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class ModeratorDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Moderator mod)
        {
            context.Moderators.Add(mod);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Moderator> List()
        {
            return context.Moderators.ToList();
        }

        public Moderator SearchById(int id)
        {
            return context.Moderators.FirstOrDefault(m => m.Id == id);
        }

        public void Remove(Moderator mod)
        {
            context.Moderators.Remove(mod);
            Update();
        }

        public Moderator SearchByUserId(int id)
        {
            return context.Moderators.FirstOrDefault(m => m.UserId == id);
        }
    }
}
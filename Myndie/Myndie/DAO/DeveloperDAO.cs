using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class DeveloperDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Developer dev)
        {
            context.Developers.Add(dev);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Developer> List()
        {
            return context.Developers.ToList();
        }

        public Developer SearchById(int id)
        {
            return context.Developers.FirstOrDefault(d => d.Id == id);
        }

        public void Remove(Developer dev)
        {
            context.Developers.Remove(dev);
            Update();
        }

        public Developer AttachDevUser(Developer dev)
        {
            return context.Developers.OrderByDescending(d => d.Id).FirstOrDefault(d => d.Name == dev.Name && d.Info == dev.Info);
        }
    }
}
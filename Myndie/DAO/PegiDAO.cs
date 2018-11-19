using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class PegiDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Pegi pegi)
        {
            context.Pegis.Add(pegi);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Pegi> List()
        {
            return context.Pegis.ToList();
        }

        public Pegi SearchById(int id)
        {
            return context.Pegis.FirstOrDefault(p => p.Id == id);
        }

        public void Remove(Pegi pegi)
        {
            context.Pegis.Remove(pegi);
            Update();
        }

        public bool IsUnique(Pegi pegi)
        {
            try
            {
                Pegi peg = context.Pegis.FirstOrDefault(p => p.Age == pegi.Age);
                if (peg != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
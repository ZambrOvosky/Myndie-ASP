using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class CountryDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Country cou)
        {
            context.Countries.Add(cou);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Country> List()
        {
            return context.Countries.ToList();
        }

        public Country SearchById(int id)
        {
            return context.Countries.FirstOrDefault(c => c.Id == id);
        }

        public void Remove(Country cou)
        {
            context.Countries.Remove(cou);
            Update();
        }

        public bool IsUnique(Country con)
        {
            try
            {
                Country cont = context.Countries.FirstOrDefault(c => c.Name == con.Name);
                if (cont != null)
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
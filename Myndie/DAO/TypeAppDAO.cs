using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class TypeAppDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(TypeApp type)
        {
            context.TypeApps.Add(type);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<TypeApp> List()
        {
            return context.TypeApps.ToList();
        }

        public TypeApp SearchById(int id)
        {
            return context.TypeApps.FirstOrDefault(t => t.Id == id);
        }

        public void Remove(TypeApp type)
        {
            context.TypeApps.Remove(type);
            Update();
        }

        public bool IsUnique(TypeApp type)
        {
            try
            {
                TypeApp tp = context.TypeApps.FirstOrDefault(t => t.Name == type.Name);
                if (tp != null)
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class LanguageDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Language lan)
        {
            context.Languages.Add(lan);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Language> List()
        {
            return context.Languages.ToList();
        }

        public Language SearchById(int id)
        {
            return context.Languages.FirstOrDefault(l => l.Id == id);
        }

        public void Remove(Language lan)
        {
            context.Languages.Remove(lan);
            Update();
        }

        public bool IsUnique(Language lan)
        {
            try
            {
                Language lang = context.Languages.FirstOrDefault(l => l.Name == lan.Name);
                if (lang != null)
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
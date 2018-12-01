using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class GenreDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Genre gen)
        {
            context.Genres.Add(gen);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Genre> List()
        {
            return context.Genres.ToList();
        }

        public Genre SearchById(int id)
        {
            return context.Genres.FirstOrDefault(g => g.Id == id);
        }

        public void Remove(Genre gen)
        {
            context.Genres.Remove(gen);
            Update();
        }

        public bool IsUnique(Genre gen)
        {
            try
            {
                Genre gene = context.Genres.FirstOrDefault(g => g.Name == gen.Name);
                if (gene != null)
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

        public IList<Genre> GetTop10()
        {
            return (from g in context.Genres select g).Take(10).ToList();
        }

        public IList<Genre> ListId()
        {
            return (from g in context.Genres orderby g.Id select g).ToList();
        }
    }
}
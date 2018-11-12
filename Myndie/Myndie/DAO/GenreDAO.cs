﻿using System;
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
    }
}
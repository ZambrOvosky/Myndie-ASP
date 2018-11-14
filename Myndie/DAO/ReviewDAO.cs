﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class ReviewDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Review review)
        {
            context.Reviews.Add(review);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Review> List()
        {
            return context.Reviews.ToList();
        }

        public Review SearchById(int id)
        {
            return context.Reviews.FirstOrDefault(r => r.Id == id);
        }

        public void Remove(Review review)
        {
            context.Reviews.Remove(review);
            Update();
        }
    }
}
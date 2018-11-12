using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class CommentDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Comment com)
        {
            context.Comments.Add(com);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Comment> List()
        {
            return context.Comments.ToList();
        }

        public Comment SearchById(int id)
        {
            return context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void Remove(Comment com)
        {
            context.Comments.Remove(com);
            Update();
        }
    }
}
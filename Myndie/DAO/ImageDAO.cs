using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class ImageDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(Image img)
        {
            context.Images.Add(img);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<Image> List()
        {
            return context.Images.ToList();
        }

        public Image SearchById(int id)
        {
            return context.Images.FirstOrDefault(i => i.Id == id);
        }

        public void Remove(Image img)
        {
            context.Images.Remove(img);
            Update();
        }
    }
}
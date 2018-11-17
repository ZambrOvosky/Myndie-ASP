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

        public void Add(Image images)
        {
            context.Images.Add(images);
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

        public IList<Image> SearchAppImages(int appId)
        {
            return (from i in context.Images where i.ApplicationId == appId select i).ToList();
        }

        public Image SearchAppFirstImages(int appId)
        {
            return (from i in context.Images where i.ApplicationId == appId select i).FirstOrDefault();
        }

        public void Remove(Image img)
        {
            context.Images.Remove(img);
            Update();
        }
    }
}
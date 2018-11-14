using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO
{
    public class UserDAO
    {
        private EntityContext context = new EntityContext();

        public void Add(User user)
        {
            context.Users.Add(user);
            Update();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public IList<User> List()
        {
            return context.Users.ToList();
        }

        public User SearchById(int id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Remove(User user)
        {
            context.Users.Remove(user);
            Update();
        }

        public User Login(User user)
        {
            User us = context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            return us;
        }
    }
}
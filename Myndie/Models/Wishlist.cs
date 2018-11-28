using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myndie.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public virtual User users  { get; set; }
        public virtual Application applications  { get; set; }
    }
}
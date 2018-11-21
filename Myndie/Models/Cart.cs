using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myndie.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required]
        public int ApplicationId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public double Price { get; set; }

        public virtual Application Applications { get; set; }
        public virtual User Users { get; set; }
    }
}
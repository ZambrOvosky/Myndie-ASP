using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Myndie.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Value{ get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public virtual User Users { get; set; }
        public virtual Application Applications { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Myndie.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public int Value { get; set; }
        [Required, StringLength(512)]
        public string Desc { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required, StringLength(10)]
        public string Version { get; set; }
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public virtual Application Applications { get; set; }
        public virtual User Users { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Myndie.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        [Index(IsUnique = true)]
        public string Url { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ApplicationId { get; set; }
        public virtual User Users { get; set; }
        public virtual Application Applications { get; set; }
    }
}
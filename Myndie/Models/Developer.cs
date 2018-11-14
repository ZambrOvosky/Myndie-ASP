using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Myndie.Models
{
    public class Developer
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Info { get; set; }
        [Required]
        public int NumSoft{ get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}
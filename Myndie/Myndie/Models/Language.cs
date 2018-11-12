using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Myndie.Models
{
    public class Language
    {
        public int Id { get; set; }
        [Required, StringLength(45)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Myndie.Models
{
    public class Pegi
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        [Index(IsUnique = true)]
        public string PhotoPath { get; set; }
        [Required, StringLength(2)]
        public string Age { get; set; }
        [Required, StringLength(45)]
        public string Desc { get; set; }
    }
}
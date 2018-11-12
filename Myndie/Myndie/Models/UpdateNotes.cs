using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Myndie.Models
{
    public class UpdateNotes
    {
        public int Id { get; set; }
        [Required, StringLength(512)]
        public string Value { get; set; }
        public int ApplicationId { get; set; }
        public virtual Application Applications { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Myndie.Models
{
    public class Moderator
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
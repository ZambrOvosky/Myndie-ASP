using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Myndie.Models
{
    public class ApplicationGenre
    {
        public int Id { get; set; }
        [Required]
        public int ApplicationId { get; set; }
        [Required]
        public int GenreId { get; set; }
        public virtual Application Applications { get; set; }
        public virtual Genre Genres { get; set; }
    }
}
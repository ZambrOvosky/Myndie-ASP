using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myndie.Models;
using System.ComponentModel.DataAnnotations;

namespace Myndie.Models {
	public class User {
		public int Id { get; set; }
        [Required, StringLength(25)]
		public string Username { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [Required, StringLength(256)]
        public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public string Picture { get; set; }
        [Required]
		public DateTime CrtDate { get; set; }
		//FKs:
		public int CountryId { get; set; }
		public int LanguageId { get; set; }
        public int? DeveloperId { get; set; }
        public int? ModeratorId { get; set; }
        public virtual Country Countries { get; set; }
        public virtual Language Languages { get; set; }
        public virtual Developer Developers { get; set; }
        public virtual Moderator Moderators { get; set; }
    }
}
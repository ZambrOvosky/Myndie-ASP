using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Myndie.Models {
    public class Application {
        public int Id { get; set; }
        [Required,StringLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Required, StringLength(500)]
        public string Desc { get; set; }
        [Required, StringLength(10)]
        public string Version { get; set; }
        [Required]
        public double Price { get; set; }
        [Required, StringLength(50)]
        public string PublisherName { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [StringLength(255)]
        public string Archive { get; set; }
        public int Approved { get; set; }
        public string ImageUrl { get; set; }
        public int Value { get; set; }
        //FKs
        public int DeveloperId { get; set; }
        public int TypeAppId { get; set; }
        public int PegiId { get; set; }
        public int? ModeratorId { get; set; }
        public virtual Developer Developers { get; set; }
        public virtual TypeApp TypeApps { get; set; }
        public virtual Pegi Pegis { get; set; }
        public virtual Moderator Moderators { get; set; }
    }
}
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Myndie.Models;

namespace Myndie.DAO {

	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	public class EntityContext : DbContext {

		//https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html  ----> LEVE ISSO PARA VIDA!
	    public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationGenre> ApplicationGenres { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Pegi> Pegis { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<SellItem> SellItems { get; set; }
        public DbSet<TypeApp> TypeApps { get; set; }
        public DbSet<UpdateNotes> Updates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }


        public EntityContext() : base("dbMyndie") { }
		public EntityContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Application>();
            modelBuilder.Entity<ApplicationGenre>();
            modelBuilder.Entity<Cart>();
            modelBuilder.Entity<Comment>();
            modelBuilder.Entity<Country>();
            modelBuilder.Entity<Developer>();
            modelBuilder.Entity<Genre>();
            modelBuilder.Entity<Image>();
            modelBuilder.Entity<Language>();
            modelBuilder.Entity<Moderator>();
            modelBuilder.Entity<Pegi>();
            modelBuilder.Entity<Review>();
            modelBuilder.Entity<Sell>();
            modelBuilder.Entity<SellItem>();
            modelBuilder.Entity<TypeApp>();
            modelBuilder.Entity<UpdateNotes>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Wishlist>();
            //Optionals
            modelBuilder.Entity<Application>().Property(a => a.ModeratorId).IsOptional();
            modelBuilder.Entity<User>().Property(u => u.DeveloperId).IsOptional();
            modelBuilder.Entity<User>().Property(u => u.ModeratorId).IsOptional();


			base.OnModelCreating(modelBuilder);
		}

	}
}
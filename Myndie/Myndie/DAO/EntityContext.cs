using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Myndie.DAO {

	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	public class EntityContext : DbContext {

		//https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html  ----> LEVE ISSO PARA VIDA!
		//public DbSet<User> Users { get; set; }
		//public DbSet<ProdCategory> Categorys { get; set; }
		//public DbSet<Product> Products { get; set; }

		public EntityContext() : base("dbMyndie") { }
		public EntityContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			base.OnModelCreating(modelBuilder);
		}

	}
}
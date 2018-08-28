using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myndie.Models {
	public class User {
		public int Id { get; set; }
		public int Login { get; set; }
		public int Name { get; set; }
		public int Pass { get; set; }
		public int Email { get; set; }
		public int BornDate { get; set; }
		public int Pic { get; set; }
		public int CrtDate { get; set; }
		//FKs:
		public int IdCountry { get; set; }
		public int IdLang { get; set; }
		public int IdDev { get; set; }
		public int IdMod { get; set; }
	}
}
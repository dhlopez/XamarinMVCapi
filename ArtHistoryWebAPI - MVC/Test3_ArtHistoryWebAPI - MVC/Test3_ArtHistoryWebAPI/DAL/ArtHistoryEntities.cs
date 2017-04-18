using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test3_ArtHistory.Models;

namespace Test3_ArtHistory.DAL
{
    public class ArtHistoryEntities : DbContext
    {
        public ArtHistoryEntities()
            : base("name=ArtHistoryEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Movement> Movements { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //This option keeps table names in singular form, my personal preference.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }
}

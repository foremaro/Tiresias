using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Tiresias.Models
{
    public class TiresiasDBContext: DbContext
    {
        public TiresiasDBContext() : base("tiresiasdbConnectionString")
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Metadata> MetaData { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
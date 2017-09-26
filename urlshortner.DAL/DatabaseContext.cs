using urlshortner.Entities.Models;
using System.Data.Entity;
using urlshortner.DAL.Config;

namespace urlshortner.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<URLModel> Links { get; set; }
        public DbSet<UserModel> Users { get; set; }

        public DatabaseContext() : base("DatabaseContext") { }

        static DatabaseContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, urlshortner.DAL.Migrations.Configuration>());
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new URLModelConfig());
            modelBuilder.Configurations.Add(new UserModelConfig());
        }
    }
}

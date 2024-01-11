using Microsoft.EntityFrameworkCore;
using twoday_Internship_Task.Database.DatabaseModels;

namespace twoday_Internship_Task.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public DbSet<EnclosureObject> EnclosureObjects { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enclosure>().HasMany(x => x.Objects).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}

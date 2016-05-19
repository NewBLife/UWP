using Microsoft.Data.Entity;

namespace EfSqliteUwpDemo.Models
{
    public class SensorDbContext : DbContext
    {
        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<Ambience> AmbientDataSample { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Sensors.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sensor>()
                .Property(b => b.SensorId)
                .IsRequired();

            modelBuilder.Entity<Ambience>()
                .Property(b => b.AmbienceId)
                .IsRequired();
        }
    }
}

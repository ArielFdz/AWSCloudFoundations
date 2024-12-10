using AWSCloudFoundations.Controllers;
using Microsoft.EntityFrameworkCore;

namespace AWSCloudFoundations.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>()
                .HasKey(a => a.id);

            modelBuilder.Entity<Alumno>()
                .Property(a => a.id)
                .ValueGeneratedOnAdd(); // Configura el incremento automático

            modelBuilder.Entity<Profesor>()
               .HasKey(a => a.Id);

            modelBuilder.Entity<Profesor>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}

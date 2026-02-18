using Microsoft.EntityFrameworkCore;
using TaxiApp.Models;

namespace TaxiApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Taxis> Taxis { get; set; }
        public DbSet<RegistroDiario> RegistrosDiarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Taxis>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Taxis)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegistroDiario>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.RegistrosDiarios)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegistroDiario>()
                .HasOne(r => r.Taxi)
                .WithMany(t => t.RegistrosDiarios)
                .HasForeignKey(r => r.TaxiId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using _2026_1C_Estacionamiento.Models;
using Microsoft.EntityFrameworkCore;

namespace _2026_1C_Estacionamiento.Data
{
    public class EstacionamientoContext : DbContext
    {
        public EstacionamientoContext(DbContextOptions options) : base(options)
        {
                
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<_2026_1C_Estacionamiento.Models.ClienteVehiculo> ClienteVehiculo { get; set; }
        public DbSet<_2026_1C_Estacionamiento.Models.Cliente> Cliente { get; set; }
        public DbSet<_2026_1C_Estacionamiento.Models.Empleado> Empleado { get; set; }
        public DbSet<_2026_1C_Estacionamiento.Models.Telefono> Telefono { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relación muchos a muchos
            modelBuilder.Entity<ClienteVehiculo>()
                .HasOne(cv => cv.Cliente)
                .WithMany(c => c.ClienteVehiculos)
                .HasForeignKey(cv => cv.ClienteId);

            modelBuilder.Entity<ClienteVehiculo>()
                .HasOne(cv => cv.Vehiculo)
                .WithMany(v => v.ClienteVehiculos)
                .HasForeignKey(cv => cv.VehiculoId);

            // Índice único compuesto para evitar duplicados
            modelBuilder.Entity<ClienteVehiculo>()
                .HasIndex(cv => new { cv.ClienteId, cv.VehiculoId });
        }
    }
}

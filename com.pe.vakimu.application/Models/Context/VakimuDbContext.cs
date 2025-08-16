
using System.Data.Entity;
using System.Linq;
using com.pe.vakimu.application.Models.Entities;

namespace com.pe.vakimu.application.Models.Context
{
    public class VakimuDbContext : DbContext
    {
        public VakimuDbContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<InformacionNutricional> InformacionNutricional { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetallesOrden { get; set; }
        public DbSet<DireccionEntrega> DireccionesEntrega { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Primary keys
            modelBuilder.Entity<Producto>().HasKey(p => p.Id);
            modelBuilder.Entity<Cliente>().HasKey(c => c.Id);
            modelBuilder.Entity<Departamento>().HasKey(d => d.Id);
            modelBuilder.Entity<Sucursal>().HasKey(s => s.Id);
            modelBuilder.Entity<MetodoPago>().HasKey(s => s.Id);
            modelBuilder.Entity<Orden>().HasKey(o => o.Id);
            modelBuilder.Entity<DetalleOrden>().HasKey(o => o.Id);
            modelBuilder.Entity<DireccionEntrega>().HasKey(o => o.Id);
            modelBuilder.Entity<InformacionNutricional>().HasKey(i => i.IdProducto);
            
            modelBuilder.Entity<DetalleOrden>()
                .Property(d => d.Descuento)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DetalleOrden>()
                .Property(d => d.Subtotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DetalleOrden>()
                .Property(d => d.Total)
                .HasPrecision(10, 2);

            
            
            // Product - Infonutri
            // Configurar precisión decimal para Producto
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioOferta)
                .HasPrecision(10, 2);

            // Configurar precisión decimal para InformacionNutricional
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.EnergiaKcal).HasPrecision(6, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.GrasaTotal).HasPrecision(5, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.GrasaSaturada).HasPrecision(5, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.GrasaTrans).HasPrecision(5, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.ColesterolMg).HasPrecision(5, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.SodioMg).HasPrecision(6, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.CarbohidratosTotales).HasPrecision(5, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.FibraDietaria).HasPrecision(5, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.AzucaresTotales).HasPrecision(5, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.AzucaresAnhadidos).HasPrecision(5, 2);
            modelBuilder.Entity<InformacionNutricional>().Property(i => i.Proteina).HasPrecision(5, 2);

            // Relación uno a uno entre Producto e InformacionNutricional
            modelBuilder.Entity<Producto>()
                .HasOptional(p => p.InformacionNutricional)
                .WithRequired(n => n.Producto)
                .WillCascadeOnDelete(true);
            
            
            // Sucursal
            modelBuilder.Entity<Sucursal>()
                .Property(s => s.Longitud)
                .HasPrecision(9, 6);

            modelBuilder.Entity<Sucursal>()
                .Property(s => s.Latitud)
                .HasPrecision(9, 6);
        }
    }
}
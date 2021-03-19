using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace SuperCash.Models
{
    public partial class supercashContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public supercashContext()
        {
            //_configuration = configuration;
        }

        public supercashContext(DbContextOptions<supercashContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<NivelesDirecto> NivelesDirectos { get; set; }
        public virtual DbSet<NivelesEquipo> NivelesEquipos { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<Transaccione> Transacciones { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("trabajo"));                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<NivelesDirecto>(entity =>
            {
                entity.ToTable("niveles_directo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Ganancia).HasColumnName("ganancia");

                entity.Property(e => e.Nivel).HasColumnName("nivel");
            });

            modelBuilder.Entity<NivelesEquipo>(entity =>
            {
                entity.ToTable("niveles_equipos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Ganancia).HasColumnName("ganancia");

                entity.Property(e => e.Nivel).HasColumnName("nivel");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("pagos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdPadre).HasColumnName("id_padre");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.MontoTrx).HasColumnName("monto_trx");

                entity.Property(e => e.TipoPago)
                    .HasMaxLength(95)
                    .IsUnicode(false)
                    .HasColumnName("tipo_pago");
            });

            modelBuilder.Entity<Transaccione>(entity =>
            {
                entity.ToTable("transacciones");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado)
                    .HasMaxLength(95)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Monto).HasColumnName("monto");

                entity.Property(e => e.MontoTrx).HasColumnName("monto_trx");

                entity.Property(e => e.Id_transaccion).HasColumnName("id_transaccion");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Acceso).HasColumnName("acceso");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Clave)
                    .HasMaxLength(145)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Email)
                    .HasMaxLength(145)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecha_registro");

                entity.Property(e => e.GananciaDirecta).HasColumnName("ganancia_directa");

                entity.Property(e => e.GananciaEquipo).HasColumnName("ganancia_equipo");

                entity.Property(e => e.IdPadre).HasColumnName("id_padre");

                entity.Property(e => e.NivelDirecto).HasColumnName("nivel_directo");

                entity.Property(e => e.NivelEquipo).HasColumnName("nivel_equipo");

                entity.Property(e => e.PrimeraCompra).HasColumnName("primera_compra");

                entity.Property(e => e.Rango)
                    .HasMaxLength(95)
                    .IsUnicode(false)
                    .HasColumnName("rango");

                entity.Property(e => e.Wallet)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wallet");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperCash.Models;

namespace SuperCash.Migrations
{
    [DbContext(typeof(supercashContext))]
    [Migration("20210323172445_Cambios_transacciones")]
    partial class Cambios_transacciones
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SuperCash.Models.NivelesDirecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Costo")
                        .HasColumnType("int")
                        .HasColumnName("costo");

                    b.Property<int?>("Ganancia")
                        .HasColumnType("int")
                        .HasColumnName("ganancia");

                    b.Property<int?>("Nivel")
                        .HasColumnType("int")
                        .HasColumnName("nivel");

                    b.HasKey("Id");

                    b.ToTable("niveles_directo");
                });

            modelBuilder.Entity("SuperCash.Models.NivelesEquipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Costo")
                        .HasColumnType("int")
                        .HasColumnName("costo");

                    b.Property<int?>("Ganancia")
                        .HasColumnType("int")
                        .HasColumnName("ganancia");

                    b.Property<int?>("Nivel")
                        .HasColumnType("int")
                        .HasColumnName("nivel");

                    b.HasKey("Id");

                    b.ToTable("niveles_equipos");
                });

            modelBuilder.Entity("SuperCash.Models.Pago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha");

                    b.Property<int?>("IdPadre")
                        .HasColumnType("int")
                        .HasColumnName("id_padre");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.Property<double?>("MontoTrx")
                        .HasColumnType("float")
                        .HasColumnName("monto_trx");

                    b.Property<string>("TipoPago")
                        .HasMaxLength(95)
                        .IsUnicode(false)
                        .HasColumnType("varchar(95)")
                        .HasColumnName("tipo_pago");

                    b.HasKey("Id");

                    b.ToTable("pagos");
                });

            modelBuilder.Entity("SuperCash.Models.Transaccione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Depositado")
                        .HasColumnType("float")
                        .HasColumnName("depositado");

                    b.Property<string>("Estado")
                        .HasMaxLength(95)
                        .IsUnicode(false)
                        .HasColumnType("varchar(95)")
                        .HasColumnName("estado");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.Property<string>("Id_transaccion")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("id_transaccion");

                    b.Property<double>("Recibido")
                        .HasColumnType("float")
                        .HasColumnName("recibido");

                    b.Property<string>("TipoPago")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tipo_pago");

                    b.HasKey("Id");

                    b.ToTable("transacciones");
                });

            modelBuilder.Entity("SuperCash.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Acceso")
                        .HasColumnType("int")
                        .HasColumnName("acceso");

                    b.Property<double?>("Balance")
                        .HasColumnType("float")
                        .HasColumnName("balance");

                    b.Property<string>("Clave")
                        .HasMaxLength(145)
                        .IsUnicode(false)
                        .HasColumnType("varchar(145)")
                        .HasColumnName("clave");

                    b.Property<string>("Email")
                        .HasMaxLength(145)
                        .IsUnicode(false)
                        .HasColumnType("varchar(145)")
                        .HasColumnName("email");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("date")
                        .HasColumnName("fecha_registro");

                    b.Property<double?>("GananciaDirecta")
                        .HasColumnType("float")
                        .HasColumnName("ganancia_directa");

                    b.Property<double?>("GananciaEquipo")
                        .HasColumnType("float")
                        .HasColumnName("ganancia_equipo");

                    b.Property<int?>("IdPadre")
                        .HasColumnType("int")
                        .HasColumnName("id_padre");

                    b.Property<int?>("NivelDirecto")
                        .HasColumnType("int")
                        .HasColumnName("nivel_directo");

                    b.Property<int?>("NivelEquipo")
                        .HasColumnType("int")
                        .HasColumnName("nivel_equipo");

                    b.Property<int?>("PrimeraCompra")
                        .HasColumnType("int")
                        .HasColumnName("primera_compra");

                    b.Property<string>("Rango")
                        .HasMaxLength(95)
                        .IsUnicode(false)
                        .HasColumnType("varchar(95)")
                        .HasColumnName("rango");

                    b.Property<string>("Wallet")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("wallet");

                    b.HasKey("Id");

                    b.ToTable("usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}

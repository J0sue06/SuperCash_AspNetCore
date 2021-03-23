using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperCash.Migrations
{
    public partial class Cambios_transacciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "niveles_directo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nivel = table.Column<int>(type: "int", nullable: true),
                    ganancia = table.Column<int>(type: "int", nullable: true),
                    costo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_niveles_directo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "niveles_equipos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nivel = table.Column<int>(type: "int", nullable: true),
                    ganancia = table.Column<int>(type: "int", nullable: true),
                    costo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_niveles_equipos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pagos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    id_padre = table.Column<int>(type: "int", nullable: true),
                    monto_trx = table.Column<double>(type: "float", nullable: true),
                    tipo_pago = table.Column<string>(type: "varchar(95)", unicode: false, maxLength: 95, nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transacciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    depositado = table.Column<double>(type: "float", nullable: false),
                    recibido = table.Column<double>(type: "float", nullable: false),
                    tipo_pago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_transaccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    estado = table.Column<string>(type: "varchar(95)", unicode: false, maxLength: 95, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transacciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_padre = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "varchar(145)", unicode: false, maxLength: 145, nullable: true),
                    clave = table.Column<string>(type: "varchar(145)", unicode: false, maxLength: 145, nullable: true),
                    wallet = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ganancia_directa = table.Column<double>(type: "float", nullable: true),
                    ganancia_equipo = table.Column<double>(type: "float", nullable: true),
                    balance = table.Column<double>(type: "float", nullable: true),
                    rango = table.Column<string>(type: "varchar(95)", unicode: false, maxLength: 95, nullable: true),
                    nivel_directo = table.Column<int>(type: "int", nullable: true),
                    nivel_equipo = table.Column<int>(type: "int", nullable: true),
                    primera_compra = table.Column<int>(type: "int", nullable: true),
                    fecha_registro = table.Column<DateTime>(type: "date", nullable: true),
                    acceso = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "niveles_directo");

            migrationBuilder.DropTable(
                name: "niveles_equipos");

            migrationBuilder.DropTable(
                name: "pagos");

            migrationBuilder.DropTable(
                name: "transacciones");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}

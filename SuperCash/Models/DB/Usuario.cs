using System;
using System.Collections.Generic;

#nullable disable

namespace SuperCash.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public int? IdPadre { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string Wallet { get; set; }
        public double? GananciaDirecta { get; set; }
        public double? GananciaEquipo { get; set; }
        public double? Balance { get; set; }
        public string Rango { get; set; }
        public int? NivelDirecto { get; set; }
        public int? NivelEquipo { get; set; }
        public int? PrimeraCompra { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? Acceso { get; set; }
    }
}

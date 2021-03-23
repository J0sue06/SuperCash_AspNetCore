using System;
using System.Collections.Generic;

#nullable disable

namespace SuperCash.Models
{
    public partial class Transaccione
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public double Depositado { get; set; }
        public double Recibido { get; set; }
        public string TipoPago { get; set; }
        public string Id_transaccion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
    }
}

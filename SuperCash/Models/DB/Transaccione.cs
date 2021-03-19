using System;
using System.Collections.Generic;

#nullable disable

namespace SuperCash.Models
{
    public partial class Transaccione
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public string Monto { get; set; }
        public double? MontoTrx { get; set; }
        public string Id_transaccion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
    }
}

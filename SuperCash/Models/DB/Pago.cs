using System;
using System.Collections.Generic;

#nullable disable

namespace SuperCash.Models
{
    public partial class Pago
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdPadre { get; set; }
        public double? MontoTrx { get; set; }
        public string TipoPago { get; set; }
        public DateTime Fecha { get; set; }
    }
}

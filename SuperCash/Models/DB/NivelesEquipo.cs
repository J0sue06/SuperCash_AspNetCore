using System;
using System.Collections.Generic;

#nullable disable

namespace SuperCash.Models
{
    public partial class NivelesEquipo
    {
        public int Id { get; set; }
        public int? Nivel { get; set; }
        public int? Ganancia { get; set; }
        public int? Costo { get; set; }
    }
}

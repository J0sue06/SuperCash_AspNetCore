using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperCash.Helpers
{
    public class RetiroViewModel
    {
        [Required(ErrorMessage = "Debes introducir un valor")]        
        public int Retiro { get; set; }
    }
}

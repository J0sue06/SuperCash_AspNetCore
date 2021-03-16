using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperCash.Helpers
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El Id es obligatorio")]        
        public int ID { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria")]        
        public string Pass { get; set; }
    }
}

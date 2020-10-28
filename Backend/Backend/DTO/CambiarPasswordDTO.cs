using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO
{
    public class CambiarPasswordDTO
    {
        public string PasswordAnterior { get; set; }
        public string NuevaPassword { get; set; }
    }
}

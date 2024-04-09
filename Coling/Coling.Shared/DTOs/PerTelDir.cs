using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared.DTOs
{
    public class PerTelDir
    {
        public Persona personas { get; set; }
        public Telefono telefonos { get; set; }
        public Direccion direccions { get; set; }
        public RegistrarUsuario registrarUsuario { get; set; }
    }
}

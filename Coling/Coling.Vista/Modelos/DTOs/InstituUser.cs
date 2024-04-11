using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Modelos.DTOs
{
    public class InstituUser
    {
        public Institucion institucion { get; set; }
        public RegistrarUsuario registrarUsuario { get; set; }
    }
}

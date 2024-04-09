using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afilidados.DTOs
{
    public class PersonaTeleDirecUser
    {
        public Persona persona {  get; set; }
        public Telefono telefono { get; set; }
        public Direccion direccion { get; set; }
    }
}

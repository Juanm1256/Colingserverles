using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afilidados.DTOs
{
    public class MostarNombrePersonaDireccion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        // Otras propiedades de dirección...

        // Relación con Persona
        public int PersonaId { get; set; }
        public Persona persona { get; set; }

        // Propiedad calculada para el nombre completo de la persona
        public string NombreCompletoPersona => $"{persona.Nombre} {persona.Apellidos}";
    }
}

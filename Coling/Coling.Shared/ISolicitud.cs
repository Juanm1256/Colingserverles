using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface ISolicitud
    {
        public string? Afiliado { get; set; }
        public string? NombreCompleto { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string? PretencionSalarial { get; set; }
        public string? Acercade { get; set; }
        public string? Oferta { get; set; }
    }
}

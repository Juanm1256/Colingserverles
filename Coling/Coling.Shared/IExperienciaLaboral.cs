using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IExperienciaLaboral
    {
        public string Afiliado { get; set; }
        public string Institucion { get; set; }
        public string CargoTitulo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public string Estado { get; set; }
    }
}

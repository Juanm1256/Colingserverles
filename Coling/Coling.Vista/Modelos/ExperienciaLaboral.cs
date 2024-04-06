using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Modelos
{
    public class ExperienciaLaboral : IExperienciaLaboral
    {
        public string Afiliado { get; set; }
        public string Institucion { get; set; }
        public string CargoTitulo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public string Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public string ETag { get; set; }
    }
}

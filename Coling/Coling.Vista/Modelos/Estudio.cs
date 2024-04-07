using Azure;
using Coling.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Modelos
{
    public class Estudio : IEstudios
    {
        public string Tipo { get; set; }
        public string Afiliado { get; set; }
        public string Grado { get; set; }
        public string TituloRecibido { get; set; }
        public string Institucion { get; set; }
        public string Anio { get; set; }
        public string Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public string ETag { get; set; }
    }
}

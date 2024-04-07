using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.Shared;

namespace Coling.API.Bolsatrabajo.Modelo
{
    public class Solicitud : ISolicitud
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Afiliado { get; set; }
        public string? NombreCompleto { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string? PretencionSalarial { get; set; }
        public string? Acercade { get; set; }
        public string? Oferta { get; set; }
        public string? Estado { get; set; }
    }
}

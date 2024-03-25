using Coling.API.Bolsatrabajo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Contratos.Repositorio
{
    public interface IOfertaLaboralLogic
    {
        public Task<bool> Insertar(OfertaLaboral ofertaLaboral);
        public Task<List<OfertaLaboral>> getall();
        public Task<bool> UpdateIns(OfertaLaboral ofertaLaboral, string id);
        public Task<bool> Eliminar(string id);
        public Task<OfertaLaboral> ObtenerbyId(string id);
    }
}

using Coling.API.Bolsatrabajo.Modelo;
using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Contratos.Repositorio
{
    public interface ISolicitudLogic
    {
        public Task<bool> Insertar(Solicitud solicitud);
        public Task<List<Solicitud>> getall();
        public Task<List<Solicitud>> ListarSolicitudEstado();
        public Task<List<Solicitud>> ListarSolicitudPorNombre(string nombre);
        public Task<bool> UpdateIns(Solicitud solicitud, string id);
        public Task<bool> Eliminar(string id);
        public Task<Solicitud> ObtenerbyId(string id);
    }
}

using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Bolsatrabajo
{
    public interface ISolicitudService
    {
        Task<List<Solicitud>> Listar(string token);
        Task<List<Solicitud>> ListarEstado(string token);
        Task<List<Solicitud>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(Solicitud solicitud, string token);
        Task<bool> Modificar(Solicitud solicitud, string id, string token);
        Task<bool> Eliminar(string id, string token);
        Task<Solicitud> ObtenerPorId(string id, string token);

    }
}

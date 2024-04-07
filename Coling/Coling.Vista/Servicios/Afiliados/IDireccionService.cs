using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface IDireccionService
    {
        Task<List<Direccion>> ListarDireccion(string token);
        Task<List<Direccion>> ListarEstado(string token);
        Task<List<Direccion>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(Direccion direccion, string token);
        Task<bool> Modificar(Direccion direccions, int id, string token);
        Task<bool> Eliminar(int id, string token);
        Task<Direccion> ObtenerPorId(int id, string token);
    }
}

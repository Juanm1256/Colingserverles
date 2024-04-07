using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface ITelefonoService
    {
        Task<List<Telefono>> ListarTelefono(string token);
        Task<List<Telefono>> ListarEstado(string token);
        Task<List<Telefono>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(Telefono telefono, string token);
        Task<bool> Modificar(Telefono telefono, int id, string token);
        Task<bool> Eliminar(int id, string token);
        Task<Telefono> ObtenerPorId(int id, string token);
    }
}

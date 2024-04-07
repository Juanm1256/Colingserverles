using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface IAfiliadoService
    {
        Task<List<Afiliado>> ListarAfiliado(string token);
        Task<List<Afiliado>> ListarEstado(string token);
        Task<List<Afiliado>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(Afiliado afiliado, string token);
        Task<bool> Modificar(Afiliado afiliado, int id, string token);
        Task<bool> Eliminar(int id, string token);
        Task<Afiliado> ObtenerPorId(int id, string token);
    }
}

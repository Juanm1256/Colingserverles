using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface ITipoSocialService
    {
        Task<List<TipoSocial>> ListarTipoSocial(string token);
        Task<List<TipoSocial>> ListarEstado(string token);
        Task<List<TipoSocial>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(TipoSocial tipoSocial, string token);
        Task<bool> Modificar(TipoSocial tipoSocial, int id, string token);
        Task<bool> Eliminar(int id, string token);
        Task<TipoSocial> ObtenerPorId(int id, string token);
    }
}

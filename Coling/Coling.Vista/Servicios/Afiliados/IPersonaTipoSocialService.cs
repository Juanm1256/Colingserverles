using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface IPersonaTipoSocialService
    {
        Task<List<PersonaTipoSocial>> ListarPersonaTipoSocial(string token);
        Task<List<PersonaTipoSocial>> ListarEstado(string token);
        Task<List<PersonaTipoSocial>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(PersonaTipoSocial personaTipoSocial, string token);
        Task<bool> Modificar(PersonaTipoSocial personaTipoSocial, int id, string token);
        Task<bool> Eliminar(int id, string token);
        Task<PersonaTipoSocial> ObtenerPorId(int id, string token);
    }
}

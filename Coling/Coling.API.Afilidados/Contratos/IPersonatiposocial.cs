using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afilidados.Contratos
{
    public interface IPersonatiposocial
    {
        public Task<bool> InsertarPersonaTipoSocial(PersonaTipoSocial social);
        public Task<bool> ModificarPersonaTipoSocial(PersonaTipoSocial social, int id);
        public Task<bool> EliminarPersonaTipoSocial(int id);
        public Task<List<PersonaTipoSocial>> ListarPersonaTipoSocialTodos();
        public Task<List<PersonaTipoSocial>> ListarPersonaTipoSocialEstado();
        public Task<List<PersonaTipoSocial>> ListarPersonaTipoSocialPorNombre(string nombre);
        public Task<PersonaTipoSocial> ObtenerPersonaTipoSocialById(int id);
    }
}

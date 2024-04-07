using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afilidados.Contratos
{
    public interface ITelefonoLogic
    {
        public Task<bool> InsertarTelefono(Telefono telefono);
        public Task<bool> ModificarTelefono(Telefono telefono, int id);
        public Task<bool> EliminarTelefono(int id);
        public Task<List<Telefono>> ListarTelefonoTodos();
        public Task<List<Telefono>> ListarTelefonoEstado();
        public Task<List<Telefono>> ListarTelefonoPorNombre(string nombre);
        public Task<Telefono> ObtenerTelefonoById(int id);

    }
}

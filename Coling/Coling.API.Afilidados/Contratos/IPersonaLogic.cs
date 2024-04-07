using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afilidados.Contratos
{
    public interface IPersonaLogic
    {
        public Task<bool> InsertarPersona(Persona persona);
        public Task<bool> ModificarPersona(Persona persona, int id);
        public Task<bool> Eliminarersona(int id);
        public Task<List<Persona>> ListarPersonaTodos();
        public Task<List<Persona>> ListarPersonaEstado();
        public Task<List<Persona>> ListarPersonaPorNombre(string nombre);
        public Task<Persona> ObtenerPersonaById(int id);

    }
}

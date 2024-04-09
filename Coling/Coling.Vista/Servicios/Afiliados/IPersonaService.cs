using Coling.Shared;
using Coling.Shared.DTOs;
using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface IPersonaService
    {
        Task<List<Persona>> ListarPersona(string token);
        Task<List<Persona>> ListarEstado(string token);
        Task<List<Persona>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(Persona persona, string token);
        Task<bool> InsertarAll(PerTelDir registroperteldir, string token);
        Task<bool> Modificar(Persona personas, int id, string token);
        Task<bool> Eliminar(int id, string token);
        Task<Persona> ObtenerPorId(int id, string token);
    }
}

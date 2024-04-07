using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IProfesionService
    {
        Task<List<Profesion>> Listar(string token);
        Task<List<Profesion>> ListarEstado(string token);
        Task<List<Profesion>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(Profesion profesion, string token);
        Task<bool> Modificar(Profesion profesion, string token);
        Task<bool> Eliminar(string id, string token);
        Task<Profesion> ObtenerPorId(string id, string token);
    }
}

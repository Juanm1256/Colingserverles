using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IExperienciaLaboralService
    {
        Task<List<ExperienciaLaboral>> Listar(string token);
        Task<List<ExperienciaLaboral>> ListarEstado(string token);
        Task<bool> Insertar(ExperienciaLaboral experienciaLaboral, string token);
        Task<bool> Modificar(ExperienciaLaboral experienciaLaboral, string token);
        Task<bool> Eliminar(string id, string token);
        Task<ExperienciaLaboral> ObtenerPorId(string id, string token);
    }
}

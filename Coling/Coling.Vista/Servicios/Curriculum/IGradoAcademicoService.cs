using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IGradoAcademicoService
    {
        Task<List<GradoAcademico>> Listar(string token);
        Task<List<GradoAcademico>> ListarEstado(string token);
        Task<bool> Insertar(GradoAcademico gradoAcademico, string token);
        Task<bool> Modificar(GradoAcademico gradoAcademico, string token);
        Task<bool> Eliminar(string id, string token);
        Task<GradoAcademico> ObtenerPorId(string id, string token);
    }
}

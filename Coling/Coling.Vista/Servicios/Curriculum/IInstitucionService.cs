using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IInstitucionService
    {
        Task<List<Institucion>> ListarInstitucion(string token);
        Task<bool> InsertarInstitucion(Institucion institucion, string token);
        Task<bool> ModificarInstitucion(Institucion institucion, string token);
        Task<bool> EliminarInstitucion(string id, string token);
        Task<Institucion> ObtenerInstitucionPorId(string id, string token);
    }
}

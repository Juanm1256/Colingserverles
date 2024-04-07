using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IEstudioService
    {
        Task<List<Estudio>> Listar(string token);
        Task<List<Estudio>> ListarEstado(string token);
        Task<List<Estudio>> ListarPorNombre(string nombre, string token);
        Task<List<Institucion>> ListarPorNombreInstitucion(string nombre, string token);
        Task<bool> Insertar(Estudio estudio, string token);
        Task<bool> Modificar(Estudio estudio, string token);
        Task<bool> Eliminar(string id, string token);
        Task<Estudio> ObtenerPorId(string id, string token);
    }
}

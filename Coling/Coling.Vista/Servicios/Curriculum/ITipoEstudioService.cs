using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface ITipoEstudioService
    {
        Task<List<TipoEstudio>> Listar(string token);
        Task<List<TipoEstudio>> ListarEstado(string token);
        Task<List<TipoEstudio>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(TipoEstudio tipoEstudio, string token);
        Task<bool> Modificar(TipoEstudio tipoEstudio, string token);
        Task<bool> Eliminar(string id, string token);
        Task<TipoEstudio> ObtenerPorId(string id, string token);
    }
}

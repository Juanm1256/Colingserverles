using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Bolsatrabajo
{
    public interface IOfertaLaboralService
    {
        Task<List<OfertaLaboral>> Listar(string token);
        Task<List<OfertaLaboral>> ListarEstado(string token);
        Task<List<OfertaLaboral>> ListarPorNombre(string nombre, string token);
        Task<bool> Insertar(OfertaLaboral ofertaLaboral, string token);
        Task<bool> Modificar(OfertaLaboral ofertaLaboral, string id, string token);
        Task<bool> Eliminar(string id, string token);
        Task<OfertaLaboral> ObtenerPorId(string id, string token);
    }
}

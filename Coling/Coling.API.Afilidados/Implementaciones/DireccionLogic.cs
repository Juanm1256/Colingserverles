using Coling.API.Afilidados.Contratos;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afilidados.Implementaciones
{
    public class DireccionLogic : IDireccionLogic
    {
        private readonly Contexto contexto;

        public DireccionLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarDireccion(int id)
        {
            bool sw = false;
            Direccion direcicion = await contexto.Direccions.FirstOrDefaultAsync(x => x.Id == id);
            contexto.Direccions.Remove(direcicion);
            await contexto.SaveChangesAsync();
            if (direcicion != null)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarDireccion(Direccion direccion)
        {
            bool sq = false;
            contexto.Direccions.Add(direccion);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sq = true;
            }
            return sq;
        }

        public async Task<List<Direccion>> ListarDireccionTodos()
        {
            var listar = await contexto.Direccions.ToListAsync();
            return listar;
        }

        public async Task<List<Direccion>> ListarDireccionEstado()
        {
            var listar = await contexto.Direccions.ToListAsync();
            var respuesta = listar.Where(x => x.Estado == "Activo" || x.Estado == "Inactivo");
            return respuesta.ToList();
        }

        public async Task<List<Direccion>> ListarDireccionPorNombre(string nombre)
        {
            var listar = await contexto.Direccions.Where(x => x.IdPersonanav.Nombre == nombre).ToListAsync();
            return listar;
        }

        public async Task<bool> ModificarDireccion(Direccion direccion, int id)
        {
            bool sw = false;
            Direccion modificar = await contexto.Direccions.FirstOrDefaultAsync(x => x.Id == id);
            if (modificar != null)
            {
                modificar.Idpersona = direccion.Idpersona;
                modificar.Descripcion = direccion.Descripcion;
                modificar.Estado = direccion.Estado;

                await contexto.SaveChangesAsync();
                sw = true;
            }

            return sw;
        }

        public async Task<Direccion> ObtenerDireccionById(int id)
        {
            var personabyid = await contexto.Direccions.FirstOrDefaultAsync(x => x.Id == id);
            return personabyid;
        }
    }
}

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
    public class TipoSocialLogic : ITipoSocial
    {
        private readonly Contexto contexto;

        public TipoSocialLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarTipoSocial(int id)
        {
            bool sw = false;
            TipoSocial tipoSocial = await contexto.TipoSocials.FirstOrDefaultAsync(x => x.Id == id);
            contexto.TipoSocials.Remove(tipoSocial);
            await contexto.SaveChangesAsync();
            if (tipoSocial != null)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarTipoSocial(TipoSocial tipoSocial)
        {
            bool sq = false;
            contexto.TipoSocials.Add(tipoSocial);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sq = true;
            }
            return sq;
        }

        public async Task<List<TipoSocial>> ListarTipoSocialTodos()
        {
            var listar = await contexto.TipoSocials.ToListAsync();
            return listar;
        }

        public async Task<List<TipoSocial>> ListarTipoSocialEstado()
        {
            var listar = await contexto.TipoSocials.ToListAsync();
            var respuesta = listar.Where(x => x.Estado == "Activo" || x.Estado == "Inactivo");
            return respuesta.ToList();
        }

        public async Task<List<TipoSocial>> ListarTipoSocialEstadoActivo()
        {
            var listar = await contexto.TipoSocials.Where(x => x.Estado == "Activo").ToListAsync();
            return listar;
        }

        public async Task<List<TipoSocial>> ListarTipoSocialPorNombre(string nombre)
        {
            var listar = await contexto.TipoSocials.Where(x => x.NombreSocial == nombre).ToListAsync();
            return listar;
        }

        public async Task<bool> ModificarTipoSocial(TipoSocial tipoSocial, int id)
        {
            bool sw = false;
            TipoSocial modificar = await contexto.TipoSocials.FirstOrDefaultAsync(x => x.Id == id);
            if (modificar != null)
            {
                modificar.NombreSocial = tipoSocial.NombreSocial;
                modificar.Estado = tipoSocial.Estado;

                await contexto.SaveChangesAsync();
                sw = true;
            }

            return sw;
        }

        public async Task<TipoSocial> ObtenerTipoSocialById(int id)
        {
            var personabyid = await contexto.TipoSocials.FirstOrDefaultAsync(x => x.Id == id);
            return personabyid;
        }
    }
}

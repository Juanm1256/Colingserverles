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
    public class AfiliadoLogic : IAfiliadoLogic
    {
        private readonly Contexto contexto;

        public AfiliadoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarAfiliado(int id)
        {
            bool sw = false;
            Afiliado afiliado = await contexto.Afiliados.FirstOrDefaultAsync(x => x.Id == id);
            contexto.Afiliados.Remove(afiliado);
            await contexto.SaveChangesAsync();
            if (afiliado != null)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarAfiliado(Afiliado afiliado)
        {
            bool sq = false;
            contexto.Afiliados.Add(afiliado);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sq = true;
            }
            return sq;
        }

        public async Task<List<Afiliado>> ListarAfiliadosTodos()
        {
            var listar = await contexto.Afiliados.ToListAsync();
            return listar;
        }

        public async Task<List<Afiliado>> ListarAfiliadoEstado()
        {
            var listarstatus = await contexto.Afiliados.Include(d=>d.IdPersonanav).Where(x => x.Estado == "Activo" || x.Estado == "Inactivo").ToListAsync();
            return listarstatus;
        }

        public async Task<List<Afiliado>> ListarAfiliadoEstadoActivo()
        {
            var listarstatus = await contexto.Afiliados.Include(d => d.IdPersonanav).Where(x => x.Estado == "Activo").ToListAsync();
            return listarstatus;
        }

        public async Task<List<Afiliado>> ListarAfiliadoPorNombre(string nombre)
        {
            var listar =  await contexto.Afiliados.Include(d => d.IdPersonanav).Where(x => x.IdPersonanav.Nombre == nombre).ToListAsync();
            return listar;
        }


        public async Task<bool> ModificarAfiliado(Afiliado afiliado, int id)
        {
            bool sw = false;
            Afiliado modificar = await contexto.Afiliados.FirstOrDefaultAsync(x => x.Id == id);
            if (modificar != null)
            {
                modificar.Idpersona = afiliado.Idpersona;
                modificar.fechaafiliacion = afiliado.fechaafiliacion;
                modificar.CodigoAfiliado = afiliado.CodigoAfiliado;
                modificar.Nrotituloprovisional = afiliado.Nrotituloprovisional;
                modificar.Estado = afiliado.Estado;

                await contexto.SaveChangesAsync();
                sw = true;
            }

            return sw;
        }

        public async Task<Afiliado> ObtenerAfiliadoById(int id)
        {
            var personabyid = await contexto.Afiliados.FirstOrDefaultAsync(x => x.Id == id);
            return personabyid;
        }
    }
}

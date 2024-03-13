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
    public class ProfesionAfiliadoLogic : IProfesionAfiliado
    {
        private readonly Contexto contexto;

        public ProfesionAfiliadoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarProfesionAfiliado(int id)
        {
            bool sw = false;
            ProfesionAfiliado valors = await contexto.ProfesionAfiliados.FirstOrDefaultAsync(x => x.Id == id);
            contexto.ProfesionAfiliados.Remove(valors);
            await contexto.SaveChangesAsync();
            if (valors != null)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarProfesionAfiliado(ProfesionAfiliado profesionAfiliado)
        {
            bool sq = false;
            contexto.ProfesionAfiliados.Add(profesionAfiliado);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sq = true;
            }
            return sq;
        }

        public async Task<List<ProfesionAfiliado>> ListarProfesionAfiliadoTodos()
        {
            var listar = await contexto.ProfesionAfiliados.ToListAsync();
            return listar;
        }

        public async Task<bool> ModificaProfesionAfiliado(ProfesionAfiliado profesionAfiliado, int id)
        {
            bool sw = false;
            ProfesionAfiliado modificar = await contexto.ProfesionAfiliados.FirstOrDefaultAsync(x => x.Id == id);
            if (modificar != null)
            {
                modificar.Idafiliado = profesionAfiliado.Idafiliado;
                modificar.Idprofesion = profesionAfiliado.Idprofesion;
                modificar.fechaasignacion = profesionAfiliado.fechaasignacion;
                modificar.Nrosellosib = profesionAfiliado.Nrosellosib;
                modificar.Estado = profesionAfiliado.Estado;

                await contexto.SaveChangesAsync();
                sw = true;
            }

            return sw;
        }

        public async Task<ProfesionAfiliado> ObtenerProfesionAfiliadoById(int id)
        {
            var personabyid = await contexto.ProfesionAfiliados.FirstOrDefaultAsync(x => x.Id == id);
            return personabyid;
        }
    }
}

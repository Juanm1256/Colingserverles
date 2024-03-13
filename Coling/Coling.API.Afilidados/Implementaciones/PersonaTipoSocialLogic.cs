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
    public class PersonaTipoSocialLogic : IPersonatiposocial
    {
        private readonly Contexto contexto;

        public PersonaTipoSocialLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarPersonaTipoSocial(int id)
        {
            bool sw = false;
            PersonaTipoSocial valors = await contexto.PersonaTipoSocials.FirstOrDefaultAsync(x=> x.Id == id);
            contexto.PersonaTipoSocials.Remove(valors);
            await contexto.SaveChangesAsync();
            if (valors != null)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarPersonaTipoSocial(PersonaTipoSocial personaTipoSocial)
        {
            bool sq = false;
            contexto.PersonaTipoSocials.Add(personaTipoSocial);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sq = true;
            }
            return sq;
        }

        public async Task<List<PersonaTipoSocial>> ListarPersonaTipoSocialTodos()
        {
            var listar = await contexto.PersonaTipoSocials.ToListAsync();
            return listar;
        }

        public async Task<bool> ModificarPersonaTipoSocial(PersonaTipoSocial social, int id)
        {
            bool sw = false;
            PersonaTipoSocial modificar = await contexto.PersonaTipoSocials.FirstOrDefaultAsync(x => x.Id == id);
            if (modificar != null)
            {
                modificar.Idtiposocial = social.Idtiposocial;
                modificar.Idpersona = social.Idpersona;
                modificar.Estado = social.Estado;

                await contexto.SaveChangesAsync();
                sw = true;
            }

            return sw;
        }

        public async Task<PersonaTipoSocial> ObtenerPersonaTipoSocialById(int id)
        {
            var personabyid = await contexto.PersonaTipoSocials.FirstOrDefaultAsync(x => x.Id == id);
            return personabyid;
        }
    }
}

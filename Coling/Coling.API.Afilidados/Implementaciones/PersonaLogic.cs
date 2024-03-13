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
    public class PersonaLogic : IPersonaLogic
    {
        private readonly Contexto contexto;

        public PersonaLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> Eliminarersona(int id)
        {
            bool sw = false;
            Persona persona = await contexto.Personas.FirstOrDefaultAsync(x=> x.Id==id);
            contexto.Personas.Remove(persona);
            await contexto.SaveChangesAsync();
            if (persona != null)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarPersona(Persona persona)
        {
            bool sq = false;
            contexto.Personas.Add(persona);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sq = true;
            }
            return sq;
        }

        public async Task<List<Persona>> ListarPersonaTodos()
        {
            var listar = await contexto.Personas.ToListAsync();
            return listar;
        }

        public async Task<bool> ModificarPersona(Persona persona, int id)
        {
            bool sw = false;
            Persona modpersonas = await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
            if (modpersonas!=null)
            {
                modpersonas.Nombre = persona.Nombre;
                modpersonas.Apellidos = persona.Apellidos;
                modpersonas.FechaNacimiento = persona.FechaNacimiento;
                modpersonas.Foto = persona.Foto;
                modpersonas.Estado = persona.Estado;

                await contexto.SaveChangesAsync();
                sw = true;
            }

            return sw;
        }

        public async Task<Persona> ObtenerPersonaById(int id)
        {
            var personabyid = await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
            return personabyid;
        }
    }
}

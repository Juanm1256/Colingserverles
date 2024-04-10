using Coling.API.Afilidados.Contratos;
using Coling.API.Afilidados.DTOs;
using Coling.Shared;
using Coling.Shared.DTOs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
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
            try
            {
                bool sw = false;
                Persona persona = await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
                contexto.Personas.Remove(persona);
                await contexto.SaveChangesAsync();
                if (persona != null)
                {
                    sw = true;
                }
                return sw;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public async Task<int> InsertarAllPersona(PerTelDir all)
        {
            try
            {
                int id = 0;
                int response = 0;
                if (!all.personas.IsNullOrDefault())
                {
                    contexto.Personas.Add(all.personas);
                    response = await contexto.SaveChangesAsync();
                    id = all.personas.Id;
                }
                
                if (!all.telefonos.IsNullOrDefault())
                {
                    
                    all.telefonos.Idpersona = id;
                    contexto.Telefonos.Add(all.telefonos);
                    response = await contexto.SaveChangesAsync();
                }
                if (!all.direccions.IsNullOrDefault())
                {
                    all.direccions.Idpersona = id;
                    contexto.Direccions.Add(all.direccions);
                    response = await contexto.SaveChangesAsync();
                }
                if (response>0)
                {
                    return id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
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

        public async Task<List<Persona>> ListarPersonaEstado()
        {
            var listar = await contexto.Personas.ToListAsync();
            var respuesta = listar.Where(x => x.Estado == "Activo" || x.Estado == "Inactivo");
            return respuesta.ToList();
        }
        
        public async Task<List<Persona>> ListarPersonaEstadoActivo()
        {
            var listar = await contexto.Personas.ToListAsync();
            var respuesta = listar.Where(x => x.Estado == "Activo");
            return respuesta.ToList();
        }

        public async Task<List<Persona>> ListarPersonaPorNombre(string nombre)
        {
            var listar = await contexto.Personas.ToListAsync();
            var respuesta = listar.Where(x => x.Nombre == nombre );
            return respuesta.ToList();
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

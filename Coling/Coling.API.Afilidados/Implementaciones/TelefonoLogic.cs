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
    public class TelefonoLogic : ITelefonoLogic
    {
        private readonly Contexto contexto;

        public TelefonoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarTelefono(int id)
        {
            bool sw = false;
            Telefono valor = await contexto.Telefonos.FirstOrDefaultAsync(x => x.Id == id);
            contexto.Telefonos.Remove(valor);
            await contexto.SaveChangesAsync();
            if (valor != null)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarTelefono(Telefono telefono)
        {
            bool sq = false;
            contexto.Telefonos.Add(telefono);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sq = true;
            }
            return sq;
        }

        public async Task<List<Telefono>> ListarTelefonoTodos()
        {
            var listar = await contexto.Telefonos.ToListAsync();
            return listar;
        }

        public async Task<bool> ModificarTelefono(Telefono telefono, int id)
        {
            bool sw = false;
            Telefono modificar = await contexto.Telefonos.FirstOrDefaultAsync(x => x.Id == id);
            if (modificar != null)
            {
                modificar.Idpersona = telefono.Idpersona;
                modificar.nrotelefono = telefono.nrotelefono;
                modificar.Estado = telefono.Estado;

                await contexto.SaveChangesAsync();
                sw = true;
            }

            return sw;
        }

        public async Task<Telefono> ObtenerTelefonoById(int id)
        {
            var personabyid = await contexto.Telefonos.FirstOrDefaultAsync(x => x.Id == id);
            return personabyid;
        }
    }
}

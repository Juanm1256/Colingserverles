using Coling.API.Bolsatrabajo.Contratos.Repositorio;
using Coling.API.Bolsatrabajo.Modelo;
using Coling.Shared;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Repositorio
{
    public class OfertaLaboralRepositorio : IOfertaLaboralLogic
    {
        private readonly string? cadenaconexion;
        private readonly string? tablanombre;
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<OfertaLaboral> collection;

        public OfertaLaboralRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaconexion = configuration.GetSection("ClaveConexion").Value;
            var client = new MongoClient(cadenaconexion);
            var database = client.GetDatabase("TareaBD");
            collection = database.GetCollection<OfertaLaboral>("Oferta");
        }

        public async Task<bool> Eliminar(string id)
        {
            try
            {

                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var result = await collection.DeleteOneAsync(Builders<OfertaLaboral>.Filter.Eq("_id", objectId));
                    return result.DeletedCount == 1;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<OfertaLaboral>> getall()
        {
            List<OfertaLaboral> lista = new List<OfertaLaboral>();

            lista = await collection.Find(d => true).ToListAsync();
            return lista;

        }

        public async Task<List<OfertaLaboral>> ListarOfertaLaboralEstado()
        {
            var listar = await collection.FindAsync(d => d.Estado == "Activo" || d.Estado == "Inactivo");
            return listar.ToList();
        }

        public async Task<List<OfertaLaboral>> ListarOfertaLaboralPorNombre(string nombre)
        {
            var listar = await collection.FindAsync(x => x.tipoinstitucion == nombre);
            return listar.ToList();
        }

        public async Task<bool> Insertar(OfertaLaboral ofertaLaboral)
        {
            try
            {
                await collection.InsertOneAsync(ofertaLaboral);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<OfertaLaboral> ObtenerbyId(string id)
        {
            try
            {
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var cursor = await collection.Find(Builders<OfertaLaboral>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();

                    return cursor;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateIns(OfertaLaboral ofertaLaboral, string id)
        {
            try
            {
                bool sw = false;
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    OfertaLaboral modificar = await collection.Find(Builders<OfertaLaboral>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
                    if (modificar != null)
                    {
                        modificar.tipoinstitucion = ofertaLaboral.tipoinstitucion;
                        modificar.FechaOferta = ofertaLaboral.FechaOferta;
                        modificar.FechaLimite = ofertaLaboral.FechaLimite;
                        modificar.Descripcion = ofertaLaboral.Descripcion;
                        modificar.TituloCargo = ofertaLaboral.TituloCargo;
                        modificar.TipoContrato = ofertaLaboral.TipoContrato;
                        modificar.TipoTrabajo = ofertaLaboral.TipoTrabajo;
                        modificar.Area = ofertaLaboral.Area;
                        modificar.Caracteristicas = ofertaLaboral.Caracteristicas;
                        modificar.Estado = ofertaLaboral.Estado;

                        await collection.ReplaceOneAsync(Builders<OfertaLaboral>.Filter.Eq("_id", objectId), modificar);
                        sw = true;
                    }
                    return sw;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

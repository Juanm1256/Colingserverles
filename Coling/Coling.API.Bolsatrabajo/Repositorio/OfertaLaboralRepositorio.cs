using Coling.API.Bolsatrabajo.Contratos.Repositorio;
using Coling.API.Bolsatrabajo.Modelo;
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

            var filter = Builders<OfertaLaboral>.Filter.Empty; // Filtro vacío para obtener todos los documentos

            var cursor = await collection.FindAsync(filter);
            await cursor.ForEachAsync(doc => lista.Add(doc));

            return lista;

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
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var result = await collection.ReplaceOneAsync(Builders<OfertaLaboral>.Filter.Eq("_id", objectId), ofertaLaboral);
                    return result.ModifiedCount == 1;
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

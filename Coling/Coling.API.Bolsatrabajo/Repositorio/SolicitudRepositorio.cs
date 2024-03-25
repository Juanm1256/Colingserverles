using Coling.API.Bolsatrabajo.Contratos.Repositorio;
using Coling.API.Bolsatrabajo.Modelo;
using Coling.Shared;
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
    public class SolicitudRepositorio : ISolicitudLogic
    {
        private readonly string? cadenaconexion;
        private readonly string? tablanombre;
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Solicitud> collection;

        public SolicitudRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaconexion = configuration.GetSection("ClaveConexion").Value;
            var client = new MongoClient(cadenaconexion);
            var database = client.GetDatabase("TareaBD");
            collection = database.GetCollection<Solicitud>("Solicitud");
        }

        public async Task<bool> Eliminar(string id)
        {
            try
            {

                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var result = await collection.DeleteOneAsync(Builders<Solicitud>.Filter.Eq("_id", objectId));
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

        public async Task<List<Solicitud>> getall()
        {
            List<Solicitud> lista = new List<Solicitud>();

            var filter = Builders<Solicitud>.Filter.Empty; // Filtro vacío para obtener todos los documentos

            var cursor = await collection.FindAsync(filter);
            await cursor.ForEachAsync(doc => lista.Add(doc));

            return lista;
        }

        public async Task<bool> Insertar(Solicitud solicitud)
        {
            try
            {
                await collection.InsertOneAsync(solicitud);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Solicitud> ObtenerbyId(string id)
        {
            try
            {
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var cursor = await collection.Find(Builders<Solicitud>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();

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

        public async Task<bool> UpdateIns(Solicitud solicitud, string id)
        {
            try
            {
                ObjectId objectId;
                if (ObjectId.TryParse(id, out objectId))
                {
                    var result = await collection.ReplaceOneAsync(Builders<Solicitud>.Filter.Eq("_id", objectId), solicitud);
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

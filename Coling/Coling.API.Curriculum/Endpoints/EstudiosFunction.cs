using Coling.API.Curriculum.Contrato.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.Endpoints
{
    public class EstudiosFunction
    {
        private readonly ILogger<EstudiosFunction> _logger;
        private readonly IEstudiosRepositorio repos;

        public EstudiosFunction(ILogger<EstudiosFunction> logger, IEstudiosRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarEstudios")]
        public async Task<HttpResponseData> InsertarEstudios([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<Estudios>() ?? throw new Exception("Debe ingresar una estudios con todos sus datos");
                registro.RowKey = Guid.NewGuid().ToString();
                registro.Timestamp = DateTime.UtcNow;
                bool sw = await repos.Insertar(registro);
                if (sw)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("ListarEstudios")]
        public async Task<HttpResponseData> ListarEstudios([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            try
            {
                var lista = repos.Getall();
                var respuest = req.CreateResponse(HttpStatusCode.OK);
                await respuest.WriteAsJsonAsync(lista.Result);
                return respuest;

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        /*tarea para ahora*/
        [Function("EliminarEstudios")]
        public async Task<HttpResponseData> EliminarEstudios([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, string partitionkey, string rowkey)
        {
            try
            {
                var lista = repos.Delete(partitionkey, rowkey);
                var respuest = req.CreateResponse(HttpStatusCode.OK);
                await respuest.WriteAsJsonAsync(lista);
                return respuest;

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("ObtenerEstudios")]
        public async Task<HttpResponseData> ObtenerEstudios([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, string id)
        {
            try
            {
                var lista = repos.Get(id);
                var respuest = req.CreateResponse(HttpStatusCode.OK);
                await respuest.WriteAsJsonAsync(lista.Result);
                return respuest;

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("ModificarEstudios")]
        public async Task<HttpResponseData> ModificarEstudios([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<Estudios>() ?? throw new Exception("Debe ingresar una estudios con todos sus datos");
                bool sw = await repos.UpdateIns(registro);
                if (sw)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
    }
}

using Coling.API.Curriculum.Contrato.Repositorios;
using Coling.API.Curriculum.Modelo;
using Coling.Utilitarios.Attributes;
using Coling.Utilitarios.Roles;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
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
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Insertarspec", "Estudio")]
        [OpenApiRequestBody("application/json", typeof(Estudios))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios))]
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
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Listarspec", "Estudio")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios))]
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

        [Function("ListarEstudiosEstado")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Listarestadospec", "Estudio")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios))]
        public async Task<HttpResponseData> ListarEstudiosEstado([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            try
            {
                var lista = repos.Getallstatus();
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
        [Function("ListarPorNombreEstudioIns")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Listarestadospec", "Estudio")]
        [OpenApiParameter(name: "nombre", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Obtener por id", Description = "Obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios))]
        public async Task<HttpResponseData> ListarPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPorNombreEstudioIns/{nombre}")] HttpRequestData req, string nombre)
        {
            try
            {
                var lista = repos.ListarPorNombre(nombre);
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

        [Function("EstudioListarPorNombreInstitucion")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Listarestadospec", "Estudio")]
        [OpenApiParameter(name: "nombre", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Obtener por id", Description = "Obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios))]
        public async Task<HttpResponseData> ListarPorNombreInstitucion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "EstudioListarPorNombreInstitucion/{nombre}")] HttpRequestData req, string nombre)
        {
            try
            {
                var lista = repos.ListarPorNombreInstitucion(nombre);
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
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Eliminarspec", "Estudio")]
        [OpenApiParameter("id", In =ParameterLocation.Path,Type =typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios))]
        public async Task<HttpResponseData> EliminarEstudios([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarEstudio/{id}")] HttpRequestData req, string partitionkey, string rowkey)
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
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("obtenerspec", "Estudio")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios))]
        public async Task<HttpResponseData> ObtenerEstudios([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerEstudio/{id}")] HttpRequestData req, string id)
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
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("modificarspec", "Estudio")]
        [OpenApiRequestBody("application/json", typeof(Estudios))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios))]
        public async Task<HttpResponseData> ModificarEstudios([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
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

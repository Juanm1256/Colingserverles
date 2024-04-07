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
    public class ExperienciaLaboralFunction
    {
        private readonly ILogger<ExperienciaLaboralFunction> _logger;
        private readonly IExperienciaLaboralRepositorio repos;

        public ExperienciaLaboralFunction(ILogger<ExperienciaLaboralFunction> logger, IExperienciaLaboralRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarExperienciaLaboral")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insetarspec", "ExperienciaLaboral")]
        [OpenApiRequestBody("application/json", typeof(ExperienciaLaboral))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExperienciaLaboral))]
        public async Task<HttpResponseData> InsertarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<ExperienciaLaboral>() ?? throw new Exception("Debe ingresar una institucion con todos sus datos");
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
        [Function("ListarExperienciaLaboral")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Listarspec", "ExperienciaLaboral")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExperienciaLaboral))]
        public async Task<HttpResponseData> ListarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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

        [Function("ListarExpLabosEstado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Listarestadospec", "Estudio")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExperienciaLaboral))]
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

        [Function("ListarPorNombreExpeLabIns")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Listarestadospec", "Estudio")]
        [OpenApiParameter(name: "nombre", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Obtener por id", Description = "Obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExperienciaLaboral))]
        public async Task<HttpResponseData> ListarPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPorNombreExpeLabIns/{nombre}")] HttpRequestData req, string nombre)
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

        /*tarea para ahora*/
        [Function("EliminarExperienciaLaboral")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarspec", "ExperienciaLaboral")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExperienciaLaboral))]
        public async Task<HttpResponseData> EliminarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "delete", Route ="eliminarExperiencia/{id}")] HttpRequestData req, string partitionkey, string rowkey)
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

        [Function("ObtenerExperienciaLaboral")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerspec", "ExperienciaLaboral")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExperienciaLaboral))]
        public async Task<HttpResponseData> ObtenerExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerExperiencia/{id}")] HttpRequestData req, string id)
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

        [Function("ModificarExperienciaLaboral")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarspec", "ExperienciaLaboral")]
        [OpenApiRequestBody("application/json", typeof(ExperienciaLaboral))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExperienciaLaboral))]
        public async Task<HttpResponseData> ModificarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<ExperienciaLaboral>() ?? throw new Exception("Debe ingresar una institucion con todos sus datos");
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

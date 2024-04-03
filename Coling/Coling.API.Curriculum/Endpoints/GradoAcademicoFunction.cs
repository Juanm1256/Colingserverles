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
    public class GradoAcademicoFunction
    {
        private readonly ILogger<GradoAcademicoFunction> _logger;
        private readonly IGradoAcademicoRepositorio repos;

        public GradoAcademicoFunction(ILogger<GradoAcademicoFunction> logger, IGradoAcademicoRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarGradoAcademico")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insetarspec", "GradoAcademico")]
        [OpenApiRequestBody("application/json", typeof(GradoAcademico))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GradoAcademico))]
        public async Task<HttpResponseData> InsertarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<GradoAcademico>() ?? throw new Exception("Debe ingresar una gradoacademico con todos sus datos");
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
        [Function("ListarGradoAcademico")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Listarspec", "GradoAcademico")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GradoAcademico))]
        public async Task<HttpResponseData> ListarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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
        [Function("EliminarGradoAcademico")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarspec", "GradoAcademico")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GradoAcademico))]
        public async Task<HttpResponseData> EliminarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarGrado/{id}")] HttpRequestData req, string partitionkey, string rowkey)
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

        [Function("ObtenerGradoAcademico")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerspec", "GradoAcademico")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GradoAcademico))]
        public async Task<HttpResponseData> ObtenerGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerGrado/{id}")] HttpRequestData req, string id)
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

        [Function("ModificarGradoAcademico")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarspec", "GradoAcademico")]
        [OpenApiRequestBody("application/json", typeof(GradoAcademico))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GradoAcademico))]
        public async Task<HttpResponseData> ModificarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<GradoAcademico>() ?? throw new Exception("Debe ingresar una gradoacademico con todos sus datos");
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

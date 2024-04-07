
using Coling.API.Bolsatrabajo.Contratos.Repositorio;
using Coling.API.Bolsatrabajo.Modelo;
using Coling.Utilitarios.Attributes;
using Coling.Utilitarios.Roles;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Bolsatrabajo.EndPoint
{
    public class SolicitudFunction
    {
        private readonly ILogger<SolicitudFunction> _logger;
        private readonly ISolicitudLogic repos;

        public SolicitudFunction(ILogger<SolicitudFunction> logger, ISolicitudLogic repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("ListarSolicitud")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Listarspec", "Solicitud", Description = " Sirve para listar todas las Solicitudes")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Solicitud>), Description = "Mostrar una lista de Solicitudes")]

        public async Task<HttpResponseData> ListarSolicitud([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            try
            {
                var lista = repos.getall();
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

        [Function("ListarSolicitudEstado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarSolicitud", "Solicitud", Description = "Listar Solicitud")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Solicitud>))]
        public async Task<HttpResponseData> ListarSolicitudEstado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarSolicitudEstado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar afiliado.");
            try
            {
                var listaafiliado = repos.ListarSolicitudEstado();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaafiliado.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("ListarSolicitudPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarSolicitud", "Solicitud", Description = "Listar Solicitud")]
        [OpenApiParameter("nombre", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Solicitud>))]
        public async Task<HttpResponseData> ListarSolicitudPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarSolicitudPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            _logger.LogInformation("Ejecutando azure function para insertar afiliado.");
            try
            {
                var listaafiliado = repos.ListarSolicitudPorNombre(nombre);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaafiliado.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarSolicitud")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Insertarspec", "Solicitud", Description = " Sirve para listar todas las Solicitudes")]
        [OpenApiRequestBody("application/json", typeof(Solicitud), Description = "Oferta Laboral modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Insertara la Oferta Laboral.")]

        public async Task<HttpResponseData> InsertarSolicitud([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una institucion con todos sus datos");
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

        [Function("ModificarSolicitud")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("ModificarSolicitud", "Solicitud", Summary = "Modifica una Solicitud existente en el sistema.")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "ID de la solicitud", Description = "El ID de la Solicitud a modificar.")]
        [OpenApiRequestBody("application/json", typeof(Solicitud), Description = "Objeto de tipo Solicitud que representa la solicitud a modificar.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Insertara la Oferta Laboral.")]

        public async Task<HttpResponseData> ModificarSolicitud(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "ModificarSolicitud/{id}")] HttpRequestData req,
            string id)
        {
            try
            {
                var solicitud = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una solicitud con todos sus datos");
                bool success = await repos.UpdateIns(solicitud, id);
                if (success)
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

        [Function("EliminarSolicitud")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Eliminarprec", "Solicitud", Description = "Este endpoint nos sirve para eliminar")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Description = "ID de la Solicitud a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Confirmación de eliminación exitosa")]


        public async Task<HttpResponseData> EliminarSolicitud(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "EliminarSolicitud/{id}")] HttpRequestData req,
            string id)
        {
            try
            {
                bool success = await repos.Eliminar(id);
                if (success)
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

        [Function("ObtenerSolicitud")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Listarspec", "Solicitud", Description = " Sirve para obtener la Solicitud")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Description = "ID de la solicitud a obtener la solicitud")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Solicitud>), Description = "Datos de la Solicitud correspondiente al ID proporcionado.")]

        public async Task<HttpResponseData> ObtenerSolicitud(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerSolicitud/{id}")] HttpRequestData req,
            string id)
        {
            try
            {
                var ofertaLaboral = await repos.ObtenerbyId(id);
                if (ofertaLaboral != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(ofertaLaboral);
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

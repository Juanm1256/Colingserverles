using Coling.API.Bolsatrabajo.Contratos.Repositorio;
using Coling.API.Bolsatrabajo.Modelo;
using Coling.Shared;
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
    public class OfertaLaboralFunction
    {
        private readonly ILogger<OfertaLaboralFunction> _logger;
        private readonly IOfertaLaboralLogic repos;

        public OfertaLaboralFunction(ILogger<OfertaLaboralFunction> logger, IOfertaLaboralLogic repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("ListarOferta")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria + "," + AplicacionRoles.Institucion)]
        [OpenApiOperation("Listarspec", "OfertaLaboral", Description = " Sirve para listar todas las Ofertas Laborales")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OfertaLaboral>), Description = "Mostrar una lista de Ofertas Laborales")]

        public async Task<HttpResponseData> ListarOferta([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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

        [Function("InsertarOfertaLaboral")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Secretaria + "," + AplicacionRoles.Institucion)]
        [OpenApiOperation("Insertarspec", "OfertaLaboral", Description = " Sirve para listar todas las Ofertas Laborales")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral), Description = "Oferta Laboral modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OfertaLaboral), Description = "Insertara la Oferta Laboral.")]

        public async Task<HttpResponseData> InsertarOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar una institucion con todos sus datos");
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

        [Function("ListarOfertaLaboralEstado")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria + "," + AplicacionRoles.Institucion)]
        [OpenApiOperation("listarOfertaLaboral", "OfertaLaboral", Description = "Listar OfertaLaboral")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<OfertaLaboral>))]
        public async Task<HttpResponseData> ListarOfertaLaboralEstado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarOfertaLaboralEstado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar afiliado.");
            try
            {
                var listaafiliado = repos.ListarOfertaLaboralEstado();
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
        [Function("ListarOfertaLaboralPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria + "," + AplicacionRoles.Institucion)]
        [OpenApiOperation("listarOfertaLaboral", "OfertaLaboral", Description = "Listar OfertaLaboral")]
        [OpenApiParameter("nombre", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<OfertaLaboral>))]
        public async Task<HttpResponseData> ListarOfertaLaboralPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarOfertaLaboralPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            _logger.LogInformation("Ejecutando azure function para insertar afiliado.");
            try
            {
                var listaafiliado = repos.ListarOfertaLaboralPorNombre(nombre);
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

        [Function("ModificarOferta")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria + "," + AplicacionRoles.Institucion)]
        [OpenApiOperation("ModificarOfertaLaboral", "OfertaLaboral", Summary = "Modifica una oferta laboral existente en el sistema.")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "ID de la oferta laboral", Description = "El ID de la oferta laboral a modificar.")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral), Description = "Objeto de tipo OfertaLaboral que representa la oferta laboral a modificar.")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType:typeof(OfertaLaboral))]
        public async Task<HttpResponseData> ModificarOfertaLaboral(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "ModificarOfertaLaboral/{id}")] HttpRequestData req, string id)
        {
            try
            {
                var ofertaLaboral = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar una oferta laboral con todos sus datos");
                bool success = await repos.UpdateIns(ofertaLaboral, id);
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

        [Function("EliminarOferta")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Eliminarprec", "OfertaLaboral", Description = "Este endpoint nos sirve para eliminar")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Description = "ID de la oferta Laboral a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OfertaLaboral), Description = "Confirmación de eliminación exitosa")]
        public async Task<HttpResponseData> EliminarOfertaLaboral(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "EliminarOferta/{id}")] HttpRequestData req,
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

        [Function("ObtenerOfertaLaboral")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria + "," + AplicacionRoles.Institucion)]
        [OpenApiOperation("Listarspec", "OfertaLaboral", Description = " Sirve para obtener la oferta Laboral")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Description = "ID de la oferta a obtener la oferta Laboral")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OfertaLaboral>), Description = "Datos de la oferta Laboral correspondiente al ID proporcionado.")]

        public async Task<HttpResponseData> ObtenerOfertaLaboral(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerOfertaLaboral/{id}")] HttpRequestData req, string id)
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

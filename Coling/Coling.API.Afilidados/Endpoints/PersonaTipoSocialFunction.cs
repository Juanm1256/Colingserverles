using Coling.API.Afilidados.Contratos;
using Coling.API.Afilidados.Implementaciones;
using Coling.Shared;
using Coling.Utilitarios.Attributes;
using Coling.Utilitarios.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Afilidados.Endpoints
{
    public class PersonaTipoSocialFunction
    {
        private readonly ILogger<PersonaTipoSocialFunction> _logger;
        private readonly IPersonatiposocial personatiposocialLogic;

        public PersonaTipoSocialFunction(ILogger<PersonaTipoSocialFunction> logger, IPersonatiposocial personatiposocialLogic)
        {
            _logger = logger;
            this.personatiposocialLogic = personatiposocialLogic;
        }

        [Function("ListarPersonaTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<PersonaTipoSocial>))]
        public async Task<HttpResponseData> ListarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonaTipoSocial")] HttpRequestData req)
        {
            try
            {
                var listapersonatiposocials = personatiposocialLogic.ListarPersonaTipoSocialTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listapersonatiposocials.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ListarPersonaTipoSocialEstado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarTipoSocial", "TipoSocial", Description = "Listar TipoSocial")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<PersonaTipoSocial>))]
        public async Task<HttpResponseData> ListarTipoSocialEstado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonaTipoSocialEstado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar personatiposocial.");
            try
            {
                var listapersonatiposocial = personatiposocialLogic.ListarPersonaTipoSocialEstado();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listapersonatiposocial.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("ListarPersonaTipoSocialPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarTipoSocial", "TipoSocial", Description = "Listar TipoSocial")]
        [OpenApiParameter("nombre", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<PersonaTipoSocial>))]
        public async Task<HttpResponseData> ListarTipoSocialPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonaTipoSocialPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            _logger.LogInformation("Ejecutando azure function para insertar personatiposocial.");
            try
            {
                var listapersonatiposocial = personatiposocialLogic.ListarPersonaTipoSocialPorNombre(nombre);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listapersonatiposocial.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarPersonaTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insertarPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiRequestBody("application/json", bodyType: typeof(PersonaTipoSocial))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(PersonaTipoSocial))]
        public async Task<HttpResponseData> InsertarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarPersonaTipoSocial")] HttpRequestData req)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar una personatiposocial con todos sus datos");
                bool seGuardo = await personatiposocialLogic.InsertarPersonaTipoSocial(per);
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("EliminarPersonaTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(PersonaTipoSocial))]
        public async Task<HttpResponseData> EliminarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarPersonaTipoSocial/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var personatiposocial = await personatiposocialLogic.EliminarPersonaTipoSocial(id);
                if (personatiposocial != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(personatiposocial);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ObtenerPersonaTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(PersonaTipoSocial))]
        public async Task<HttpResponseData> ObtenerPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerPersonaTipoSocial/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var listapersonatiposocials = personatiposocialLogic.ObtenerPersonaTipoSocialById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listapersonatiposocials.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ModificarPersonaTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiRequestBody("application/json", bodyType: typeof(PersonaTipoSocial))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(PersonaTipoSocial))]
        public async Task<HttpResponseData> ModificarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarPersonaTipoSocial/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar una personatiposocial con todos sus datos");

                bool seGuardo = await personatiposocialLogic.ModificarPersonaTipoSocial(per, id);
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
    }
}

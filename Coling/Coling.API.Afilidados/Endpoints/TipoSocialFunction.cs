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
    public class TipoSocialFunction
    {
        private readonly ILogger<TipoSocialFunction> _logger;
        private readonly ITipoSocial tipoSocial;

        public TipoSocialFunction(ILogger<TipoSocialFunction> logger, ITipoSocial tipoSocial)
        {
            _logger = logger;
            this.tipoSocial = tipoSocial;
        }

        [Function("ListarTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarTipoSocial", "TipoSocial")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<TipoSocial>))]
        public async Task<HttpResponseData> ListarTipoSocials([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTipoSocial")] HttpRequestData req)
        {
            try
            {
                var listatiposocial = tipoSocial.ListarTipoSocialTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listatiposocial.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ListarTipoSocialEstado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarTipoSocial", "TipoSocial", Description = "Listar TipoSocial")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<TipoSocial>))]
        public async Task<HttpResponseData> ListarTipoSocialEstado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTipoSocialEstado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar tipoSocial.");
            try
            {
                var listatipoSocial = tipoSocial.ListarTipoSocialEstado();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listatipoSocial.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("ListarTipoSocialPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarTipoSocial", "TipoSocial", Description = "Listar TipoSocial")]
        [OpenApiParameter("nombre", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<TipoSocial>))]
        public async Task<HttpResponseData> ListarTipoSocialPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTipoSocialPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            _logger.LogInformation("Ejecutando azure function para insertar tipoSocial.");
            try
            {
                var listatipoSocial = tipoSocial.ListarTipoSocialPorNombre(nombre);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listatipoSocial.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ListarTipoSocialEstadoActivo")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarTipoSocialactivo", "TipoSocial", Description = "Listar Tipo Social activas")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<TipoSocial>))]
        public async Task<HttpResponseData> ListarPersonasEstadoActivo([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTipoSocialEstadoActivo")] HttpRequestData req)
        {
            try
            {
                var listapersonas = tipoSocial.ListarTipoSocialEstadoActivo();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listapersonas.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insertarTipoSocial", "TipoSocial")]
        [OpenApiRequestBody("application/json", bodyType: typeof(TipoSocial))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(TipoSocial))]
        public async Task<HttpResponseData> InsertarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarTipoSocial")] HttpRequestData req)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar una tipoSocial con todos sus datos");
                bool seGuardo = await tipoSocial.InsertarTipoSocial(per);
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

        [Function("EliminarTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarTipoSocial", "TipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(TipoSocial))]
        public async Task<HttpResponseData> EliminarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarTipoSocial/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var tipoSocials = await tipoSocial.EliminarTipoSocial (id);
                if (tipoSocials != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(tipoSocial);
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

        [Function("ObtenerTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerTipoSocial", "TipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(TipoSocial))]
        public async Task<HttpResponseData> ObtenerTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerTipoSocial/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var listatiposocial = tipoSocial.ObtenerTipoSocialById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listatiposocial.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ModificarTipoSocial")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarTipoSocial", "TipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiRequestBody("application/json", bodyType: typeof(TipoSocial))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(TipoSocial))]
        public async Task<HttpResponseData> ModificarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarTipoSocial/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar una tipoSocial con todos sus datos");

                bool seGuardo = await tipoSocial.ModificarTipoSocial(per, id);
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

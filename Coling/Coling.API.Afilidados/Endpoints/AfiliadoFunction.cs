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
    public class AfiliadoFunction
    {
        private readonly ILogger<AfiliadoFunction> _logger;
        private readonly IAfiliadoLogic afiliadoLogic;

        public AfiliadoFunction(ILogger<AfiliadoFunction> logger, IAfiliadoLogic afiliadoLogic)
        {
            _logger = logger;
            this.afiliadoLogic = afiliadoLogic;
        }

        [Function("ListarAfiliado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarAfiliado", "Afiliado")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Afiliado>))]
        public async Task<HttpResponseData> ListarAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarAfiliado")] HttpRequestData req)
        {
            try
            {
                var listaafiliado = afiliadoLogic.ListarAfiliadosTodos();
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

        [Function("ListarAfiliadosEstado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarAfiliados", "Afiliado", Description = "Listar Afiliados")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Afiliado>))]
        public async Task<HttpResponseData> ListarAfiliadosEstado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarAfiliadosEstado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar afiliado.");
            try
            {
                var listaafiliado = afiliadoLogic.ListarAfiliadoEstado();
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
        [Function("ListarAfiliadosPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarAfiliados", "Afiliado", Description = "Listar Afiliados")]
        [OpenApiParameter("nombre", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Afiliado>))]
        public async Task<HttpResponseData> ListarAfiliadosPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarAfiliadosPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            _logger.LogInformation("Ejecutando azure function para insertar afiliado.");
            try
            {
                var listaafiliado = afiliadoLogic.ListarAfiliadoPorNombre(nombre);
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

        [Function("ListarAfiliadoEstadoActivo")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarAfiliadoactivo", "Afiliado", Description = "Listar Afiliado activas")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Afiliado>))]
        public async Task<HttpResponseData> ListarPersonasEstadoActivo([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarAfiliadoEstadoActivo")] HttpRequestData req)
        {
            try
            {
                var listapersonas = afiliadoLogic.ListarAfiliadoEstadoActivo();
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

        [Function("InsertarAfiliado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insertarAfiliado", "Afiliado")]
        [OpenApiRequestBody("application/json", bodyType: typeof(Afiliado))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Afiliado))]
        public async Task<HttpResponseData> InsertarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarAfiliado")] HttpRequestData req)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe ingresar una afiliado con todos sus datos");
                bool seGuardo = await afiliadoLogic.InsertarAfiliado(per);
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

        [Function("EliminarAfiliado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarAfiliado", "Afiliado")]
        [OpenApiParameter("id", In =ParameterLocation.Path, Type =typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Afiliado))]
        public async Task<HttpResponseData> EliminarAfiliado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarAfiliado/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var afiliado = await afiliadoLogic.EliminarAfiliado(id);
                if (afiliado != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(afiliado);
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

        [Function("ObtenerAfiliado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerAfiliado", "Afiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Afiliado))]
        public async Task<HttpResponseData> ObtenerAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerAfiliado/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var listaafiliado = afiliadoLogic.ObtenerAfiliadoById(id);
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

        [Function("ModificarAfiliado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarAfiliado", "Afiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiRequestBody("application/json", bodyType: typeof(Afiliado))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Afiliado))]
        public async Task<HttpResponseData> ModificarAfiliado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarAfiliado/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe ingresar una afiliado con todos sus datos");

                bool seGuardo = await afiliadoLogic.ModificarAfiliado(per, id);
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

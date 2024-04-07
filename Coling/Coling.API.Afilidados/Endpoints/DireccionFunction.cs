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
    public class DireccionFunction
    {
        private readonly ILogger<DireccionFunction> _logger;
        private readonly IDireccionLogic direccionLogic;

        public DireccionFunction(ILogger<DireccionFunction> logger, IDireccionLogic direccionLogic)
        {
            _logger = logger;
            this.direccionLogic = direccionLogic;
        }

        [Function("ListarDireccion")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarDireccion", "Direccion")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Direccion>))]
        public async Task<HttpResponseData> ListarDireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarDireccion")] HttpRequestData req)
        {
            try
            {
                var listadireccion = direccionLogic.ListarDireccionTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listadireccion.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ListarDireccionEstado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarDireccion", "Direccion", Description = "Listar Direccion")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Direccion>))]
        public async Task<HttpResponseData> ListarDireccionEstado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarDireccionEstado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar direccion.");
            try
            {
                var listadireccion = direccionLogic.ListarDireccionEstado();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listadireccion.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("ListarDireccionPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarDireccion", "Direccion", Description = "Listar Direccion")]
        [OpenApiParameter("nombre", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Direccion>))]
        public async Task<HttpResponseData> ListarDireccionPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarDireccionPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            _logger.LogInformation("Ejecutando azure function para insertar direccion.");
            try
            {
                var listadireccion = direccionLogic.ListarDireccionPorNombre(nombre);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listadireccion.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarDireccion")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insertarDireccion", "Direccion")]
        [OpenApiRequestBody("application/json", bodyType: typeof(Direccion))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Direccion))]
        public async Task<HttpResponseData> InsertarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarDireccion")] HttpRequestData req)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar una direccion con todos sus datos");
                bool seGuardo = await direccionLogic.InsertarDireccion(per);
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

        [Function("EliminarDireccion")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarDireccion", "Direccion")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Direccion))]
        public async Task<HttpResponseData> EliminarDireccion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarDireccion/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var direccion = await direccionLogic.EliminarDireccion(id);
                if (direccion != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(direccion);
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

        [Function("ObtenerDireccion")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerDireccion", "Direccion")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Direccion))]
        public async Task<HttpResponseData> ObtenerDireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerDireccion/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var listadireccions = direccionLogic.ObtenerDireccionById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listadireccions.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ModificarDireccion")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarDireccion", "Direccion")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiRequestBody("application/json", bodyType: typeof(Direccion))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Direccion))]
        public async Task<HttpResponseData> ModificarDireccion([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarDireccion/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar una direccion con todos sus datos");

                bool seGuardo = await direccionLogic.ModificarDireccion(per, id);
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

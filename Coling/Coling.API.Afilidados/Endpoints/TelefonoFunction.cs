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
    public class TelefonoFunction
    {
        private readonly ILogger<TelefonoFunction> _logger;
        private readonly ITelefonoLogic telefonoLogic;

        public TelefonoFunction(ILogger<TelefonoFunction> logger, ITelefonoLogic telefonoLogic)
        {
            _logger = logger;
            this.telefonoLogic = telefonoLogic;
        }

        [Function("ListarTelefono")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarTelefono", "Telefonos")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Telefono>))]
        public async Task<HttpResponseData> ListarTelefono([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTelefono")] HttpRequestData req)
        {
            try
            {
                var listatelefonos = telefonoLogic.ListarTelefonoTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listatelefonos.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ListarTelefonoEstado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarTelefono", "Telefonos", Description = "Listar Telefono")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Telefono>))]
        public async Task<HttpResponseData> ListarTelefonoEstado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTelefonoEstado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar telefono.");
            try
            {
                var listatelefono = telefonoLogic.ListarTelefonoEstado();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listatelefono.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("ListarTelefonoPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarTelefono", "Telefonos", Description = "Listar Telefono")]
        [OpenApiParameter("nombre", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Telefono>))]
        public async Task<HttpResponseData> ListarTelefonoPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTelefonoPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            _logger.LogInformation("Ejecutando azure function para insertar telefono.");
            try
            {
                var listatelefono = telefonoLogic.ListarTelefonoPorNombre(nombre);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listatelefono.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarTelefono")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insertarTelefono", "Telefonos")]
        [OpenApiRequestBody("application/json", bodyType: typeof(Telefono))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Telefono))]
        public async Task<HttpResponseData> InsertarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarTelefono")] HttpRequestData req)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar una telefono con todos sus datos");
                bool seGuardo = await telefonoLogic.InsertarTelefono(per);
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

        [Function("EliminarTelefono")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarTelefono", "Telefonos")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Telefono))]
        public async Task<HttpResponseData> EliminarTelefono([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarTelefono/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var telefono = await telefonoLogic.EliminarTelefono(id);
                if (telefono != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(telefono);
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

        [Function("ObtenerTelefono")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerTelefono", "Telefonos")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Telefono))]
        public async Task<HttpResponseData> ObtenerTelefono([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerTelefono/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var listatelefonos = telefonoLogic.ObtenerTelefonoById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listatelefonos.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ModificarTelefono")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarTelefono", "Telefonos")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiRequestBody("application/json", bodyType: typeof(Telefono))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Telefono))]
        public async Task<HttpResponseData> ModificarTelefono([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarTelefono/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar una telefono con todos sus datos");

                bool seGuardo = await telefonoLogic.ModificarTelefono(per, id);
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

using Coling.API.Afilidados.Contratos;
using Coling.API.Afilidados.Implementaciones;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
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

        [Function("ListarTelefonos")]
        public async Task<HttpResponseData> ListarTelefonos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listartelefonos")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar telefonos.");
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

        [Function("InsertarTelefono")]
        public async Task<HttpResponseData> InsertarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertartelefono")] HttpRequestData req)
        {

            _logger.LogInformation("Ejecutando azure function para insertar telefonos.");
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
        public async Task<HttpResponseData> EliminarTelefono([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarTelefono/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar telefonos.");
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
        public async Task<HttpResponseData> ObtenerTelefono([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerTelefono/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar telefonos.");
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
        public async Task<HttpResponseData> ModificarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "modificarTelefono/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para modificar telefonos.");
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

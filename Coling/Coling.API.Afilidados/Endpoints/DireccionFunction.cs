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
        public async Task<HttpResponseData> ListarDireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarDireccion")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar direccions.");
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

        [Function("InsertarDireccion")]
        public async Task<HttpResponseData> InsertarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertardireccion")] HttpRequestData req)
        {

            _logger.LogInformation("Ejecutando azure function para insertar direccions.");
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
        public async Task<HttpResponseData> EliminarDireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarDireccion/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar direccions.");
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
        public async Task<HttpResponseData> ObtenerDireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerDireccion/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar direccions.");
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
        public async Task<HttpResponseData> ModificarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "modificarDireccion/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para modificar direccions.");
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

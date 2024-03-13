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
    public class TipoSocialFunction
    {
        private readonly ILogger<TipoSocialFunction> _logger;
        private readonly ITipoSocial tipoSocial;

        public TipoSocialFunction(ILogger<TipoSocialFunction> logger, ITipoSocial tipoSocial)
        {
            _logger = logger;
            this.tipoSocial = tipoSocial;
        }

        [Function("ListarTipoSocials")]
        public async Task<HttpResponseData> ListarTipoSocials([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listartiposocial")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar tiposocial.");
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

        [Function("InsertarTipoSocial")]
        public async Task<HttpResponseData> InsertarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertartipoSocial")] HttpRequestData req)
        {

            _logger.LogInformation("Ejecutando azure function para insertar tiposocial.");
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
        public async Task<HttpResponseData> EliminarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarTipoSocial/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar tiposocial.");
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
        public async Task<HttpResponseData> ObtenerTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerTipoSocial/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar tiposocial.");
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
        public async Task<HttpResponseData> ModificarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "modificarTipoSocial/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para modificar tiposocial.");
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

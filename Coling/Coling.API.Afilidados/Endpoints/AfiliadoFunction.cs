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
        public async Task<HttpResponseData> ListarAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarafiliado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar afiliado.");
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

        [Function("InsertarAfiliado")]
        public async Task<HttpResponseData> InsertarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarafiliado")] HttpRequestData req)
        {

            _logger.LogInformation("Ejecutando azure function para insertar afiliado.");
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
        public async Task<HttpResponseData> EliminarAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarAfiliado/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar afiliado.");
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
        public async Task<HttpResponseData> ObtenerAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerAfiliado/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar afiliado.");
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
        public async Task<HttpResponseData> ModificarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "modificarAfiliado/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para modificar afiliado.");
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

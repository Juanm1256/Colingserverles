using Coling.API.Afilidados.Contratos;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Afilidados.Endpoints
{
    public class ProfesionAfiliadoFunction
    {
        private readonly ILogger<ProfesionAfiliadoFunction> _logger;
        private readonly IProfesionAfiliado profesionAfiliadoLogic;

        public ProfesionAfiliadoFunction(ILogger<ProfesionAfiliadoFunction> logger, IProfesionAfiliado profesionAfiliadoLogic)
        {
            _logger = logger;
            this.profesionAfiliadoLogic = profesionAfiliadoLogic;
        }

        [Function("ListarProfesionAfiliados")]
        public async Task<HttpResponseData> ListarProfesionAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarprofesionAfiliados")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar profesionAfiliados.");
            try
            {
                var listaprofesionAfiliados = profesionAfiliadoLogic.ListarProfesionAfiliadoTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaprofesionAfiliados.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarProfesionAfiliado")]
        public async Task<HttpResponseData> InsertarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarprofesionAfiliado")] HttpRequestData req)
        {

            _logger.LogInformation("Ejecutando azure function para insertar profesionAfiliados.");
            try
            {
                var per = await req.ReadFromJsonAsync<ProfesionAfiliado>() ?? throw new Exception("Debe ingresar una profesionAfiliado con todos sus datos");
                bool seGuardo = await profesionAfiliadoLogic.InsertarProfesionAfiliado(per);
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

        [Function("EliminarProfesionAfiliado")]
        public async Task<HttpResponseData> EliminarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarProfesionAfiliado/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar profesionAfiliados.");
            try
            {
                var profesionAfiliado = await profesionAfiliadoLogic.EliminarProfesionAfiliado(id);
                if (profesionAfiliado != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(profesionAfiliado);
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

        [Function("ObtenerProfesionAfiliado")]
        public async Task<HttpResponseData> ObtenerProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerProfesionAfiliado/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar profesionAfiliados.");
            try
            {
                var listaprofesionAfiliados = profesionAfiliadoLogic.ObtenerProfesionAfiliadoById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaprofesionAfiliados.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ModificarProfesionAfiliado")]
        public async Task<HttpResponseData> ModificarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "modificarProfesionAfiliado/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para modificar profesionAfiliados.");
            try
            {
                var per = await req.ReadFromJsonAsync<ProfesionAfiliado>() ?? throw new Exception("Debe ingresar una profesionAfiliado con todos sus datos");

                bool seGuardo = await profesionAfiliadoLogic.ModificaProfesionAfiliado(per, id);
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

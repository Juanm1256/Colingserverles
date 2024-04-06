using Coling.API.Afilidados.Contratos;
using Coling.Shared;
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
        [OpenApiOperation("listarProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<ProfesionAfiliado>))]
        public async Task<HttpResponseData> ListarProfesionAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarprofesionAfiliados")] HttpRequestData req)
        {
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
        [OpenApiOperation("insertarProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiRequestBody("application/json", bodyType: typeof(ProfesionAfiliado))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(ProfesionAfiliado))]
        public async Task<HttpResponseData> InsertarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarprofesionAfiliado")] HttpRequestData req)
        {
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
        [OpenApiOperation("eliminarProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(ProfesionAfiliado))]
        public async Task<HttpResponseData> EliminarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarProfesionAfiliado/{id}")] HttpRequestData req, int id)
        {
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
        [OpenApiOperation("obtenerProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(ProfesionAfiliado))]
        public async Task<HttpResponseData> ObtenerProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerProfesionAfiliado/{id}")] HttpRequestData req, int id)
        {
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
        [OpenApiOperation("modificarProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiRequestBody("application/json", bodyType: typeof(ProfesionAfiliado))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(ProfesionAfiliado))]
        public async Task<HttpResponseData> ModificarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarProfesionAfiliado/{id}")] HttpRequestData req, int id)
        {
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

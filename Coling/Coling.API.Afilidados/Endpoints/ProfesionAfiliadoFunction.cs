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
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<ProfesionAfiliado>))]
        public async Task<HttpResponseData> ListarProfesionAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarProfesionAfiliados")] HttpRequestData req)
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

        [Function("ListarProfesionAfiliadoEstado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarProfesionAfiliado", "ProfesionAfiliado", Description = "Listar ProfesionAfiliado")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<ProfesionAfiliado>))]
        public async Task<HttpResponseData> ListarProfesionAfiliadoEstado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarProfesionAfiliadoEstado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar profesionAfiliado.");
            try
            {
                var listaprofesionAfiliado = profesionAfiliadoLogic.ListarProfesionAfiliadoEstado();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaprofesionAfiliado.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("ListarProfesionAfiliadoPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarProfesionAfiliado", "ProfesionAfiliado", Description = "Listar ProfesionAfiliado")]
        [OpenApiParameter("nombre", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<ProfesionAfiliado>))]
        public async Task<HttpResponseData> ListarProfesionAfiliadoPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarProfesionAfiliadoPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            _logger.LogInformation("Ejecutando azure function para insertar profesionAfiliado.");
            try
            {
                var listaprofesionAfiliado = profesionAfiliadoLogic.ListarProfesionAfiliadoPorNombre(nombre);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaprofesionAfiliado.Result);
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
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insertarProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiRequestBody("application/json", bodyType: typeof(ProfesionAfiliado))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(ProfesionAfiliado))]
        public async Task<HttpResponseData> InsertarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarProfesionAfiliado")] HttpRequestData req)
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
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(ProfesionAfiliado))]
        public async Task<HttpResponseData> EliminarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarProfesionAfiliado/{id}")] HttpRequestData req, int id)
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
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(ProfesionAfiliado))]
        public async Task<HttpResponseData> ObtenerProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerProfesionAfiliado/{id}")] HttpRequestData req, int id)
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
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarProfesionAfiliados", "ProfesionAfiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiRequestBody("application/json", bodyType: typeof(ProfesionAfiliado))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(ProfesionAfiliado))]
        public async Task<HttpResponseData> ModificarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarProfesionAfiliado/{id}")] HttpRequestData req, int id)
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

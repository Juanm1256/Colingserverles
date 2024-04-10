using Coling.API.Afilidados.Contratos;
using Coling.API.Afilidados.DTOs;
using Coling.Shared;
using Coling.Shared.DTOs;
using Coling.Utilitarios.Attributes;
using Coling.Utilitarios.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Afilidados.Endpoints
{
    public class PersonaFunction
    {
        private readonly ILogger<PersonaFunction> _logger;
        private readonly IPersonaLogic personaLogic;

        public PersonaFunction(ILogger<PersonaFunction> logger, IPersonaLogic personaLogic)
        {
            _logger = logger;
            this.personaLogic = personaLogic;
        }

        [Function("ListarPersonas")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarPersonas", "Persona", Description = "Listar Personas")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Persona>))]
        public async Task<HttpResponseData> ListarPersonas([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonas")] HttpRequestData req)
        {           
            _logger.LogInformation("Ejecutando azure function para insertar personas.");
            try
            {
                var listapersonas = personaLogic.ListarPersonaTodos();
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
        [Function("ListarPersonasEstado")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarPersonasestado", "Persona", Description = "Listar Personas")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Persona>))]
        public async Task<HttpResponseData> ListarPersonasEstado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonasEstado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar personas.");
            try
            {
                var listapersonas = personaLogic.ListarPersonaEstado();
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

        [Function("ListarPersonasEstadoActivo")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarPersonasactivo", "Persona", Description = "Listar Personas activas")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Persona>))]
        public async Task<HttpResponseData> ListarPersonasEstadoActivo([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonasEstadoActivo")] HttpRequestData req)
        {
            try
            {
                var listapersonas = personaLogic.ListarPersonaEstadoActivo();
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

        [Function("ListarPersonasPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("listarPersonaspornombre", "Persona", Description = "Listar Personas")]
        [OpenApiParameter("nombre", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Persona>))]
        public async Task<HttpResponseData> ListarPersonasPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonasPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            _logger.LogInformation("Ejecutando azure function para insertar personas.");
            try
            {
                var listapersonas = personaLogic.ListarPersonaPorNombre(nombre);
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

        [Function("InsertarPersona")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insertarPersonas", "Persona", Description = "Crear Personas")]
        [OpenApiRequestBody("application/json", bodyType: typeof(Persona))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Persona))]
        public async Task<HttpResponseData> InsertarPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarPersona")] HttpRequestData req)
        {

            _logger.LogInformation("Ejecutando azure function para insertar personas.");
            try
            {
                var per = await req.ReadFromJsonAsync<Persona>() ?? throw new Exception("Debe ingresar una persona con todos sus datos");
                bool seGuardo = await personaLogic.InsertarPersona(per);
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

        [Function("InsertarAllPersona")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("insertarAllPersonas", "Persona", Description = "Crear Personas, Telefono, Direccion")]
        [OpenApiRequestBody("application/json", bodyType: typeof(PerTelDir))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(PerTelDir))]
        public async Task<HttpResponseData> InsertarAllPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarAllPersona")] HttpRequestData req)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<PerTelDir>() ?? throw new Exception("Debe ingresar una persona con todos sus datos");
                int seGuardo = await personaLogic.InsertarAllPersona(per);
                if (seGuardo>0)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    respuesta.WriteAsJsonAsync(new { Idinsertado = seGuardo });
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

        [Function("EliminarPersona")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarPersonas", "Persona", Description = "Eliminar Personas")]
        [OpenApiParameter("id", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Persona))]
        public async Task<HttpResponseData> EliminarPersona([HttpTrigger(AuthorizationLevel.Function, "get", Route = "EliminarPersona/{id}")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar personas.");
            try
            {
                var persona = await personaLogic.Eliminarersona(id);
                if (persona != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(persona);
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

        [Function("ObtenerPersona")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerPersonas", "Persona", Description = "Obtener Personas")]
        [OpenApiParameter("id", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Persona))]
        public async Task<HttpResponseData> ObtenerPersona([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerPersona/{id}")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar personas.");
            try
            {
                var listapersonas = personaLogic.ObtenerPersonaById(id);
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

        [Function("ModificarPersona")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarPersonas", "Persona", Description = "Modificar Personas")]
        [OpenApiParameter("id", In =Microsoft.OpenApi.Models.ParameterLocation.Path, Type =typeof(string))]
        [OpenApiRequestBody("application/json", bodyType: typeof(Persona))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Persona))]
        public async Task<HttpResponseData> ModificarPersona([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarPersona/{id}")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para modificar personas.");
            try
            {
                var per = await req.ReadFromJsonAsync<Persona>() ?? throw new Exception("Debe ingresar una persona con todos sus datos");

                bool seGuardo = await personaLogic.ModificarPersona(per, id);
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

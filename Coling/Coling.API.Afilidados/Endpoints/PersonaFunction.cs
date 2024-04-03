using Coling.API.Afilidados.Contratos;
using Coling.Shared;
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
        [OpenApiOperation("listarPersonas", "Persona", Description = "Listar Personas")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<Persona>))]
        public async Task<HttpResponseData> ListarPersonas([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarpersonas")] HttpRequestData req)
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

        [Function("InsertarPersona")]
        [OpenApiOperation("insertarPersonas", "Persona", Description = "Crear Personas")]
        [OpenApiRequestBody("application/json", bodyType: typeof(Persona))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Persona))]
        public async Task<HttpResponseData> InsertarPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarpersona")] HttpRequestData req)
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

        [Function("EliminarPersona")]
        [OpenApiOperation("eliminarPersonas", "Persona", Description = "Eliminar Personas")]
        [OpenApiParameter("id", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Persona))]
        public async Task<HttpResponseData> EliminarPersona([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarPersona/{id}")] HttpRequestData req, int id)
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
        [OpenApiOperation("obtenerPersonas", "Persona", Description = "Obtener Personas")]
        [OpenApiParameter("id", In = Microsoft.OpenApi.Models.ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Persona))]
        public async Task<HttpResponseData> ObtenerPersona([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerPersona/{id}")] HttpRequestData req, int id)
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
        [OpenApiOperation("modificarPersonas", "Persona", Description = "Modificar Personas")]
        [OpenApiParameter("id", In =Microsoft.OpenApi.Models.ParameterLocation.Path, Type =typeof(string))]
        [OpenApiRequestBody("application/json", bodyType: typeof(Persona))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(Persona))]
        public async Task<HttpResponseData> ModificarPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "modificarPersona/{id}")] HttpRequestData req, int id)
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

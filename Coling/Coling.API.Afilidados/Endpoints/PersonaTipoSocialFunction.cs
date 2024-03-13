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
    public class PersonaTipoSocialFunction
    {
        private readonly ILogger<PersonaTipoSocialFunction> _logger;
        private readonly IPersonatiposocial personatiposocialLogic;

        public PersonaTipoSocialFunction(ILogger<PersonaTipoSocialFunction> logger, IPersonatiposocial personatiposocialLogic)
        {
            _logger = logger;
            this.personatiposocialLogic = personatiposocialLogic;
        }

        [Function("ListarPersonaTipoSocial")]
        public async Task<HttpResponseData> ListarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarpersonatiposocials")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar personatiposocials.");
            try
            {
                var listapersonatiposocials = personatiposocialLogic.ListarPersonaTipoSocialTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listapersonatiposocials.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarPersonaTipoSociales")]
        public async Task<HttpResponseData> InsertarPersonaTipoSociales([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarpersonatiposocial")] HttpRequestData req)
        {

            _logger.LogInformation("Ejecutando azure function para insertar personatiposocials.");
            try
            {
                var per = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar una personatiposocial con todos sus datos");
                bool seGuardo = await personatiposocialLogic.InsertarPersonaTipoSocial(per);
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

        [Function("EliminarPersonaTipoSociales")]
        public async Task<HttpResponseData> EliminarPersonaTipoSociales([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarPersonaTipoSociales/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar personatiposocials.");
            try
            {
                var personatiposocial = await personatiposocialLogic.EliminarPersonaTipoSocial(id);
                if (personatiposocial != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(personatiposocial);
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

        [Function("ObtenerPersonaTipoSociales")]
        public async Task<HttpResponseData> ObtenerPersonaTipoSociales([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerPersonaTipoSociales/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para eliminar personatiposocials.");
            try
            {
                var listapersonatiposocials = personatiposocialLogic.ObtenerPersonaTipoSocialById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listapersonatiposocials.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ModificarPersonaTipoSociales")]
        public async Task<HttpResponseData> ModificarPersonaTipoSociales([HttpTrigger(AuthorizationLevel.Function, "post", Route = "modificarPersonaTipoSociales/")] HttpRequestData req, int id)
        {

            _logger.LogInformation("Ejecutando azure function para modificar personatiposocials.");
            try
            {
                var per = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar una personatiposocial con todos sus datos");

                bool seGuardo = await personatiposocialLogic.ModificarPersonaTipoSocial(per, id);
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

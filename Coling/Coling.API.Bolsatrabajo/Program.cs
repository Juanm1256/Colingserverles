using Coling.API.Bolsatrabajo.Contratos.Repositorio;
using Coling.API.Bolsatrabajo.Repositorio;
using Coling.Utilitarios.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    //.ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IOfertaLaboralLogic, OfertaLaboralRepositorio>();
        services.AddScoped<ISolicitudLogic, SolicitudRepositorio>();
    }).ConfigureFunctionsWebApplication(x =>
    {
        x.UseMiddleware<JwtMiddleware>();
    })
    .Build();

host.Run();

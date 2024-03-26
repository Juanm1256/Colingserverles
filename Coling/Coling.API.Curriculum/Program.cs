using Coling.API.Curriculum.Contrato.Repositorios;
using Coling.API.Curriculum.Implementacion.Repositorios;
using Coling.Utilitarios.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    //.ConfigureFunctionsWebApplication(/worker => worker.UseNewtonsoftJson()/)
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IInstitucionRepositorio, InstitucionRepositorio>();
        services.AddScoped<IProfesionRepositorio, ProfesionRepositorio>();
        services.AddScoped<IGradoAcademicoRepositorio, GradoAcademicoRepositorio>();
        services.AddScoped<IEstudiosRepositorio, EstudiosRepostorio>();
        services.AddScoped<ITipoEstudioRepositorio, TipoEstudioRepositorio>();
        services.AddScoped<IExperienciaLaboralRepositorio, ExperienciaLaboralRepositorio>();
        services.AddSingleton<JwtMiddleware>();
    }).ConfigureFunctionsWebApplication(x =>
    {
        x.UseMiddleware<JwtMiddleware>();
    })
    .Build();

host.Run();
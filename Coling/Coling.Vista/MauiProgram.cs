using Coling.Vista.Servicios.Afiliados;
using Coling.Vista.Servicios.Autentificacion;
using Coling.Vista.Servicios.Bolsatrabajo;
using Coling.Vista.Servicios.Curriculum;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.Extensions.Logging;

namespace Coling.Vista
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddSweetAlert2();
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddHttpClient();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IAppService, AppService>();
            builder.Services.AddScoped<IPersonaService, PersonaService>();
            builder.Services.AddScoped<IAfiliadoService, AfiliadoServices>();
            builder.Services.AddScoped<IDireccionService, DireccionService>();
            builder.Services.AddScoped<IPersonaTipoSocialService, PersonaTipoSocialService>();
            builder.Services.AddScoped<IProfesionAfiliadoService, ProfesionAfiliadoService>();
            builder.Services.AddScoped<ITelefonoService, TelefonoService>();
            builder.Services.AddScoped<ITipoSocialService, TipoSocialService>();
            builder.Services.AddScoped<IInstitucionService, IntitucionServices>();
            builder.Services.AddScoped<IEstudioService, EstudioService>();
            builder.Services.AddScoped<IExperienciaLaboralService, ExperienciaLaboralService>();
            builder.Services.AddScoped<IGradoAcademicoService, GradoAcademicoService>();
            builder.Services.AddScoped<IProfesionService, ProfesionService>();
            builder.Services.AddScoped<ITipoEstudioService, TipoEstudioService>();
            builder.Services.AddScoped<IOfertaLaboralService, OfertaLaboralService>();
            builder.Services.AddScoped<ISolicitudService, SolicitudService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;
using WebClient.Authentication;
using WebClient.Common;
using WebClient.Components;
using WebClient.IServices;
using WebClient.Services;

namespace WebClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient();

            builder.Services.AddScoped(x => new HttpClient { BaseAddress = new Uri("https://localhost:7255/") });

            builder.Services.AddScoped<SpinnerService>();
            builder.Services.AddScoped<SpinnerHandler>();

            builder.Services.AddScoped(s =>
            {
                SpinnerHandler spinHandler = s.GetRequiredService<SpinnerHandler>();
                spinHandler.InnerHandler = new HttpClientHandler();
                NavigationManager navManager = s.GetRequiredService<NavigationManager>();
                return new HttpClient(spinHandler)
                {
                    BaseAddress = new Uri("https://localhost:7255/")
                };
            });

            builder.Services.AddControllers();

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomEnd;
                config.SnackbarConfiguration.ClearAfterNavigation = false;
            });

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>(); 
            builder.Services.AddAuthorizationCore();

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<IMenuService, MenuService>();
            builder.Services.AddTransient<ICampusService, CampusService>();
            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IExamService, ExamService>();
            builder.Services.AddTransient<IStatusService, StatusService>();
            builder.Services.AddTransient<ISendMailService, SendMailService>();
            builder.Services.AddTransient<ISubjectService, SubjectService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapControllers();

            app.UseStatusCodePagesWithRedirects("/404");

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}

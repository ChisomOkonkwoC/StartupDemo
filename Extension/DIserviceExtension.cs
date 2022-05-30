using Microsoft.Extensions.DependencyInjection;
using StartupDemo.Data.Repositories.Implementations;
using StartupDemo.Data.Repositories.Interfaces;
using StartupDemoCore.Interfaces;
using StartupDemoCore.Services;

namespace StartupDemo.Extension
{
    public static class DIserviceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ITokenGen, TokenGen>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReportServices, ReportServices>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
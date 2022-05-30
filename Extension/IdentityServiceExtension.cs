using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StartupDemo.Domain.Models;
using StartupDemoInfrastructure.Context;

namespace StartupDemo.Extension
{
    public static class IdentityServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = true;

            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StartupDemo.Data.Dtos.Request;
using StartupDemo.Domain.Models;
using StartupDemo.Extension;
using StartupDemoCore.DataValidationHelper;
using StartupDemoCore.Mapper;
using StartupDemoInfrastructure.Context;
using static StartupDemoInfrastructure.Seeder.Seeders;

namespace StartupDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                getAssembly => getAssembly.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            ));
            services.AddControllers();
            services.ConfigureIdentity();
            services.ConfigureAuthentication(Configuration);
            services.AddDependencyInjection();
            services.AddSwaggerConfiguration();
            services.AddAutoMapper(typeof(UserMappings));
            services.AddControllers().AddFluentValidation(fl =>
           {
               fl.RegisterValidatorsFromAssemblyContaining<Startup>();
                fl.ImplicitlyValidateChildProperties = true;
           });
            services.AddTransient<IValidator<CreateReportRequestDto>, CreateReportRequestValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StartupDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            Seeder.Seed(roleManager, userManager, dbContext).GetAwaiter().GetResult();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.API.Helpers;
using EMP.Data;
using EMP.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EMP.API
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
            DBKeys.BaseDatabasePath = Configuration.GetConnectionString("BaseDatabasePath");
            DBKeys.GoogleCredential = Configuration.GetConnectionString("GoogleCredential");
            services.AddDbContext<EmpContext>(options => options.UseSqlite($"Data Source={Configuration.GetConnectionString("BaseDatabasePath")}"));
            services.AddControllers();

            services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/common-auth-core5";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/common-auth-core5",
            ValidateAudience = true,
            ValidAudience = "common-auth-core5",
            ValidateLifetime = true
        };
    });


            services.AddSwaggerGen();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeGroupService, EmployeeGroupService>();
            services.AddTransient<IShipmentService, ShipmentService>();
            services.AddTransient<ISchemeProfitLossService, SchemeProfitLossService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });

            app.UseRouting();

            //app.UseFirebaseAuthentication("https://securetoken.google.com/common-auth-core5", "common-auth-core5");
            app.UseAuthentication();
            app.UseAuthorization();
            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}

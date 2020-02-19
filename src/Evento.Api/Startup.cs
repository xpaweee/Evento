using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evento.Core.Repositories;
using Evento.Infrastructure.Mappers;
using Evento.Infrastructure.Repositories;
using Evento.Infrastructure.Services;
using Evento.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Evento.Api
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
            services.AddAuthorization( x=> x.AddPolicy("HasAdminRole", p => p.RequireRole("admin")));
            services.AddMemoryCache();
            services.AddControllers();
            //services.AddTransient   <- nowy obiekt za każdym razem
            //services.AddScoped   <- pojedyncze per żądanie http
            services.AddScoped<IEventRepository,EventRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IEventService,EventService>();
            services.AddScoped<IUserService,UserService>();
            services.AddSingleton<IJwtHandler,JwtHandler>();
            services.AddScoped<ITicketService,TicketService>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            // configure jwt authentication
            var appSettingsSection = Configuration.GetSection("jwt");
            services.Configure<JwtSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Key);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    // ValidateIssuerSigningKey = true,
                    // IssuerSigningKey = new SymmetricSecurityKey(key),
                    // ValidateIssuer = false,
                    // ValidateAudience = false
                    ValidIssuer = appSettings.Issuer,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)       

                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

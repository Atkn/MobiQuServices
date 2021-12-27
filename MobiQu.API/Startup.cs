using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MobiQu.Application.Service.Abstraction;
using MobiQu.Application.Service.Concrete;
using MobiQu.Services.Application.Application.Password;
using MobiQu.Services.Application.Services.Abstraction;
using MobiQu.Services.Application.Services.Concrete;
using MobiQu.Services.Core.Domain.Entitites;
using MobiQu.Services.Core.Domain.Entitites.Projects;
using MobiQu.Services.Core.Persistence.EntityFramework.Repository.Abstraction;
using MobiQu.Services.Core.Persistence.EntityFramework.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobiQu.API
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
            services.AddControllers();

            services.AddTransient<IPasswordCryptology, PasswordCryptology>();

            services.AddTransient<IRepository<SmartBox>, Repository<SmartBox>>();
            services.AddTransient<IRepository<Device>, Repository<Device>>();
            services.AddTransient<ISmartBoxService, SmartBoxService>();

            services.AddTransient<IRepository<ColdChainBox>, Repository<ColdChainBox>>();
            services.AddTransient<IColdChainBoxService, ColdChainBoxService>();

            services.AddTransient<IRepository<Company>, Repository<Company>>();
            services.AddTransient<ICompanyService, CompanyService>();

            services.AddTransient<IRepository<MobiQuBranchTableSettings>, Repository<MobiQuBranchTableSettings>>();
            services.AddTransient<IMobiQuBranchTableSettingsService, MobiQuBranchTableSettingsService>();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("45.130.13.89"));
            });
            services.AddSwaggerGen(m =>
            {

                m.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MobiQu API",
                    Description = "A MobiQu API",
                    TermsOfService = new Uri("https://mobiqu.com/terms-of-service"),
                    Contact = new OpenApiContact
                    {
                        Name = "Atakan - Sertan - Selçuk",
                        Email = "info@mobiqu.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MobiQu",
                        Url = new Uri("https://mobiqu.com"),
                    }
                });
            });
            
        }

        // This method gets called by the runtime. Use this nmethod to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            //app.UseHttpsRedirection();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

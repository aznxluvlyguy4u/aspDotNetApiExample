using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using samsung.api.DataSource;
using samsung.api.Models.Requests;
using samsung.api.Repositories.Profiles;
using samsung.api.Services.Profiles;
using samsung_api.Models.Interfaces;

namespace samsung_api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Dependencies
            services
                .AddSingleton<DatabaseContext>()
                .AddSingleton(CreateMapper())
                // Services
                .AddTransient<IProfilesService, ProfilesService>()
                // Repositories
                .AddTransient<IProfilesRepository, ProfilesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProfileCreateRequest, IProfile>(MemberList.None).ReverseMap();
                cfg.CreateMap<IProfile, Profile>(MemberList.None).ReverseMap();
            }).CreateMapper();
        }
    }
}

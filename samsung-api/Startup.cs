﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using samsung.api.DataSource;
using samsung.api.Middleware;
using samsung.api.Models.Requests;
using samsung.api.Repositories.Profiles;
using samsung.api.Services.Profiles;
using samsung_api.Models.Interfaces;
using samsung_api.Services.Logger;
using Swashbuckle.AspNetCore.Swagger;
using System;
using Profile = samsung.api.DataSource.Models.Profile;

namespace samsung_api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Dependencies
            services
                .AddSingleton(_configuration)
                .AddSingleton(CreateMapper())
                .AddSingleton<DatabaseContext>()
                .AddSingleton<ILogger, ConsoleLogger>()
                // Services
                .AddTransient<IProfilesService, ProfilesService>()
                // Repositories
                .AddTransient<IProfilesRepository, ProfilesRepository>();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "Samsung School Link", Version = "v1" }));
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

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Samsung School Link V1"));

            app.UseHttpsRedirection();

            // TODO: make this more specific
            app.UseCors(x =>
                x
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseHttpsRedirection()
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseMvc();
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
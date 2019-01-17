using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.IdentityModel.Tokens;
using samsung.api.Constants;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung.api.Middleware;
using samsung.api.Models;
using samsung.api.Models.Requests;
using samsung.api.Models.Response;
using samsung.api.Repositories.Buddies;
using samsung.api.Repositories.GeneralUsers;
using samsung.api.Repositories.TeachingSubjects;
using samsung.api.Services.Auth;
using samsung.api.Services.Buddies;
using samsung.api.Services.GeneralUsers;
using samsung.api.Services.TeachingSubjects;
using samsung_api.Models.Interfaces;
using samsung_api.Models.Requests;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samsung_Api_Aws
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _signingKey;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            var secretKey = _configuration.GetSection("SecretKey").Value;
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();

            // Identity settings
            // add identity
            services
                .AddDefaultIdentity<AppUser>()
                .AddEntityFrameworkStores<DatabaseContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

                // ClaimsIdentity settings.
                options.ClaimsIdentity.UserIdClaimType = "Id";
            });

            // JWT settings
            // Get options from app settings
            var jwtAppSettingOptions = _configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(JwtConstants.Strings.JwtClaimIdentifiers.Rol, JwtConstants.Strings.JwtClaims.ApiAccess));
            });

            // set default require authorization
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // Dependencies
            services
                .AddScoped<UserManager<AppUser>>()
                .AddSingleton(_configuration)
                .AddSingleton(CreateMapper())
                .AddSingleton<DatabaseContext>()
                .AddSingleton<ILogger, ConsoleLogger>()
                // Services
                .AddTransient<IGeneralUsersService, GeneralUsersService>()
                .AddTransient<IBuddiesService, BuddiesService>()
                .AddTransient<ITeachingSubjectsService, TeachingSubjectsService>()
                .AddTransient<IAuthService, AuthService>()
                .AddTransient<IJwtFactory, JwtFactory>()
                // Repositories
                .AddTransient<IGeneralUsersRepository, GeneralUsersRepository>()
                .AddTransient<IBuddiesRepository, BuddiesRepository>()
                .AddTransient<ITeachingSubjectsRepository, TeachingSubjectsRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Samsung School Link", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityRequirement(security);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // TODO: Reactivate this on AWS deployement
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("../swagger/v1/swagger.json", "Samsung School Link V1"));

            app.UseAuthentication();

            // TODO: make this more specific
            app.UseCors(x =>
                x
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            // TODO: Reactivate this on AWS deployement
            app.UseHttpsRedirection();
            app//.UseMiddleware<ExceptionHandlingMiddleware>()
                .UseMvc();
        }

        private IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IGeneralUser, AppUser>().ForMember(dest => dest.UserName, map => map.MapFrom(src => src.email));
                cfg.CreateMap<GeneralUserCreateRequest, IGeneralUser>(MemberList.None).ReverseMap();
                cfg.CreateMap<TeachingSubject, ITeachingSubject>(MemberList.None).ReverseMap();
                cfg.CreateMap<ITeachingSubject, GetTeachingSubjectsResponse>(MemberList.None).ReverseMap();
                cfg.CreateMap<GeneralUser, IGeneralUser>()
                    .ForMember(d => d.firstName, opt => opt.MapFrom(src => src.Identity.FirstName))
                    .ForMember(d => d.lastName, opt => opt.MapFrom(src => src.Identity.LastName))
                    .ForMember(d => d.email, opt => opt.MapFrom(src => src.Identity.Email))
                    .ForMember(d => d.city, opt => opt.MapFrom(src => src.Identity.City))
                    .ForMember(d => d.phoneNumber, opt => opt.MapFrom(src => src.Identity.PhoneNumber))
                    .ForMember(d => d.techLevel, opt => opt.MapFrom(src => src.Identity.TechLevel))
                    .ForMember(d => d.linkedInId, opt => opt.MapFrom(src => src.Identity.LinkedInId))
                    .ForMember(d => d.facebookId, opt => opt.MapFrom(src => src.Identity.FacebookId))
                    .ReverseMap();
            }).CreateMapper();
        }
    }
}
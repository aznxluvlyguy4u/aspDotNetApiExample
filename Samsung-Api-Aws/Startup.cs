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
using samsung.api.Repositories.Interests;
using samsung.api.Repositories.Links;
using samsung.api.Repositories.TeachingAgeGroups;
using samsung.api.Repositories.TeachingLevels;
using samsung.api.Repositories.TeachingSubjects;
using samsung.api.Services.Auth;
using samsung.api.Services.AwsS3;
using samsung.api.Services.Buddies;
using samsung.api.Services.GeneralUsers;
using samsung.api.Services.Interests;
using samsung.api.Services.Links;
using samsung.api.Services.TeachingAgeGroups;
using samsung.api.Services.TeachingLevels;
using samsung.api.Services.TeachingSubjects;
using samsung_api.DataSource.Models;
using samsung_api.Models.Interfaces;
using samsung_api.Models.Requests;
using SamsungApiAws.DataSource.Models;
using SamsungApiAws.Extensions;
using SamsungApiAws.Repositories.Geo;
using SamsungApiAws.Services.Geo;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLogger = samsung_api.Services.Logger.ConsoleLogger;
using ILogger = samsung_api.Services.Logger.ILogger;

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
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddDefaultAWSOptions(_configuration.GetAWSOptions());
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
                .AddDbContext<DatabaseContext>()
                .AddSingleton<ILogger, ConsoleLogger>()
                // Services
                .AddTransient<IGeneralUsersService, GeneralUsersService>()
                .AddTransient<IBuddiesService, BuddiesService>()
                .AddTransient<ITeachingSubjectsService, TeachingSubjectsService>()
                .AddTransient<ITeachingLevelsService, TeachingLevelsService>()
                .AddTransient<ITeachingAgeGroupsService, TeachingAgeGroupsService>()
                .AddTransient<IInterestsService, InterestsService>()
                .AddTransient<IAuthService, AuthService>()
                .AddTransient<IJwtFactory, JwtFactory>()
                .AddTransient<IGeoService, GeoService>()
                .AddTransient<ILinksService, LinksService>()
                .AddTransient<IAwsS3Service, AwsS3Service>()
                // Repositories
                .AddTransient<IGeneralUsersRepository, GeneralUsersRepository>()
                .AddTransient<IInterestsRepository, InterestsRepository>()
                .AddTransient<IBuddiesRepository, BuddiesRepository>()
                .AddTransient<ITeachingSubjectsRepository, TeachingSubjectsRepository>()
                .AddTransient<ITeachingLevelsRepository, TeachingLevelsRepository>()
                .AddTransient<ITeachingAgeGroupsRepository, TeachingAgeGroupsRepository>()
                .AddTransient<IGeoRepository, GeoRepository>()
                .AddTransient<ILinksRepository, LinksRepository>()
                ;

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

            services.Verify(new ConsoleLogger());
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
            app
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseMvc();
        }

        private IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IGeneralUser, AppUser>().ForMember(dest => dest.UserName, map => map.MapFrom(src => src.Email));
                cfg.CreateMap<ICity, City>(MemberList.None).ReverseMap();
                cfg.CreateMap<ILink, Link>(MemberList.None).ReverseMap();
                    //.ForMember(dest => dest.GeneralUser, map => map.MapFrom(src => src.GeneralUser))
                    //.ForMember(dest => dest.GeneralUserId, map => map.MapFrom(src => src.GeneralUser.Id))
                    //.ReverseMap();
                cfg.CreateMap<int, ICity>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src));
                cfg.CreateMap<int, ITeachingAgeGroup>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src));
                cfg.CreateMap<int, ITeachingSubject>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src));
                cfg.CreateMap<int, ITeachingLevel>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src));
                cfg.CreateMap<int, IInterest>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src));

                cfg.CreateMap<GeneralUserTeachingAgeGroup, ITeachingAgeGroup>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src.TeachingAgeGroup.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(src => src.TeachingAgeGroup.Name))
                    .ReverseMap();

                cfg.CreateMap<GeneralUserTeachingSubject, ITeachingSubject>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src.TeachingSubject.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(src => src.TeachingSubject.Name))
                    .ReverseMap();

                cfg.CreateMap<GeneralUserTeachingLevel, ITeachingLevel>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src.TeachingLevel.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(src => src.TeachingLevel.Name))
                    .ReverseMap();

                cfg.CreateMap<GeneralUserInterest, IInterest>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Interest.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Interest.Name))
                    .ReverseMap();

                cfg.CreateMap<GeneralUser, IGeneralUser>()
                    .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.Identity.FirstName))
                    .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.Identity.LastName))
                    .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Identity.Email))
                    .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(src => src.Identity.PhoneNumber))
                    .ForMember(d => d.TechLevel, opt => opt.MapFrom(src => src.Identity.TechLevel))
                    .ForMember(d => d.LinkedInId, opt => opt.MapFrom(src => src.Identity.LinkedInId))
                    .ForMember(d => d.FacebookId, opt => opt.MapFrom(src => src.Identity.FacebookId))
                    .ForMember(d => d.TeachingAgeGroups, opt => opt.MapFrom(src => src.GeneralUserTeachingAgeGroups.Select(x => x)))
                    .ForMember(d => d.TeachingSubjects, opt => opt.MapFrom(src => src.GeneralUserTeachingSubjects.Select(x => x)))
                    .ForMember(d => d.TeachingLevels, opt => opt.MapFrom(src => src.GeneralUserTeachingLevels.Select(x => x)))
                    .ForMember(d => d.Interests, opt => opt.MapFrom(src => src.GeneralUserInterests.Select(x => x)))
                    .ReverseMap();

                cfg.CreateMap<GeneralUser, ILimitedGeneralUser>()
                    .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.Identity.FirstName))
                    .ForMember(d => d.LinkedInId, opt => opt.MapFrom(src => src.Identity.LinkedInId))
                    .ForMember(d => d.FacebookId, opt => opt.MapFrom(src => src.Identity.FacebookId))
                    .ForMember(d => d.TeachingAgeGroups, opt => opt.MapFrom(src => src.GeneralUserTeachingAgeGroups.Select(x => x)))
                    .ForMember(d => d.TeachingSubjects, opt => opt.MapFrom(src => src.GeneralUserTeachingSubjects.Select(x => x)))
                    .ForMember(d => d.TeachingLevels, opt => opt.MapFrom(src => src.GeneralUserTeachingLevels.Select(x => x)))
                    .ForMember(d => d.Interests, opt => opt.MapFrom(src => src.GeneralUserInterests.Select(x => x)))
                    .ReverseMap();

                cfg.CreateMap<IGeneralUser, ILimitedGeneralUser>().ReverseMap();

                cfg.CreateMap<BuddyRequest, IBuddyRequest>()
                    .ForMember(d => d.ReceivingGeneralUser, opt => opt.MapFrom(src => src.ReceivingGeneralUser))
                    .ForMember(d => d.RequestingGeneralUser, opt => opt.MapFrom(src => src.RequestingGeneralUser))
                    .ReverseMap();

                // Requests
                cfg.CreateMap<CreateGeneralUserRequest, IGeneralUser>(MemberList.None).ReverseMap();
                cfg.CreateMap<CreateLinkRequest, ILink>().ReverseMap();
                cfg.CreateMap<CreateFavoriteLinkRequest, ILink>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(src => src.LinkId))
                    .ReverseMap();
                cfg.CreateMap<UploadImageRequest, IImage>().ReverseMap();

                // Responses
                cfg.CreateMap<IImage, ImageResponse>(MemberList.None).ReverseMap();
                cfg.CreateMap<IGeneralUser, GetGeneralUserResponse>(MemberList.None).ReverseMap();
                cfg.CreateMap<ILimitedGeneralUser, GetGeneralUserResponse>()
                    .ForMember(d => d.LastName, opt => opt.Ignore())
                    .ForMember(d => d.Email, opt => opt.Ignore())
                    .ForMember(d => d.PhoneNumber, opt => opt.Ignore())
                    .ForMember(d => d.TechLevel, opt => opt.Ignore())
                    .ReverseMap();
                cfg.CreateMap<ILink, GetLinkResponse>(MemberList.None).ReverseMap();
                cfg.CreateMap<string, GetLinkImageSearchResponse>()
                    .ForMember(d => d.Url, opt => opt.MapFrom(src => src));
            }).CreateMapper();
        }
    }
}
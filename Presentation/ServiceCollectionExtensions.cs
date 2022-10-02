﻿using Application.Constants;
using Application.Services.Order;
using Application.Services.Product;
using Application.Services.User;
using Infrastructure.Configuration;
using Infrastructure.Persistence.MongoDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfigSections(
           this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDbConfig>(
                config.GetSection("MongoDbConfig"));
        }

        public static void AddMappings(this IServiceCollection services)
        {
            //ViewModels to DTOs mappings
            services.AddAutoMapper(typeof(MappingProfile));

            //DTOs to Domain entities mappings
            services.AddAutoMapper(typeof(Application.MappingProfile));

            //Domain entities to mongoDb collections mappings
            MongoDbPersistence.Configure();
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUserService, UserService>();

        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(AppConstants.DefaultAuthorizationPolicy, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

                auth.AddPolicy(AppConstants.AdminAuthorizationPolicy, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim("claims", "admin")
                    .RequireAuthenticatedUser().Build());
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = JwtTokenConstants.IssuerSigningKey,
                        ValidAudience = JwtTokenConstants.Audience,
                        ValidIssuer = JwtTokenConstants.Issuer,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(0)
                    };
                });
        }
    }
}



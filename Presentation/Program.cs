using Presentation;
using Infrastructure.UnitOfWork;
using Infrastructure.Persistence.MongoDb;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        //prevent automatic 400-404 response
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    });

builder.Services.ConfigureAuthentication();
builder.Services.ConfigureAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Type = SecuritySchemeType.Http
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddConfigSections(builder.Configuration);

//configure all mappings
builder.Services.AddMappings();

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration["MongoDbConfig:ConnectionString"])
);

builder.Services.AddScoped<IMongoContext, MongoContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

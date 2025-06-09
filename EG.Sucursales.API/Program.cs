using EG.Sucursales.Application.Interfaces;
using EG.Sucursales.Application.Services;
using EG.Sucursales.Domain.Interfaces;
using EG.Sucursales.Infrastructure.Context;
using EG.Sucursales.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de DapperContext
builder.Services.AddSingleton<DapperContext>();

// Repositorios
builder.Services.AddScoped<IMonedaRepository, MonedaRepository>();
builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();

// Servicios de aplicaci�n
builder.Services.AddScoped<IMonedaService, MonedaService>();

builder.Services.AddScoped<ISucursalService, SucursalService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Gesti�n de Sucursales y Monedas - EG",
        Version = "v1",
        Description = "API para gestionar monedas, sucursales y usuarios con roles usando Dapper, .NET 8 y SQL Server",
        Contact = new OpenApiContact
        {
            Name = "Equipo de Desarrollo EG",
            Email = "soporte@egempresa.com"
        }
    });

    // Si se quiere incluir documentaci�n XML:
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API EG v1");
        c.DocumentTitle = "Documentaci�n API EG";
        c.RoutePrefix = string.Empty; // Para que se cargue en ra�z: http://localhost:<puerto>/
    });
}


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

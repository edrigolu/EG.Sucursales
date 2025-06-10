using EG.Sucursales.Application.Interfaces;
using EG.Sucursales.Application.Services;
using EG.Sucursales.Domain.Interfaces;
using EG.Sucursales.Infrastructure.Context;
using EG.Sucursales.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


// Servicios de aplicación
builder.Services.AddScoped<IMonedaService, MonedaService>();
builder.Services.AddScoped<ISucursalService, SucursalService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "API de Gestión de Sucursales y Monedas - EG",
//        Version = "v1",
//        Description = "API para gestionar monedas, sucursales y usuarios con roles usando Dapper, .NET 8 y SQL Server",
//        Contact = new OpenApiContact
//        {
//            Name = "Equipo de Desarrollo EG",
//            Email = "soporte@egempresa.com"
//        }
//    });

//    // Si se quiere incluir documentación XML:
//    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
//});


var corsPolicyName = "AllowFrontend";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL de Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Si usas cookies o tokens
        });
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

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API EG v1");
//        c.DocumentTitle = "Documentación API EG";
//        c.RoutePrefix = string.Empty; // Para que se cargue en raíz: http://localhost:<puerto>/
//    });
//}


app.UseCors(corsPolicyName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

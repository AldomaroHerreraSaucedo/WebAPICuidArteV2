using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPICuidArte.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conexionString = builder.Configuration.GetConnectionString("ConexionSqlServer");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger para autenticación mediante API Key
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Ingrese su API Key para acceder a la API",
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" },
                Scheme = "ApiKeyScheme",
                Name = "X-API-KEY",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<BDContexto>(options => options.UseSqlServer(conexionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var servicio = scope.ServiceProvider;
    var contexto = servicio.GetRequiredService<BDContexto>();
    contexto.Database.EnsureCreated();

    // Cargar los datos de inicio
    BDInicializa.Inicializar(contexto);
}

app.UseHttpsRedirection();

// API KEY
var apiKey = builder.Configuration["ApiKeySettings:ApiKey"];

// Middleware para validar API KEY
app.Use(async (context, next) =>
{
    // Solo proteger rutas que empiecen con /api
    if (context.Request.Path.StartsWithSegments("/api"))
    {
        // Permitir acceso al login
        if (context.Request.Path.StartsWithSegments("/api/InicioSesion"))
        {
            await next();
            return;
        }

        var apiKeyHeader = context.Request.Headers["X-API-KEY"].FirstOrDefault();

        if (apiKeyHeader == null || apiKeyHeader != apiKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
    }

    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
